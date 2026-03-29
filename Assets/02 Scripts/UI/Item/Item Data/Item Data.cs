using UnityEngine;

public class ItemData : ScriptableObject, IItemData
{
    // 모든 아이템에 필요한 데이터 필드

    [SerializeField] private int itemID;
    [SerializeField] private Sprite itemIcon;
    [SerializeField] private string itemName;

    [TextArea] [SerializeField] private string description;

    public bool isStackable;
    public int defalutStack = 1;
    public int maxStack;

    public ItemData Data => this;
    public int ItemID => itemID;
    public string ItemName => itemName;
    public Sprite ItemIcon => itemIcon;
    public string Description => description;

    public int MaxStack => maxStack;
    public bool IsStackable => isStackable;
}

#region Inherited Data ( ItemData를 상속받은 데이터 클래스 목록들)

// ItemData를 상속받은 데이터 클래스 목록들

[CreateAssetMenu(fileName = "New ConsumableItem", menuName = "Items / Consumable")]
public class ConsumableItemData : ItemData
{
    public float HealAmount;
    public float Duration;
    public ItemType Type;
}

[CreateAssetMenu(fileName = "New EquipmentItem", menuName = "Items / Equipment")]

public class EquipmentItemData : ItemData
{
    public int AttackPower;
    public int Defense;
    public ItemType Type;
}
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