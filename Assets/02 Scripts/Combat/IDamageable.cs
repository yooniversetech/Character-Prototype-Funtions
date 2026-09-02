/// <summary>
/// 데미지를 받을 수 있는 대상이 구현하는 인터페이스.
///
/// 몬스터, 나중에 생길 보스, 파괴 가능한 오브젝트 등 "맞으면 체력이 깎이는" 대상이
/// 이 인터페이스만 구현하면, 공격하는 쪽(AttackHitbox)은 상대가 정확히 어떤 클래스인지
/// 몰라도 TakeDamage를 호출할 수 있음.
/// 즉 AttackHitbox <-> 피격 대상 사이의 연결 지점(규약) 역할.
/// </summary>
public interface IDamageable
{
    void TakeDamage(int damage);
}
