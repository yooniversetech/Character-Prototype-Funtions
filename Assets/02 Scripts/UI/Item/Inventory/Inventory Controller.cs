using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryController : BaseUI
{
    [SerializeField] private ItemCell[] cells;

    private void Awake()
    {
        OnValidate();
    }

    /// <summary>
    /// 아이템셀들을 자동 할당
    /// </summary>
    private void OnValidate()
    {
        if (cells == null || cells.Length == 0)
        {
            cells = GetComponentsInChildren<ItemCell>();
        }
    }

    /// <summary>
    /// 아이템을 합칠수있는지에 대한 여부 확인
    /// </summary>
    /// <param name="data"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    public bool CanAddItem(ItemData data, int amount)
    {
        if (data.isStackable)
        {
            foreach (var cell in cells)
            {
                if (!cell.IsEmpty && cell.ContainedItem.Data.ID == data.ID)
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
