using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class ItemSlot
{
    public ItemData ItemData { get; private set; }
    public int StackCount { get; private set; }

    public bool IsEmpty => ItemData == null;

    public event Action OnSlotChanged;

    public ItemSlot()
    {
        ItemData = null;
        StackCount = 0;
    }

    public int AddItem(ItemData data, int amount)
    {
        if (data == null || amount <= 0)
            return amount;

        if (IsEmpty)
        {
            ItemData = data;
            StackCount = 0;
        }
        else if (ItemData != data)
        {
            return amount;
        }

        int spaceLeft = ItemData.maxStackSize - StackCount;
        int amountToAdd = Math.Min(spaceLeft, amount);

        StackCount += amountToAdd;
        OnSlotChanged?.Invoke();

        return amount - amountToAdd;
    }

    public int RemoveItem(int amount)
    {
        if (IsEmpty || amount <= 0)
            return 0;

        int amountToRemove = Math.Min(amount, StackCount);
        StackCount -= amountToRemove;

        if (StackCount <= 0)
            Clear();
        else
            OnSlotChanged?.Invoke();

        return amountToRemove;
    }

    public void Clear()
    {
        ItemData = null;
        StackCount = 0;
        OnSlotChanged?.Invoke();
    }

    public void SwapWith(ItemSlot other)
    {
        (ItemData, other.ItemData) = (other.ItemData, ItemData);
        (StackCount, other.StackCount) = (other.StackCount, StackCount);

        OnSlotChanged?.Invoke();
        other.OnSlotChanged?.Invoke();
    }
}
