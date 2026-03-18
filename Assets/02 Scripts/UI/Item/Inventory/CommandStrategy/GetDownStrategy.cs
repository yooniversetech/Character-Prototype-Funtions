using System.Data.Common;
using UnityEngine;

public class GetDownStrategy : IDropRule
{
    public bool IsMatch(ItemCell targetCell, InventoryItemDraggable draggableItem)
    {
        return targetCell != null && targetCell.currentItemData == null;
    }
    public void Execute(ItemCell targetCell, InventoryItemDraggable draggableItem)
    {
        targetCell.AssignData(draggableItem.OriginalItemData, draggableItem.CurrentStack);

        draggableItem.ClearDraggable();

    }

    private void GetDownItem(ItemCell source, ItemCell target)
    {
        target.AssignData(source.currentItemData, source.CurrentStack);
        source.ClearCell();
    }
}
