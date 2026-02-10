using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private ItemCell[] cells;

    private void Awake()
    {
        if (cells == null || cells.Length == 0)
        {
            cells = GetComponentsInChildren<ItemCell>();
        }
    }

    public bool CanAddItem(ItemData data, int amount)
    {
        if (data.isStackable)
        {
            foreach (var cell in cells)
            {
                if (!cell.IsEmpty && cell.ContainedItem.Info.id == data.id)
                {
                    IDraggable targetItem = cell.ContainedItem;

                    if (targetItem.CurrentStack < data.maxStack)
                    {
                        int canAdd = data.maxStack - targetItem.CurrentStack;
                        int toAdd = Mathf.Min(canAdd, amount);

                        targetItem.CurrentStack += toAdd;
                        amount -= toAdd;

                        if (amount <= 0) return true;
                    }
                }
            }
        }
        return true;
    }


    private ItemCell FindEmptySlot()
    {
        foreach (var cell in cells)
        {
            if (cell.IsEmpty) return cell;
        }
        return null;
    }
}
