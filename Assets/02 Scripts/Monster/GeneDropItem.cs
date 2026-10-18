using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GeneDropItem : DroppedItem
{
    [SerializeField] private GeneFragmentData geneData;

    private void Start()
    {
        if (TryGetComponent<Collider>(out var col))
        {
            col.isTrigger = true;
        }
    }
    protected override void Update()
    {
        if (IsBeingPulled) // IsBeingPulled 가 true 일때 내부 코드 실행 (아이템의 상태가 Pulled 라면 true 이므로 아래 코드 실행)
        {
            base.Update(); // DroppedItem 의 Update를 실행 -> 위 조건인 IsBeingPulled 가 true 일때 실행되므로 반드시 아이템이 Pulled 상태이므로 DroppedItem 의 Update 내부에서도 case State.Pulled 가 실행되어 UpdatePulled()가 실행되고 UpdatePulled 내부에는 onArrived?.Invoke(this); 가 실행되어 결과적으로 OnItemArrived () 함수가 실행되는 결과가 됌
            return;
        }
    }

    public override int Collect(Inventory inventory) // 상속된 DroppedItem 클래스 내부의 abstract 키워드로 선언된 함수를 재정의 한 부분
    {
        if (geneData == null) return amount; // 만약 geneData 의 참조가 아무것도 없다면 amount 를 반환후 함수 실행 종료

        int leftover = inventory.AddItem(geneData, amount); // 인벤토리로 아이템 추가를 시도하고 전부 추가되면 0을, 전부 추가하지 못하고 남으면 남은만큼의 갯수를 정수로 반환 하여 leftover 에 대입
        if (leftover > 0) // leftover 가 0 보다 크다면 즉 남은 아이템들이 있다면 내부 코드를 실행하라
        {
            amount = leftover; // 남은 아이템 갯수를 amount 에 대입
            return leftover; // 남은 아이템 갯수를 반환
        }

        // TODO : 획득 이팩트, 사운드 재생 위치
        Destroy(gameObject); // 아이템 오브젝트 파괴
        return 0; // 0을 반환
    }


    public void Setup(GeneFragmentData data, int dropAmount)
    {
        geneData = data;
        amount = dropAmount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
    }

    //public override int Collect(Inventory inventory)
    //{
    //    if (geneData == null) return;

    //    int leftover = inventory.AddItem(geneData, amount);
    //    // TODO : 여기서 실제 인벤토리/유전자 도감 시스템과 연결 필요
    //    // 예시 : GeneInventoryManager.Instance.AddGenePiece(geneData.geneID, amount);
    //    if (leftover > 0)
    //    {
    //        amount = leftover;
    //        //Debug.Log($"인벤토리가 가득 차서 {leftover}개의 유전자를 획득하지 못했습니다.");
    //        return;
    //    }

    //    // TODO : 획득 이팩트, 사운드 재생 위치

    //    Destroy(gameObject);
    //}
}