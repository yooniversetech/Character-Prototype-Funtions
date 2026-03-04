using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class ItemCell : MonoBehaviour, ICell, IDropHandler, IDropRule, IItemSlot
{
    [SerializeField] private Image itemIcon;
    public IDraggable ContainedItem { get; private set; }
    public bool IsEmpty => ContainedItem == null;

    public IItemData currentItemData;

    private int currentStack = 1;

    public bool CanAccept(IDraggable draggable)
    {
        // 아이템이면 아이템만 받게끔 로직 필요
        // 장비셀이면 장비타입만 받게끔 로직 필요
        return true;
    }

    /// <summary>
    /// 테스트 및 데모용으로 간단히 구현한 메서드입니다.
    /// 실제 게임에서는 아이템 타입, 스택 가능 여부 등을 고려하여 더 복잡한 로직이 필요할 수 있습니다.
    /// </summary>
    /// <param name="draggable"></param>
    public void SetItem(IDraggable draggable)
    {
        ContainedItem = draggable;

        if (draggable != null)
        {
            if (itemIcon != null)
            {
                itemIcon.sprite = draggable.Data.ItemIcon;
                itemIcon.gameObject.SetActive(true);

                if (!itemIcon.gameObject.TryGetComponent<InventoryItemDraggable>(out var draggingScript))
                {
                    itemIcon.gameObject.AddComponent<InventoryItemDraggable>();
                }
            }
        }
        else
        {
            if (itemIcon != null)
            {
                itemIcon.gameObject.SetActive(false);
            }
        }
    }

    public void ClearCell()
    {
        ContainedItem = null;
    }

    public void OnDrop(PointerEventData eventData)
    {
        GameObject draggedObject = eventData.pointerDrag;
        if (draggedObject == null) return;

        var draggedItem = draggedObject.GetComponent<InventoryItemDraggable>();
        if (draggedItem == null) return;

        if (this.currentItemData != null)
        {
            InventoryItemDraggable existingItem = GetComponentInChildren<InventoryItemDraggable>();
            
            if (existingItem != null)
            {
                existingItem.transform.SetParent(draggedItem.originalParent);
                existingItem.transform.localPosition = Vector3.zero;

                var originCell = draggedItem.originalParent.GetComponent<ItemCell>();
                originCell.currentItemData = existingItem.itemData;
            }
        }

        ExecuteDrop(draggedItem);
    }

    private void ExecuteDrop(InventoryItemDraggable draggedItem)
    {
        draggedItem.transform.SetParent(this.transform);
        draggedItem.transform.localPosition = Vector3.zero;
        this.currentItemData = draggedItem.itemData;
    }

    public bool IsMatch(ItemCell targetCell, InventoryItemDraggable draggableItem)
    {
        if (targetCell == null) return false;
        if (targetCell.currentItemData.ItemID != draggableItem.itemData.ItemID) return false;
        if (targetCell.currentItemData.CurrentStack >= targetCell.currentItemData.MaxStack) return false;

        return true;
    }

    public void Execute(ItemCell targetCell, InventoryItemDraggable draggableItem)
    {
        
    }
}
