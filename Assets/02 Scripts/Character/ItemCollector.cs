using System.Collections.Generic;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [Header("습득 설정")]
    [SerializeField] private float pickupRadius = 3f;
    [SerializeField] private float checkInterval = 0.1f;
    [SerializeField] private LayerMask itemlayer;
    [SerializeField] private int maxDetect = 256;

    [Header("인벤토리 가득 찼을 때 설정")]
    [Tooltip("습득 실패 후 이 시간(초) 동안은 새로끌어오지 않음")]
    [SerializeField] private float fullRetryDelay = 1f;

    private Inventory inventory;
    private Collider[] results;
    private float checkTimer;
    private float blockedUntil; // 이 시간 전까지는 습득시도 안 함.

    private float radiusBonus;
    public float PickupRadius => pickupRadius + radiusBonus;

    private readonly HashSet<DroppedItem> pulling = new(); // 객체를 정수로 저장하고 끌려오거나 끌려오는 상태에서 해제된 객체를 담아서 관리하기 위한 읽기전용 필드
    private readonly List<DroppedItem> releaseBuffer = new();


    private void Awake()
    {
        inventory = GetComponent<PlayerController>().inventory;
        results = new Collider[maxDetect];
    }

    private void Update()
    {
        if (!ShouldCheck()) return;

        ReleaseOutOfRange();

        int count = DetectItems();
        //Debug.Log($"감지된 개수: {count}");
        for (int i = 0; i < count; i++)
            TryStartPull(results[i]);

        if (count == results.Length)
            checkTimer = checkInterval;
    }

    private void ReleaseOutOfRange()
    {
        float r = PickupRadius;
        float rSqr = r * r;
        releaseBuffer.Clear();

        foreach (var item in pulling)
        {
            if (item == null)
            {
                releaseBuffer.Add(item);
                continue;
            }

            if ((item.transform.position - transform.position).sqrMagnitude > rSqr)
            {
                item.CancelPull();
                releaseBuffer.Add(item);
            }
        }

        foreach (var item in releaseBuffer)
            pulling.Remove(item);
    }

    //---------- || 감지 메서드 || -----------
    //           \/            \/  

    private bool ShouldCheck()
    {
        checkTimer += Time.deltaTime;
        if (checkTimer < checkInterval) return false;

        checkTimer = 0f;
        return true;
    }

    private int DetectItems()
    {
        return Physics.OverlapSphereNonAlloc(
            transform.position,
            PickupRadius,
            results,
            itemlayer);
    }

    private void TryStartPull(Collider col)
    {
        if (!col.TryGetComponent(out DroppedItem item)) return;
        if (!CanCollect(item)) return;

        item.StartPull(transform, OnItemArrived); // 매개변수로 캐릭터의 위치, OnItemArrived 라는 함수 핸들을 StartPull 로 넘겨주어 호출
        pulling.Add(item);
    }

    private bool CanCollect(DroppedItem item)
    {
        if (item.IsBeingPulled) return false;
        if (Time.time < blockedUntil) return false;


        // 인벤토리에서 HasSpaceFor 과 같은 함수가 생긴다면 여기에 추가 예정

        return true;
    }

    // 습득 확정 (기존 ItemPickUp 에서 살린 로직)

    private void OnItemArrived(DroppedItem item) // 
    {
        pulling.Remove(item); // pulling 에 추가된 해쉬값을 삭제

        int leftover = item.Collect(inventory); // GeneDropItem 으로 부터 끝까지 실행되어 종료된 반환값은 0이므로 실행이 끝까지 성공한 경우 leftover = 0, Collect의 첫번째 조건문에서 함수 종료되었을 시 leftover = amount 
        if (leftover > 0) // Collect의 함수가 끝까지 실행되지 않았을 경우 즉 leftover의 값이 0 보다 크다면 내부 코드 실행
        {
            item.CancelPull(); // 아이템 오브젝트 끌어당기는 연출 취소
            blockedUntil = Time.time + fullRetryDelay; // 현재 1을 blockedUntil 에 대입
        }
    }

    // 외부에서 반경을 바꿀 때를 위한 대비

    public void SetRadiusBonus(float bonus)
    {
        radiusBonus = bonus;
    }

    private void OnDrawGizmosSelected() // 테스트용 기즈모 (추후 삭제 예정)
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PickupRadius);
    }
}
