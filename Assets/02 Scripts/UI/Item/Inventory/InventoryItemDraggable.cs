using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemDraggable : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [Header("UI Components")]
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    private Image itemIcon;
    public ItemSlot _SourceSlot;
    public Transform originalParent;
    public IItemData OriginalItemData;

    private int currentStack = 1;
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
        Debug.Log($"[OnEndDrag] : {CurrentStack}");

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (transform.parent != originalParent)
        {
            gameObject.SetActive(false);
            transform.localPosition = Vector3.zero;
        }
        //ClearDraggable();
    }

    public void ArrangeUI()
    {
        var canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup != null) GetComponent<CanvasGroup>().blocksRaycasts = true;

        if (CurrentStack <= 0)
        {
            FinishDragging();
            return;
        }

        transform.localPosition = Vector3.zero;
    }


    public void Initialize(IItemData data, int stack)
    {
        this.OriginalItemData = data;
        this.CurrentStack = stack;
    }

    public void ClearDraggable()
    {
        Debug.Log($"[ClearDraggable] : {CurrentStack}");

        this.gameObject.SetActive(false);
    }
    private void FinishDragging()
    {
        if (CurrentStack <= 0)
        {
            transform.SetParent(DropProcessManager.Instance.transform);
            gameObject.SetActive(false);
        }
    }
    public void Setup(IItemData data, int stack, Transform canvasTransform)
    {
        Debug.Log($"[Setup] : {CurrentStack}");

        _SourceSlot = GetComponentInParent<ItemSlot>();

        this.OriginalItemData = data;
        this.CurrentStack = stack;
        this.itemIcon.sprite = data.ItemIcon;
        
        this.gameObject.SetActive(true);

        transform.SetParent(canvasTransform, false);
        transform.SetAsLastSibling();

        transform.localPosition = Vector3.zero;
    }
}
