using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Item/Item Data")]
public class ItemData : ScriptableObject
{
    public enum ItemType
    { 
        None,       // 제작 재료
        Genes,      // 유전자
        Artifact,   // 아티팩트
        Consumable, // 음식
    }

    [Header("기본 정보")]
    public string itemID;             // 아이템 고유 ID 코드
    public string itemName;           // 화면에 표시될 아이템 이름
    public ItemType itemType;         // 아이템 타입 선택   
    public Sprite itemIcon;           // 인벤토리용 2D 이미지
    public int maxStackSize = 50;     // 유전자는 %로, 소비품은 기본 50개

    [TextArea]
    public string itemDescription;     // 아이템 설명
}
