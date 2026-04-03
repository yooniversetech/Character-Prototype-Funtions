using System.Data.Common;
using UnityEngine;

public class GetDownStrategy : IDropRule
{
    public bool IsMatch(ItemSlot targetCell, InventoryItemDraggable draggableItem)
    {
        if (targetCell.currentItemData != null) return false;

        return true;
    }
    public void Execute(ItemSlot targetCell, InventoryItemDraggable draggableItem)
    {
        var newData = new InventoryItem(draggableItem.OriginalItemData.Data, draggableItem.CurrentStack);
        targetCell.AssignData(newData, draggableItem.CurrentStack);

        if (draggableItem._SourceSlot != null)
        {
            draggableItem._SourceSlot.ClearSlot();
        }

        draggableItem.ClearDraggable(); 
    }
}
