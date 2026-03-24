using UnityEngine;

public interface IDropRule
{
    bool IsMatch(ItemSlot targetCell, InventoryItemDraggable draggableItem);

    void Execute(ItemSlot targetCell, InventoryItemDraggable draggableItem);
}
