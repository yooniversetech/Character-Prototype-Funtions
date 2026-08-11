using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "Item/Item Data")]
public class ItemData : ScriptableObject
{
    [Header("기본 정보")]
    public string itemID;             // 아이템 고유 ID 코드
    public string itemName;           // 화면에 표시될 아이템 이름
    public Sprite itemIcon;           // 인벤토리용 2D 이미지

    [Header("스택 설정")]
    public int maxStackSize = 1;      // 유전자는 %로, 소비품은 기본 50개

    [TextArea]
    public string itemDescription;     // 아이템 설명
}
