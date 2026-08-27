using UnityEngine;

[RequireComponent(typeof(Collider))]
public class GeneDropItem : MonoBehaviour
{
    [SerializeField] private GeneFragmentData geneData;
    [SerializeField] private int amount = 1;

    [Header("연출 (선택)")]
    [SerializeField] private float bobSpeed = 2f;
    [SerializeField] private float bobHeight = 0.1f;
    private Vector3 startPos;


    private void Start()
    {
        startPos = transform.position;

        if (TryGetComponent<Collider>(out var col))
        {
            col.isTrigger = true;
        }
    }
    private void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    public void Setup(GeneFragmentData data, int dropAmount)
    {
        geneData = data;
        amount = dropAmount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (other.TryGetComponent<PlayerController>(out var player))
        {
            Collect(player.inventory);
        }
    }

    private void Collect(Inventory inventory)
    {
        if (geneData == null) return;

        int leftover = inventory.AddItem(geneData, amount);
        // TODO : 여기서 실제 인벤토리/유전자 도감 시스템과 연결 필요
        // 예시 : GeneInventoryManager.Instance.AddGenePiece(geneData.geneID, amount);
        if (leftover > 0)
        {
            Debug.Log($"인벤토리가 가득 차서 {leftover}개의 유전자를 획득하지 못했습니다.");
            return;
        }

        // TODO : 획득 이팩트, 사운드 재생 위치

        Destroy(gameObject);
    }
}
