using UnityEngine;

/// <summary>
/// 캐릭터(또는 나중에 무기)에 붙는 공격 판정용 히트박스.
///
/// 이 오브젝트의 트리거 콜라이더에 IDamageable을 구현한 대상이 들어오면,
/// owner(PlayerController)가 지금 들고 있는 IAttackSource의 Damage 값을 읽어서
/// 그대로 넣어줌. 상대가 SimpleMonster인지, 나중에 생길 Boss인지, 파괴 가능한
/// 상자인지는 전혀 몰라도 됨 — IDamageable만 구현하고 있으면 끝.
///
/// 참고: 지금 버전은 트리거에 닿기만 하면 바로 데미지가 들어가는 최소 구현임.
/// "공격 버튼을 눌렀을 때만 판정 활성화" 같은 부분은 별도 티켓(공격 입력/애니메이션
/// 이벤트 연동)에서 다룰 범위라 여기서는 일부러 넣지 않음.
/// </summary>
public class AttackHitbox : MonoBehaviour
{
    [Header("참조")]
    [SerializeField] private PlayerController owner;

    private void OnTriggerEnter(Collider other)
    {
        if (owner == null) return;

        if (other.TryGetComponent<IDamageable>(out var target))
        {
            target.TakeDamage(owner.CurrentAttackSource.Damage);
        }
    }
}
