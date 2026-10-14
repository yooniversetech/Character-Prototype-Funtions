using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ItemFloatEffect))]
public abstract class DroppedItem : MonoBehaviour
{
    private enum State { Idle, Pulled }
    private State state = State.Idle;

    private Transform target;
    private ItemFloatEffect floatEffect;
    private Action<DroppedItem> onArrived;

    public bool IsBeingPulled => state == State.Pulled;

    [Header("아이템 자석 이동 설정")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float arriveDistance = 0.3f;
    [SerializeField] protected int amount = 1;

    private void Awake()
    {
        floatEffect = GetComponent<ItemFloatEffect>();
    }

    protected virtual void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.Pulled:
                UpdatePulled();
                break;
        }
    }

    public abstract int Collect(Inventory inventory);

    private void UpdatePulled()
    {
        transform.position = Vector3.MoveTowards(
            transform.position, target.position, moveSpeed * Time.deltaTime);

        if ((transform.position - target.position).sqrMagnitude
            <= arriveDistance * arriveDistance)
        {
            onArrived?.Invoke(this);
        }
    }

    private void SetState(State next)
    {
        if (state == next) return;
        state = next;

        floatEffect.enabled = (state == State.Idle);

        if (state == State.Idle)
            floatEffect.ResetOrigin(transform.position);
    }


    public void StartPull(Transform target, Action<DroppedItem> onArrived)
    {
        this.target = target;
        this.onArrived = onArrived;
        SetState(State.Pulled);
    }

    public void CancelPull()
    {
        target = null;
        onArrived = null;
        SetState(State.Idle);
    }
}
