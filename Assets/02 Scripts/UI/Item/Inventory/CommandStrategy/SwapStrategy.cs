using UnityEngine;

public class SwapStrategy : MonoBehaviour, IDropRule
{
    public bool IsMatch(ItemSlot targetCell, InventoryItemDraggable draggableItem)
    {
        if (targetCell == null) return false;
        if (targetCell.currentItemData.ItemID == draggableItem.OriginalItemData.ItemID) return false;

        return true;
    }
    public void Execute(ItemSlot targetCell, InventoryItemDraggable draggableItem)
    {
        if (!IsMatch(targetCell, draggableItem)) return;

        if (IsMatch(targetCell, draggableItem))
        {
            // 1. 드랍된 아이템과 셀의 위치만 바꿔치기
            //    ㄴ1> 드랍된 아이템은 드랍된 셀에 안착
            //    ㄴ2> 드랍된 셀에 있던 아이템은 드래그된 아이템 위치의 셀로 이동
            // 2. 필요 변수 및 필드 : 드래그된 아이템 위치, 드랍된 아이템 위치, 드래그한 아이템의 데이터, 드랍된 셀의 아이템 데이터
        }
    }
}
