/// <summary>
/// 무기를 장착하지 않았을 때(맨손)의 기본 공격 소스.
///
/// PlayerController가 처음 생성될 때부터 이 클래스로 CurrentAttackSource를
/// 채워두는 용도(Null Object 패턴) — 이렇게 해두면 AttackHitbox 쪽에서
/// "혹시 아무것도 장착 안 했으면 null이라 에러 나지 않을까" 하는 null 체크를
/// 안 해도 됨. 무기 장착 로직이 아직 없는 지금 시점에도 이 클래스 하나로
/// 이미 "맨손 공격"이 정상 동작함.
/// </summary>
public class UnarmedAttackSource : IAttackSource
{
    private readonly int damage;

    public UnarmedAttackSource(int damage)
    {
        this.damage = damage;
    }

    public int Damage => damage;
}
