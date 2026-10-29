using TMPro;
using UnityEngine;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI Instance;

    [SerializeField] RectTransform tooltipPanel;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text descText;
    [SerializeField] TMP_Text effectText;

    [SerializeField] private float offset = 20f;

    private void Awake()
    {
        Instance = this;
        Hide();
    }

    private void Update()
    {
        if (!tooltipPanel.gameObject.activeSelf) return;
        tooltipPanel.position = Input.mousePosition;
        TooltipPanelUpdatePosition();
    }

    public void Show(ItemData item)
    {
        nameText.text = item.itemName;
        descText.text = item.itemDescription;
        effectText.text = $"허기 + {item.hunger}\n수분 + {item.thirst}";
        tooltipPanel.gameObject.SetActive(true);
    }

    public void Hide() => tooltipPanel.gameObject.SetActive(false);

    private void TooltipPanelUpdatePosition()
    {
        Vector2 mousePos = Input.mousePosition; // 마우스 위치값을 2차원 좌표값으로 대입
        float width = tooltipPanel.rect.width * tooltipPanel.lossyScale.x;   // 현재의 툴팁 판넬의 너비와 너비 비율을 곱해서 대입 (각기 다른 해상도에 대응하기 위함)
        float height = tooltipPanel.rect.height * tooltipPanel.lossyScale.y; // 현재의 툴팁 판넬의 높이와 높이 비율을 곱해서 대입 (각기 다른 해상도에 대응하기 위함)

        bool overRight = mousePos.x + offset + width > Screen.width; // 마우스 x좌표 값 + 20(여유분) + 너비 400(400 x 1) -> (FHD기준 너비 비율은 1)의 값이 현재 모니터의 와이드 보다 크면 true 를 작으면 false 를 대입
        bool overBottom = mousePos.y - offset - height < 0;          // 마우스 y좌표 값 - 20(여유분) - 높이 600(600 x 1) -> (FHD기준 너비 비율은 1)의 값이 0보다 작으면 true 아니면 false 대입

        tooltipPanel.pivot = new Vector2(overRight ? 1f : 0f, overBottom ? 0f : 1f); // 툴팁 패널의 피봇에 현재까지 계산한 bool 값을 기준으로 -> x 좌표는 true 라면 1을 false 라면 0을 반환 / y 좌표는 true 라면 0을 false 라면 1을 반환

        float dirX = overRight ? -offset : offset; // overRight 가 true 라면 -20을 false 라면 20을 대입
        float dirY = overBottom ? offset : -offset; // overBottom 가 true 라면 20을 false 라면 -20을 대입
        tooltipPanel.position = mousePos + new Vector2(dirX, dirY); // 툴팁 패널의 포지션 즉 위치값은 마우스 커서의 위치에서 x,y 좌표에서 20을 더해주거나 빼주는 식으로 뒤집힌 툴팁 패널의 여유분을 재갱신
    }
}
