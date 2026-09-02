using UnityEngine;

public class SimpleMonster : MonoBehaviour, IDamageable
{
    [Header("데이터")]
    [SerializeField] private MonsterData monsterData;

    private int currentHealth;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = monsterData.maxHealth;
    }

    // 기존 OnTriggerEnter(Player 태그 닿으면 TakeDamage 호출 예정이던 TODO) 는 제거.
    // 이제 몬스터가 스스로 "누가 닿았는지"를 판정하지 않고,
    // 공격하는 쪽(AttackHitbox)이 IDamageable을 통해 TakeDamage를 호출해줌.

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;

        DropGene();

        // TODO : 사망 애니메이션, 사운드 등 연출은 여기에 추가 예정
        Destroy(gameObject, monsterData.destroyDelay);
    }

    /// <summary>
    /// 몬스터가 사망 시 유전자 아이템 오브젝트를 드롭하는 로직
    /// </summary>
    private void DropGene()
    {
        if (monsterData.geneToDrop == null || monsterData.genePickupPrefab == null) return;

        if (Random.value > monsterData.dropChance) return;

        int dropAmount = Random.Range(monsterData.minDropAmount, monsterData.maxDropAmount + 1);

        for (int i = 0; i < dropAmount; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-0.3f, 0.3f), 0f, Random.Range(-0.3f, 0.3f));
            Vector3 spawnPos = transform.position + randomOffset;

            GameObject dropObj = Instantiate(monsterData.genePickupPrefab, spawnPos, Quaternion.identity);

            if (dropObj.TryGetComponent<GeneDropItem>(out var dropItem))
            {
                dropItem.Setup(monsterData.geneToDrop, 1);
            }
        }
    }
}
