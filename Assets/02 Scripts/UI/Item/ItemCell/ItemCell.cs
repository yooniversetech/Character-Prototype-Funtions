using UnityEngine;

[System.Serializable]
public class ItemCell : MonoBehaviour, ICell
{
    public IDraggable ContainedItem { get; private set; }
    public bool IsEmpty => ContainedItem == null;

    public bool CanAccept(IDraggable draggable)
    {
        // 아이템이면 아이템만 받게끔 로직 필요
        // 장비셀이면 장비타입만 받게끔 로직 필요
        return true;
    }

    public void SetItem(IDraggable draggable)
    {
        ContainedItem = draggable;
        draggable.CurrentCell = this; 

        ((MonoBehaviour)draggable).transform.SetParent(transform);
        ((MonoBehaviour)draggable).transform.localPosition = Vector3.zero;
    }

    public void ClearCell()
    {
        ContainedItem = null;
    }
}
