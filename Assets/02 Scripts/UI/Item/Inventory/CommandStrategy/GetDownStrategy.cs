using System.Data.Common;
using UnityEngine;

public class GetDownStrategy : IDropRule
{
    public bool IsMatch(ItemSlot targetCell, InventoryItemDraggable draggableItem)
    {
        if (targetCell.currentItemData != null) return false;
        //return targetCell != null && targetCell.currentItemData == null;

        return true;
    }
    public void Execute(ItemSlot targetCell, InventoryItemDraggable draggableItem)
    {
        targetCell.AssignData(draggableItem.OriginalItemData, draggableItem.CurrentStack);

        if (draggableItem._SourceSlot != null)
        {
            draggableItem._SourceSlot.ClearCell();
        }

        draggableItem.ClearDraggable(); 
    }
}
