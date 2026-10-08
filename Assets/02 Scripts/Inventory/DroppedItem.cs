using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class DroppedItem : MonoBehaviour
{
    protected int amount = 1;

    [Header("아이템 자석 이동 설정")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float arriveDistance = 0.3f;

    private Transform target;
    private Action<DroppedItem> onArrived;

    public bool IsBeingPulled => target != null;

    protected virtual void Update()
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
    protected abstract void Collect(Inventory inventory);

    public void StartPull(Transform target, Action<DroppedItem> onArrived)
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
