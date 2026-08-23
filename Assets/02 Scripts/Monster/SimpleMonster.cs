using UnityEngine;

public class SimpleMonster : MonoBehaviour
{
    [Header("데이터")]
    [SerializeField] private MonsterData monsterData;
    
    private int currentHealth;
    private bool isDead = false;

    private void Awake()
    {
        currentHealth = monsterData.maxHealth;
    }

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
