using System.Data.Common;
using UnityEngine;

public class GetDownStrategy : MonoBehaviour, IDropRule
{
    public bool IsMatch(ItemCell targetCell, InventoryItemDraggable draggableItem)
    {
        if (targetCell != null) return false;

        return true;
    }
    public void Execute(ItemCell targetCell, InventoryItemDraggable draggableItem)
    {
        if (!IsMatch(targetCell, draggableItem)) return;

        if (IsMatch(targetCell, draggableItem))
        {
            GetDownItem(targetCell, targetCell);
        }
    }

    private void GetDownItem(ItemCell source, ItemCell target, InventoryItemDraggable draggableItem)
    {
        
        source.AssignData(source.currentItemData, source.CurrentStack);
        source.UpdateUI();
    }
}
