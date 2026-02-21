using UnityEngine;

public class CameraPivotController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerBody; // 캐릭터의 목이나 머리위치

    [Header("Settings")]
    private float mouseSensitivity = 150f;         // 마우스 감도
    private float xRotation = 0f;                  // 현제 위아래 각도

    private bool _canRotate = true;

    private void Awake()
    {
        CursorManager.OnUIModeChanged += HandleUIMode;
        HandleUIMode(Cursor.lockState == CursorLockMode.None);
    }

    private void Update()
    {
        CameraYRotation();
    }

    private void CameraYRotation()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void HandleUIMode(bool isUIOpen)
    {
        _canRotate = !isUIOpen;
    }
}
