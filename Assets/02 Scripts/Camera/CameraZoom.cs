using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform target;        // 카메라가 바라볼 캐릭터 (플레이어)

    [Header("Setting")]
    [SerializeField] private float zoomSpeed = 2.0f;  // 스크롤 속도
    [SerializeField] private float currentDis = 5.0f; // 현제 캐릭터와 카메라의 거리
    [SerializeField] private float minDis = 2.0f;     // 카메라 줌 최소 거리
    [SerializeField] private float maxDis = 10.0f;    // 카메라 줌 최대 거리
    [SerializeField] private float smoothTime = 1f; // 카메라 줌 속도

    [SerializeField] private float currentVelocity;

    private void LateUpdate()
    {
        ScrollZoom();
    }
    
    /// <summary>
    /// 휠 스크롤시 카메라 줌인 및 줌아웃 (최소거리 : 2 | 최대거리 : 10)
    /// </summary>
    private void ScrollZoom()
    {
        if (!target) return;

        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput != 0)
        {
            currentDis -= scrollInput * zoomSpeed;
        }

        currentDis = Mathf.Clamp(currentDis, minDis, maxDis);

        Vector3 targetPosition = target.position - (transform.forward * currentDis);
        transform.position = targetPosition;
    }
}