using UnityEngine;

public class CameraRigController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerBody; // 캐릭터의 목이나 머리위치

    [Header("Settings")]
    private float mouseSensitivity = 150f;         // 마우스 감도
    private float moveSmoothing = 0f;                  // 현제 위아래 각도

    [Header("Debugging")]
    private float currentYRotation = 0f;

    private void Start()
    {
        FindTarget();
        currentYRotation = transform.eulerAngles.y;  // ?
    }

    private void LateUpdate()
    {
        HandlePosition();
        HandleRotation();
    }

    private void FindTarget()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            playerBody = playerObj.transform;
    }

    private void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        currentYRotation += mouseX * mouseSensitivity * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f, currentYRotation, 0f);
    }

    private void HandlePosition()
    {
        if (moveSmoothing == 0)
        {
            transform.position = playerBody.position;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, playerBody.position, moveSmoothing * Time.deltaTime); 
        }
    }
}
