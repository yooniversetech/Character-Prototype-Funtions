using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemDraggable : MonoBehaviour
{
    [Header("UI Components")]
    public CanvasGroup canvasGroup;

    [SerializeField] private Image itemIcon;
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
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        itemIcon = GetComponent<Image>();
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
    public void Setup(IItemData data, int stack, Transform canvasTransform, ItemSlot source)
    {
        this._SourceSlot = source;
        this.OriginalItemData = data;
        this.CurrentStack = stack;
        this.gameObject.SetActive(true);

        transform.SetParent(canvasTransform, false);
        transform.SetAsLastSibling();

        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        this.canvasGroup.blocksRaycasts = false;
        this.canvasGroup.alpha = 0.6f;

        this.itemIcon.sprite = data.ItemIcon;
    }
}

