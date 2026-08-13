using UnityEngine;

[RequireComponent (typeof(Collider))]
public class GeneDropItem : MonoBehaviour
{
    [SerializeField] private GeneData geneData;
    [SerializeField] private int amount = 1;

    [Header("연출 (선택)")]
    [SerializeField] private float bobSpeed = 2f;
    [SerializeField] private float bobHeight = 0.1f;
    private Vector3 startPos;

    public void Setup(GeneData data, int dropAmount)
    {
        geneData = data;
        amount = dropAmount;
    }

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

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Collect();
    }

    private void Collect()
    {
        if (geneData == null) return;

        // TODO : 여기서 실제 인벤토리/유전자 도감 시스템과 연결 필요
        // 예시 : GeneInventoryManager.Instance.AddGenePiece(geneData.geneID, amount);
        Debug.Log($"[유전자 획득] {geneData.geneName} X {amount}");

        // TODO : 획득 이팩트, 사운드 재생 위치

        Destroy(gameObject);
    }
}
