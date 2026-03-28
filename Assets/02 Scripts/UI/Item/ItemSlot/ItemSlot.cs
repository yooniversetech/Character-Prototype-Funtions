using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class ItemSlot : MonoBehaviour, ICell, IDropHandler, IItemSlot, IBeginDragHandler
{
    // --- [1. Inspector / UI References] ---
    [SerializeField] private Image itemIcon;
    private Canvas canvas;

    // --- [2. Internal States / Data] ---
    [SerializeField] private int currentStack = 0;
    public IItemData currentItemData;
    public Transform originalParent;


    // --- [3. Interface Implementations (IItemSlot)] ---
    public IItemData ItemData => currentItemData;
    public int CurrentStack { get => currentStack; set => currentStack = value; }
    public bool IsEmpty => ContainedItem == null;
    public IDraggable ContainedItem { get; private set; }

    private void Start()
    {
        canvas = GetComponent<Canvas>();

        UpdateUI();
    }
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
        if (true)
        {
            this.currentItemData = data;
            this.CurrentStack = stack;
        }
        if (data != null)
        {
            itemIcon.sprite = data.ItemIcon;
            itemIcon.gameObject.SetActive(true);
        }

        //if (data != null && stack > 0)
        //{
        //    if (itemIcon != null)
        //    {
        //        itemIcon.sprite = data.ItemIcon;
        //        itemIcon.gameObject.SetActive(true);

        //        if (itemIcon.TryGetComponent<InventoryItemDraggable>(out var draggingScript))
        //        {
        //            draggingScript.Initialize(data, stack);
        //        }
        //    }
        //}
        //else
        //{
        //    ClearSlot();
        //    AssignData(data, stack);
        //}
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"[OnBeginDrag] : 1");

        if (currentItemData == null) return;
        Debug.Log($"[OnBeginDrag] : 2");

        originalParent = transform.parent;
      
        var draggable = DropProcessManager.Instance.DraggableItem;

        draggable.Setup(currentItemData, CurrentStack, DropProcessManager.Instance.MainCanvasTransform);

        draggable.gameObject.SetActive(true);
        itemIcon.gameObject.SetActive(false);
    }

    public void OnDrop(PointerEventData eventData)
    {
        var draggedItem = eventData.pointerDrag?.GetComponent<InventoryItemDraggable>();
        if (draggedItem == null) return;

        DropProcessManager.Instance.ProcessDrop(this, draggedItem);
        draggedItem.ArrangeUI();
    }
      
    /// <summary>
    /// 드래그가 끝나고 아이템이 셀에서 제거될 때 호출되는 메서드입니다.
    /// </summary>
    public void ClearSlot()
    {
        this.currentItemData = null;
        this.CurrentStack = 0;
        UpdateUI();
    }

    /// <summary>
    /// 각 슬롯에 데이터 할당 기능
    /// </summary>
    /// <param name="data"></param>
    /// <param name="stack"></param>
    public void AssignData(IItemData data, int stack)
    {
        this.currentItemData = data;
        this.CurrentStack = stack;
        Debug.Log($"[AssignData] : 3"); 

        UpdateUI();

    }

    /// <summary>
    /// 아이콘만 바뀐 데이터 상태를 토대로 업데이트하는 메서드입니다.
    /// </summary>
    public void UpdateUI()
    {
        if (currentItemData != null)
        {
            itemIcon.sprite = currentItemData.ItemIcon;
            itemIcon.gameObject.SetActive(true);
        }
        else
        {
            itemIcon.sprite = null;
            itemIcon.gameObject.SetActive(false);
        }
    }
}