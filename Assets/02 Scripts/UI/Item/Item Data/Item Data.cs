using UnityEngine;

#region ItemDatas (모든 아이템 데이터들)
public class ItemData : ScriptableObject
{
    // 모든 아이템에 필요한 데이터 필드
    public Sprite icon;

    public bool isStackable;
    public int defalutStack = 1;
    public int maxStack;

    [TextArea]
    public int id;
    public string itemName;
    public string description;
}

#region Inherited Data ( ItemData를 상속받은 데이터 클래스 목록들)

// ItemData를 상속받은 데이터 클래스 목록들

[CreateAssetMenu(fileName = "New ConsumableItem", menuName = "Items / Consumable")]
public class ConsumableItemData : ItemData
{
    public float healAmount;
    public float duration;
    public ItemType type;
}

[CreateAssetMenu(fileName = "New EquipmentItem", menuName = "Items / Equipment")]

public class EquipmentItemData : ItemData
{
    public int attackPower;
    public int defense;
    public ItemType type;
}
#endregion

#endregion

#region Enumerated ItemTypes (열거된 아이템 타입 목록)
public enum ItemType
{
    Consumable,
    Equipment,
    Material
}

public enum ConsumableItemCategory
{
    Meat,
    Vegetable,
    Fruit,
    Drinkable
}

public enum EquipmentItemCategory
{
    Weapon,
    Armor,
    Artifact
}
#endregion