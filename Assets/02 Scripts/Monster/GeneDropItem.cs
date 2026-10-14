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
        if (IsBeingPulled)
        {
            base.Update();
            return;
        }
    }

    public override int Collect(Inventory inventory)
    {
        if (geneData == null) return amount;

        int leftover = inventory.AddItem(geneData, amount);
        if (leftover > 0)
        {
            amount = leftover;
            return leftover;
        }

        // TODO : 획득 이팩트, 사운드 재생 위치
        Destroy(gameObject);
        return 0;
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