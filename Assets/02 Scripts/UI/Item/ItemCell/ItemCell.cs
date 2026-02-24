using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class ItemCell : MonoBehaviour, ICell, IDropHandler
{
    [SerializeField] private Image itemIcon;
    public IDraggable ContainedItem { get; private set; }
    public bool IsEmpty => ContainedItem == null;

    public IItemData currentItemData;

    public bool CanAccept(IDraggable draggable)
    {
        // 아이템이면 아이템만 받게끔 로직 필요
        // 장비셀이면 장비타입만 받게끔 로직 필요
        return true;
    }

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

        if (draggedObject != null)
        {
            var draggedItem = draggedObject.GetComponent<InventoryItemDraggable>();

            if (draggedItem != null)
            {
                ExecuteDrop(draggedItem);
            }
        }
    }

    private void ExecuteDrop(InventoryItemDraggable draggedItem)
    {
        draggedItem.transform.SetParent(this.transform);
        draggedItem.transform.localPosition = Vector3.zero;
        this.currentItemData = draggedItem.itemData;
    }
}
