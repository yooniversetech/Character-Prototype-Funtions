using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class ItemCell : MonoBehaviour, ICell, IDropHandler, IItemSlot
{
    // --- [1. Inspector / UI References] ---
    [SerializeField] private Image itemIcon;

    // --- [2. Internal States / Data] ---
    private int currentStack = 1;
    public IItemData currentItemData;

    // --- [3. Interface Implementations (IItemSlot)] ---
    public IItemData ItemData => currentItemData;
    public int CurrentStack { get => currentStack;set => currentStack = value; }
    public bool IsEmpty => ContainedItem == null;
    public IDraggable ContainedItem { get; private set; }


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
    /// <param name="data"></param>
    /// <param name="stack"></param>
    public void SetItem(IItemData data, int stack)
    {
        this.currentItemData = data;
        this.CurrentStack = stack;

        if (data != null && stack > 0)
        {
            if (itemIcon != null)
            {
                itemIcon.sprite = data.ItemIcon;
                itemIcon.gameObject.SetActive(true);

                if (itemIcon.TryGetComponent<InventoryItemDraggable>(out var draggingScript))
                {
                    draggingScript.Initialize(data, stack);
                }
            }
        }
        else
        {
            ClearCell();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        var draggedItem = eventData.pointerDrag?.GetComponent<InventoryItemDraggable>();
        if (draggedItem == null) return;

        DropProcessManager.Instance.ProcessDrop(this, draggedItem);
        draggedItem.ArrangeUI();
    }
    
    public void ClearCell()
    {
        currentItemData = null;
        CurrentStack = 0;
        if (itemIcon != null) itemIcon.gameObject.SetActive(false);
    }
}