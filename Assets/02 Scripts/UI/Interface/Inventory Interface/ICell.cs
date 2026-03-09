using UnityEngine;

public interface ICell
{
    IDraggable ContainedItem { get; }
    bool IsEmpty { get; }

    // 이 셀이 해당 데이터를 받을 수 있는지 확인
    bool CanAccept(IDraggable draggable);

    // 데이터를 슬롯에 등록 / 해제
    void SetItem(IItemData data, int stack);
    void ClearCell();
}
