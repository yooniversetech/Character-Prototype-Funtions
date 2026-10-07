using Unity.VisualScripting;
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

    private void Awake()
    {
        inventory = GetComponent<PlayerController>().inventory;
        results = new Collider[maxDetect];
    }

    private void Update()
    {
        if (!ShouldCheck()) return;

        int count = DetectItems();
        for (int i = 0; i < count; i++)
            TryStartPull(results[i]);

        if (count == results.Length)
            checkTimer = checkInterval;
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
        if (!col.TryGetComponent(out ItemPickUp item)) return;
        if (!CanCollect(item)) return;

        item.StartPull(transform, OnItemArrived);
    }

    private bool CanCollect(ItemPickUp item)
    {
        if (item.IsBeingPulled) return false;
        if (Time.time < blockedUntil) return false;


        // 인벤토리에서 HasSpaceFor 과 같은 함수가 생긴다면 여기에 추가 예정

        return true;
    }

    // 습득 확정 (기존 ItemPickUp 에서 살린 로직)

    private void OnItemArrived(ItemPickUp item)
    {
        int leftover = inventory.AddItem(item.itemData, item.amount);

        if (leftover == 0)
        {
            Destroy(item.gameObject);
        }
        else
        {
            item.amount = leftover;
            item.CancelPull();
            blockedUntil = Time.time + fullRetryDelay;
        }
    }

    // 외부에서 반경을 바꿀 때를 위한 대비

    public void SetRadiusBonus(float bonus)
    {
        radiusBonus = bonus;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PickupRadius);
    }
}
