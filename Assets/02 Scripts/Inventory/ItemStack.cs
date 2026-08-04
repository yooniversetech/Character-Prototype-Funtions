using UnityEngine;

public class ItemStack : MonoBehaviour
{
    public ItemData itemData;

    public int stackCount;

    public bool IsEmpty => itemData == null || stackCount <= 0;

    public ItemStack()
    {
        itemData = null;
        stackCount = 0;
    }

    public ItemStack(ItemData data, int count)
    {
        itemData = data;
        stackCount = count;
    }

}
