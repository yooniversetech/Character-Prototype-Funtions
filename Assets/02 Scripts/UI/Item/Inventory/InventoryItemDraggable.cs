using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemDraggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [Header("UI Components")]
    [SerializeField] private CanvasGroup canvasGroup;

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
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();

        canvas = GetComponentInParent<Canvas>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("드래그 시작");

        canvasGroup.blocksRaycasts = false;

        originalParent = transform.parent;
        transform.SetParent(canvas.transform);
        transform.SetAsLastSibling();

        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
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
}
