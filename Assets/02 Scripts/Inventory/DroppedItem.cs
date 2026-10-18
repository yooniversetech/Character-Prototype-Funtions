using System;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ItemFloatEffect))] // DroppedItem 이 컴포넌트로 붙은 오브젝트에는 모두 ItemFloatEffect 가 함께 컴포넌트로 붙음
public abstract class DroppedItem : MonoBehaviour
{
    private enum State { Idle, Pulled } // State 타입 선언부이며 현재 State 타입 내부에는 Idle, Pulled 2개의 State 타입을 선언한 상태
    private State state = State.Idle;   // State 타입의 state 필드 선언과 동시에 Idle 로 초기화 하고있음 => 아이템 오브젝트의 상태를 위해서 선언되었으므로 아이템 오브젝트의 초기 상태는 Idle 이어야 하므로 Idle 로 초기화 한 것

    private Transform target;           // 캐릭터의 위치를 위해서 선언
    private ItemFloatEffect floatEffect; // 아이템 오브젝트의 회전 및 위아래 진자운동 연출을 위해서 선언
    private Action<DroppedItem> onArrived; // 다른 스크립트의 메서드를 Invoke(); 하여 순차호출을 위해서 선언

    public bool IsBeingPulled => state == State.Pulled; // state 의 상태가 Idle -> Pulled 로 상태 변화시 IsBeingPulled = true / state 의 상태가 Pulled -> Idle 로 상태 변화시 IsBeingPulled = false

    [Header("아이템 자석 이동 설정")]
    [SerializeField] private float moveSpeed = 5f; // 유니티 직렬화 필드로써 아이템이 끌려오는 속도를 런타임에 값을 수정해서 편하게 실험하거나 확인하기위함
    [SerializeField] private float arriveDistance = 0.3f; // 설명 불가 -> 아직 이해 되지 않음
    [SerializeField] protected int amount = 1; // 유니티 직렬화 필드로써 아이템 갯수를 런타임에 값을 수정해서 편하게 실험하거나 확인하기위함

    private void Awake() // 유니티 엔진의 특수 콜백함수로써 런타임 진입시 최초로 단 한번 내부의 코드를 실행 => Start 콜백 함수보다 더 먼저 실행되는 콜백함수
    {
        floatEffect = GetComponent<ItemFloatEffect>(); // floatEffect 에 ItemFloatEffect 를 캐싱 => 추후 세밀한 조정을 위해서 미리 캐싱
    }

    protected virtual void Update() // 유니티 엔진의 특수한 콜백함수로써 런타임에 프레임 마다 반복호출 
    {
        switch (state) // 상태를 프레임 마다 확인
        {
            case State.Idle: // 아이템 상태가 Idle 이라면 아무것도 하지 않고 switch 문 종료
                break;
            case State.Pulled:
                UpdatePulled();
                break;
        }
    }

    public abstract int Collect(Inventory inventory); // DroppedItem 을 상속하는 클래스들은 반드시 Collect 라는 함수를 재정의 해야하며 반환타입은 int, 매개변수 타입은 Inventory 로 시그니쳐도 일치시켜서 재정의 해야함

    private void UpdatePulled() // 
    {
        transform.position = Vector3.MoveTowards(
            transform.position, target.position, moveSpeed * Time.deltaTime); // 현재 이 클래스가 컴포넌트로 추가되어있는 오브젝트의 위치(아이템 오브젝트) 에 현재 위치, 타겟 (캐릭터) 위치, 이동속도 를 넘겨주어 새로운 위치(Vector3)를 반환 => Update 콜백에서 호출되고있으므로 새로운 위치가 계속해서 변경 즉 캐릭터로 아이템이 끌려가는 위치가 프레임 단위로 재갱신되어 끌려가는 시각적 효과 부여

        if ((transform.position - target.position).sqrMagnitude
            <= arriveDistance * arriveDistance) // 현재 이 클래스가 컴포넌트로 추가되어있는 오브젝트의 위치에서 타겟의 위치의 마이너스된 값을 제곱한 값보다 arriveDistance 를 제곱한 값(현재는 0.09)이 크거나 같다면 내부의 코드를 실행하라 
        {
            onArrived?.Invoke(this); // 결과 적으로 이 클래스의 Update 문에서 아이템의 상태가 Pulled 라면 ItemCollector 의 OnItemArrived 함수가 실행됨
        }
    }

    private void SetState(State next) // 아이템 상태 변환함수
    {
        if (state == next) return; // 만약 함수 호출시 넘겨받은 인자와 현재 상태가 같다면 여기서 함수 종료
        state = next; // 함수 호출 부분에서 넘겨받은 인자(next) 와 현재 state 필드의 값과 다르면 state에 인자값을 대입

        floatEffect.enabled = (state == State.Idle); // state 의 상태가 Idle 이라면 ItemFloatEffect 컴포넌트 on

        if (state == State.Idle) // state 의 상태가 Idle 이라면 ItemFloatEffect 스크립트 내부의 ResetOrigin()을 호출 => 현재 아이템의 위치에 아이템 고정
            floatEffect.ResetOrigin(transform.position);
    }


    public void StartPull(Transform target, Action<DroppedItem> onArrived)
    {
        this.target = target; // 캐릭터 위치
        this.onArrived = onArrived; // 함수 핸들을 받음
        SetState(State.Pulled); // 아이템 오브젝트 상태를 Pulled 로 변경
    }

    public void CancelPull() // 아이템 끌어당기기 취소
    {
        target = null; // 캐릭터 위치 참조 끊기
        onArrived = null; // 인벤트 참조 끊기
        SetState(State.Idle); // 아이템 오브젝트 상태를 Idle로 변경
    }
}
