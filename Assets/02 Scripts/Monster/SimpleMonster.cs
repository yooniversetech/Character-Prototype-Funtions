using UnityEngine;

public class SimpleMonster : MonoBehaviour
{
    [Header("체력")]
    [SerializeField] private int maxHealth = 10;
    private int currentHealth;

    [Header("드랍 설정")]
    [SerializeField] private GeneData geneToDrop;
    [SerializeField] private GameObject genePickupPrefab;

    [Header("죽음 연출 (선택)")]
    [SerializeField] private float destroyDelay = 0f;

    private bool isDead = false;

    private void Awake()
    {
        currentHealth = maxHealth;
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
        Destroy(gameObject, destroyDelay);
    }

    private void DropGene()
    {
        if (geneToDrop == null ||  genePickupPrefab == null) return;

        if (Random.value > geneToDrop.dropChance) return;

        int dropAmount = Random.Range(geneToDrop.minDropAmount, geneToDrop.maxDropAmount + 1);

        for (int i = 0; i < dropAmount; i++)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-0.3f, 0.3f), 0f, Random.Range(-0.3f, 0.3f));
            Vector3 spawnPos = transform.position + randomOffset;

            GameObject dropObj = Instantiate(genePickupPrefab, spawnPos, Quaternion.identity);

            if (dropObj.TryGetComponent<GeneDropItem>(out var dropItem))
            {
                dropItem.Setup(geneToDrop, 1);
            }
        }
    }
}
