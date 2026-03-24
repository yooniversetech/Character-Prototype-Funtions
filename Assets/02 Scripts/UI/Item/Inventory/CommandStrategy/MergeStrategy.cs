using UnityEngine;

public class MergeStrategy : IDropRule
{
    /// <summary>
    /// 커맨드 패턴에서 사용될 메서드로, 드랍된 아이템이 스택이 가능한지 여부를 판단
    /// </summary>
    /// <param name="targetCell"></param>
    /// <param name="draggableItem"></param>
    /// <returns></returns>
    public bool IsMatch(ItemSlot targetCell, InventoryItemDraggable draggableItem)
    {

        if (targetCell == null) return false;
        if (targetCell.currentItemData == null) return false;
        if (targetCell.CurrentStack >= targetCell.currentItemData.MaxStack) return false;
        if (targetCell.currentItemData.ItemID != draggableItem.OriginalItemData.ItemID) return false;

        return true;
    }

    /// <summary>
    /// IsMatch에서 스택이 가능한 것으로 판단된 경우, 실제로 스택을 수행하는 메서드
    /// </summary>
    /// <param name="targetCell"></param>
    /// <param name="draggableItem"></param>
    public void Execute(ItemSlot targetCell, InventoryItemDraggable draggableItem)
    {
        if (!IsMatch(targetCell, draggableItem)) return;

        if (IsMatch(targetCell, draggableItem))
        {
            AddItem(targetCell, draggableItem);
        }
    }

    private void AddItem(ItemSlot targetCell, InventoryItemDraggable draggableItem)
    {
        int total = targetCell.CurrentStack + draggableItem.CurrentStack;
        int max = draggableItem.OriginalItemData.MaxStack;

        if (total <= max)
        {
            targetCell.CurrentStack = total;
            draggableItem.CurrentStack = 0;
        }
        else
        {
            targetCell.CurrentStack = max;
            draggableItem.CurrentStack = total - max;
        }
    }
}