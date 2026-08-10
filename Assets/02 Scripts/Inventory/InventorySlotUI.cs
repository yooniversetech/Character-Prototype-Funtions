using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    [Header("UI 참조 (인스펙터에서 연결 필요)")]
    [SerializeField] private Image itemIconIamge;
    [SerializeField] private TextMeshProUGUI itemCountText;

    private ItemSlot boundSlot;

    private Inventory owningInventory;
    private int slotIndex;

    private static InventorySlotUI draggedFromSlot;
    private static GameObject dragIconInstance;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="slot"></param>
    /// <param name="inventory"></param>
    /// <param name="index"></param>
    public void Bind(ItemSlot slot, Inventory inventory, int index)
    {
        if (boundSlot != null)
            boundSlot.OnSlotChanged -= Refresh;

        boundSlot = slot;
        owningInventory = inventory;
        slotIndex = index;

        boundSlot.OnSlotChanged += Refresh;
        Refresh();
    }
    /// <summary>
    /// 
    /// </summary>
    private void Refresh()
    {
        if (boundSlot == null || boundSlot.IsEmpty)
        {
            itemIconIamge.enabled = false;
            itemCountText.enabled = false;
            itemCountText.text = "";
            return;
        }

        itemIconIamge.enabled = true;
        itemIconIamge.sprite = boundSlot.ItemData.itemIcon;

        bool showCount = boundSlot.StackCount > 1;
        itemCountText.enabled = showCount;
        itemCountText.text = boundSlot.StackCount > 1 ? boundSlot.StackCount.ToString() : "";
    }
    /// <summary>
    /// 
    /// </summary>
    private void OnDestroy()
    {
        if (boundSlot != null)
            boundSlot.OnSlotChanged -= Refresh;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (boundSlot == null || boundSlot.IsEmpty) return;

        draggedFromSlot = this;

        itemIconIamge.enabled = false;
        itemCountText.enabled = false;

        dragIconInstance = new GameObject("DragIcon");
        dragIconInstance.transform.SetParent(transform.root);
        var img = dragIconInstance.AddComponent<Image>();
        img.sprite = boundSlot.ItemData.itemIcon;
        img.raycastTarget = false;
        img.rectTransform.sizeDelta = new Vector2(50, 50);
    }
    #region
    public void OnDrag(PointerEventData eventData)
    {
        if (dragIconInstance != null)
            dragIconInstance.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragIconInstance != null)
            Destroy(dragIconInstance);

        draggedFromSlot = null;

        Refresh();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (draggedFromSlot == null || draggedFromSlot == this) return;

        HandleDrop(draggedFromSlot);
    }

    private void HandleDrop(InventorySlotUI fromSlotUI)
    {
        ItemSlot fromSlot = fromSlotUI.boundSlot;
        ItemSlot toSlot = this.boundSlot;

        if (toSlot.IsEmpty)
        {
            int leftover = toSlot.AddItem(fromSlot.ItemData, fromSlot.StackCount);
            fromSlot.RemoveItem(fromSlot.StackCount - leftover);
        }
        else if (toSlot.ItemData == fromSlot.ItemData)
        {
            int leftover = toSlot.AddItem(fromSlot.ItemData, fromSlot.StackCount);
            fromSlot.RemoveItem(fromSlot.StackCount - leftover);
        }
        else
        {
            fromSlot.SwapWith(toSlot);
        }
    }
    #endregion
}
