using System;
using Unity.VisualScripting;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemData itemData;
    public int amount = 1;

    [Header("아이템 자석 이동 설정")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float arriveDistance = 0.3f;

    private Transform target;
    private Action<ItemPickUp> onArrived;

    public bool IsBeingPulled => target != null;

    private void Update()
    {
        if (target == null) return;

        transform.position = Vector3.MoveTowards(
            transform.position, target.position, moveSpeed * Time.deltaTime);

        if ((transform.position - target.position).sqrMagnitude
            <= arriveDistance * arriveDistance)
        {
            onArrived?.Invoke(this);
        }
    }

    public void StartPull(Transform target, Action<ItemPickUp> onArrived)
    {
        this.target = target;
        this.onArrived = onArrived;
    }

    public void CancelPull()
    {
        target = null;
        onArrived = null;
    }
}
