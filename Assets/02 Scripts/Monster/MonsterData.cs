using UnityEngine;

[CreateAssetMenu(menuName = "Genelien/MonsterData")]
public class MonsterData : ScriptableObject
{
    [Header("기본 정보")]
    public string monsterID;
    public string monsterName;

    [Header("체력")]
    public int maxHealth = 10;

    [Header("드랍 설정")]
    public GeneFragmentData geneToDrop;
    public GameObject genePickupPrefab;
    [Range(0f, 1f)] public float dropChance = 1f;
    public int minDropAmount = 1;
    public int maxDropAmount = 1;

    [Header("죽음 연철 (선택)")]
    public float destroyDelay = 0f;
}
