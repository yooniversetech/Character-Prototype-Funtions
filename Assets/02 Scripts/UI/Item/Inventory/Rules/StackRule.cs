using UnityEngine;

public class StackRule : MonoBehaviour, IDropRule
{
    public bool IsMatch(ItemCell targetCell, InventoryItemDraggable draggableItem)
    {
        if (targetCell == null) return false;
        if (targetCell.currentItemData.ItemID != draggableItem.itemData.ItemID) return false;
        if (targetCell.CurrentStack >= targetCell.currentItemData.MaxStack) return false;

        return true;
    }
    public void Execute(ItemCell targetCell, InventoryItemDraggable draggableItem)
    {
        
    }

}
