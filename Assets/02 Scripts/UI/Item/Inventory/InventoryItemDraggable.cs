using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI Components")]
    private CanvasGroup canvasGroup;
    private Canvas canvas;

    public Transform originalParent;
    public IItemData itemData { get; private set; }
    public ItemData existingItem { get; private set; }
    private int currentStack = 1;
    public int CurrentStack
    {
        get => currentStack;
        set => currentStack = value;
    }

    private void Awake()
    {
        canvasGroup = GetComponentInChildren<CanvasGroup>();  
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("드래그 시작");

        //canvasGroup.blocksRaycasts = false;

        originalParent = transform.parent;
        transform.SetParent(canvas.transform);
        transform.SetAsLastSibling();

        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
        Debug.Log("아이템 이동중");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("드래그 종료");

        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        if (transform.parent == canvas.transform)
        {
            transform.SetParent(originalParent);
            transform.localPosition = Vector3.zero;
        }
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

    private void FinishDragging()
    {
        Destroy(gameObject);
    }

    public void Initialize(IItemData data, int stack)
    {
        this.itemData = data;
        this.CurrentStack = stack;

        Debug.Log($"{itemData.ItemID} 아이템 드래그 스크립트가 초기화되었습니다.");
    }
}
