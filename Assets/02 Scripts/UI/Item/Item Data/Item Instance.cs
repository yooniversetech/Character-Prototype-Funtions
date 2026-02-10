using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    public ItemData itemData;
    public int currentAmount;
    public int durability;

    public ItemInstance(ItemData sourceData)
    {
        this.itemData = sourceData;
        this.currentAmount = 1;
        this.durability = 100;
    }
}