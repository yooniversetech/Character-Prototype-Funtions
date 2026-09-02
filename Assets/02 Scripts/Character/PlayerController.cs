using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterMove characterMove;
    public Inventory inventory;
    //public EquipmentManager equipmentManager; // 아이템 장착 로직 (별도 클래스 추가 예정)
    //public WeaponUser weaponUser; // 무기 사용 로직 (별도 클래스 추가 예정)

    [Header("전투")]
    [SerializeField] private int unarmedDamage = 1; // 맨손 공격 데미지 기본값

    /// <summary>
    /// 현재 캐릭터가 사용 중인 공격 소스(전략 패턴). AttackHitbox가 타격 시점에
    /// 이 값의 Damage를 읽어감. 무기 장착/해제, 유전자 능력 활성화가 생기면
    /// SetAttackSource()를 호출해서 갈아끼우기 위함 — 지금은 항상 맨손으로 초기화.
    /// </summary>
    public IAttackSource CurrentAttackSource { get; private set; }

    private void Awake()
    {
        inventory = new Inventory(36);
        CurrentAttackSource = new UnarmedAttackSource(unarmedDamage);
    }

    private void Start()
    {
        //equipmentManager = new EquipmentManager(inventory);
        //weaponUser = new WeaponUser(equipmentManager);
    }

    /// <summary>
    /// 현재 공격 소스를 교체하는 확장 지점.
    /// 지금은 아무도 호출하지 않지만, 나중에 무기 장착(EquipmentManager/WeaponUser)이나
    /// 유전자 State의 Enter() 로직이 이 메서드 하나만 호출하면 되도록 미리 열어둠.
    /// null을 넘기면 다시 맨손 상태로 되돌아감(Null Object 유지).
    /// </summary>
    public void SetAttackSource(IAttackSource source)
    {
        CurrentAttackSource = source ?? new UnarmedAttackSource(unarmedDamage);
    }
}
