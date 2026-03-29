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
        var newData = new InventoryItem(draggableItem.OriginalItemData.Data, draggableItem.CurrentStack);
        Debug.Log($"GetDownStrategy: Executing drop. Target Cell: {targetCell.name}, Draggable Item: {draggableItem.name}, Item Data: {newData.ItemName}, Stack: {newData.CurrentStack}");
        targetCell.AssignData(newData, draggableItem.CurrentStack);

        if (draggableItem._SourceSlot != null)
        {
            draggableItem._SourceSlot.ClearSlot();
        }

        draggableItem.ClearDraggable(); 
    }
}
