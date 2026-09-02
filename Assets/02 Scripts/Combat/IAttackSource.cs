/// <summary>
/// 캐릭터가 "무엇으로" 공격하고 있는지를 나타내는 규약(전략 패턴의 전략 인터페이스).
///
/// 맨손(UnarmedAttackSource), 무기, 나중에 유전자 능력까지 전부 이 인터페이스만
/// 구현하면 됨. PlayerController/AttackHitbox 쪽 코드는 지금 어떤 구현체가
/// 꽂혀있는지 몰라도 Damage 값만 읽어서 쓰면 되고, 새 공격 소스가 추가돼도
/// 이 인터페이스를 구현하는 클래스 하나만 늘어날 뿐 호출부는 안 바뀜.
/// </summary>
public interface IAttackSource
{
    /// <summary>이 공격 소스로 타격했을 때 들어가는 데미지 값.</summary>
    int Damage { get; } 
}
