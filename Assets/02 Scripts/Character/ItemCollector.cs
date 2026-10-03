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
        
    }

    private void Update()
    { 
        
    }

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
    }

    private bool CanCollect(ItemPickUp item)
    {
        return true;
    }

    private void StartPull()
    {

    }
}
