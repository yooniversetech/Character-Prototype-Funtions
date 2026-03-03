using UnityEngine;

public interface IDropRule
{
    bool IsMatch(ItemCell targetCell, InventoryItemDraggable draggableItem);

    void Execute(ItemCell targetCell, InventoryItemDraggable draggableItem);
}
