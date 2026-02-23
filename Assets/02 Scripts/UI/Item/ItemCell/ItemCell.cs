using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ItemCell : MonoBehaviour, ICell
{
    [SerializeField] private Image itemIcon;
    public IDraggable ContainedItem { get; private set; }
    public bool IsEmpty => ContainedItem == null;

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
                itemIcon.sprite = draggable.Data.IconSprite;
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
}
