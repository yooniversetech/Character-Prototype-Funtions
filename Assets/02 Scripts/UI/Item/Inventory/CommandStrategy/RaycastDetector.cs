using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class RaycastDetector : MonoBehaviour
{
    void Update()
    {
        // 마우스 왼쪽 버튼을 눌렀을 때만 검사
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);

            if (results.Count > 0)
            {
                foreach (var go in results)
                {
                    Debug.Log($"<color=yellow>[검거 완료]</color> 마우스에 걸린 녀석: <b>{go.gameObject.name}</b>");
                }
            }
            else
            {
                Debug.Log("<color=red>[경고]</color> 아무것도 잡히지 않습니다. Canvas의 GraphicRaycaster를 확인하세요!");
            }
        }
    }
}
