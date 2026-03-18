using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemDraggable : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [Header("UI Components")]
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    private Image itemIcon;
    public static ItemCell _SourceCell;
    public Transform originalParent;
    private int currentStack = 1;

    private IItemData OriginalItemData;

    public int CurrentStack
    {
        get => currentStack;
        set => currentStack = value;
    }

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        canvasGroup = GetComponentInChildren<CanvasGroup>();
    }

    private void Start()
    {
        itemIcon = GetComponent<Image>();  
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("드래그 종료");

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (transform.parent != originalParent)
        {
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
        }
        ClearDraggable();
    }

    public void ArrangeUI()
    {
        if (CurrentStack <= 0)
        {
            FinishDragging();
            return;
        }

        transform.localPosition = Vector3.zero;

        var canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup != null) GetComponent<CanvasGroup>().blocksRaycasts = true;
    }


    public void Initialize(IItemData data, int stack)
    {
        this.OriginalItemData = data;
        this.CurrentStack = stack;

        Debug.Log($"{OriginalItemData.ItemID} 아이템 드래그 스크립트가 초기화되었습니다.");
    }

    public void ClearDraggable()
    {
        this.OriginalItemData = null;
        this.CurrentStack = 0;
    }
    private void FinishDragging()
    {
        Destroy(gameObject);
    }

    public void Setup(IItemData data, int stack)
    {
        this.OriginalItemData = data;
        this.CurrentStack = stack;

        this.itemIcon.sprite = data.ItemIcon;
    }
}
