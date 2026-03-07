using UnityEngine;

public class AddRule : MonoBehaviour, IDropRule
{
    /// <summary>
    /// 커맨드 패턴에서 사용될 메서드로, 드랍된 아이템이 스택이 가능한지 여부를 판단
    /// </summary>
    /// <param name="targetCell"></param>
    /// <param name="draggableItem"></param>
    /// <returns></returns>
    public bool IsMatch(ItemCell targetCell, InventoryItemDraggable draggableItem)
    {
        if (targetCell == null) return false;
        if (targetCell.currentItemData.ItemID != draggableItem.itemData.ItemID) return false;
        if (targetCell.CurrentStack >= targetCell.currentItemData.MaxStack) return false;

        return true;
    }

    /// <summary>
    /// IsMatch에서 스택이 가능한 것으로 판단된 경우, 실제로 스택을 수행하는 메서드
    /// </summary>
    /// <param name="targetCell"></param>
    /// <param name="draggableItem"></param>
    public void Execute(ItemCell targetCell, InventoryItemDraggable draggableItem)
    {
        if (!IsMatch(targetCell, draggableItem)) return;

        if (IsMatch(targetCell, draggableItem))
        {
            // 1. 합쳐질 아이템의 스택 수 계산
            // 2. 스택 수가 최대 스택 수를 초과하는 경우, 초과된 부분은 드래그가 일어난 셀에 남겨둠
            // 3. 
        }
    }

    private void AddItem(ItemCell targetCell, InventoryItemDraggable draggableItem)
    {
        if (targetCell.CurrentStack + draggableItem.CurrentStack <= draggableItem.itemData.MaxStack)
        {
            targetCell.CurrentStack += draggableItem.CurrentStack;
            draggableItem.CurrentStack = 0;
            draggableItem.FinishDragging();
        }
        else
        {
            targetCell.CurrentStack = draggableItem.itemData.MaxStack;
            draggableItem.CurrentStack = (targetCell.CurrentStack + draggableItem.CurrentStack) - draggableItem.itemData.MaxStack;

            draggableItem.transform.position = draggableItem.originalParent.position;
            draggableItem.transform.SetParent(draggableItem.originalParent);
        }
    }
}