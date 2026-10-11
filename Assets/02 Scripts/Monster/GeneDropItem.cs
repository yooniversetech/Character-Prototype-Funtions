using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GeneDropItem : DroppedItem
{
    [SerializeField] private GeneFragmentData geneData;
    //[SerializeField] private int amount = 1;

    [Header("연출 (선택)")]
    [SerializeField] private float bobSpeed = 2f;
    [SerializeField] private float bobHeight = 0.1f;
    [SerializeField] private float defortRotSpeed = 55f;  // 일반 아이템 회전속도
    //[SerializeField] private float artRotSpeed = 20f;   // 추후 보스, 아티팩트 회전 속도는 다르게 제어
    private Vector3 startPos;


    private void Start()
    {
        startPos = transform.position;

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

        ItemBoping();
        ItemRotate();
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

    private void ItemBoping()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void ItemRotate()
    {
        transform.Rotate(0f, defortRotSpeed * Time.deltaTime, 0f, Space.World);
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