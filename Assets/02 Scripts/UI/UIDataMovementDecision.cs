using UnityEngine;
using UnityEngine.Rendering;

public class UIDataMovementDecision : MonoBehaviour
{
    public static UIDataMovementDecision Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void ProcessDrop(IDraggable draggableObject, ICell targetCell)
    {
        if (draggableObject.CurrentCell == targetCell) return;

        if (targetCell.CanAccept(draggableObject))
        {
            if (!targetCell.IsEmpty && TryMerge(draggableObject, targetCell))
            {
                return;
            }
            ExecuteSwap(draggableObject, targetCell);
        }
    }

    private bool TryMerge(IDraggable draggable, ICell target)
    {
        // 여기서 아이템 수량을 합치는 로직
        // 아이템 데이터인지 확인 후, ID가 같다면 수량 더하고 성공이면 true반환
        return false;
    }

    private void ExecuteSwap(IDraggable dragged, ICell target)
    {
        ICell sourceCell = dragged.CurrentCell;
        IDraggable existingTargetItem = target.ContainedItem;

        sourceCell.ClearCell();
        if (existingTargetItem != null)
        {
            target.ClearCell();
            sourceCell.SetItem(existingTargetItem);
        }
        target.SetItem(dragged); 
    }
}
