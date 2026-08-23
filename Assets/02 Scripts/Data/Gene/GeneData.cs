using UnityEngine;

[CreateAssetMenu(menuName = "Genelien/GeneData")]
public class GeneData : ScriptableObject
{
    [Header("기본 정보")]
    public string geneID;
    public string geneName;
    public Sprite geneIcon;
    public string description;

    [Header("수집 관련")]
    public int totalPiecesNeeded = 100;
    public bool isBossGene = false;
}
