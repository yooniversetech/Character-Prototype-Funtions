using UnityEngine;

public class GetDownRule : MonoBehaviour, IDropRule
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
            GetDownItem(targetCell, draggableItem);
        }
    }

    private void GetDownItem(ItemCell targetCell, InventoryItemDraggable draggableItem)
    {
        targetCell.CurrentStack = draggableItem.CurrentStack;
        draggableItem.CurrentStack = 0;
    }
}
