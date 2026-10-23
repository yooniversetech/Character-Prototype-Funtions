using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance;

    [SerializeField] RectTransform tooltipPanel;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text descText;
    [SerializeField] TMP_Text effectText;

    private void Awake()
    {
        Instance = this;
        Hide();
        
    }

    private void Update()
    {
        if (!tooltipPanel.gameObject.activeSelf) return;
        tooltipPanel.position = Input.mousePosition;
    }

    public void Show(ItemData item)
    {
        nameText.text = item.itemName;
        descText.text = item.itemDescription;
        effectText.text = $"허기 + {item.hunger}\n수분 + {item.thirst}";
        tooltipPanel.gameObject.SetActive(true);
    }

    public void Hide() => tooltipPanel.gameObject.SetActive(false);
}
