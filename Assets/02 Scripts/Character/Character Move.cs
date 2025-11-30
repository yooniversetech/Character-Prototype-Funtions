   using UnityEngine;

/// <summary>
/// 점프, WASD기반 움직임, 마우스 
/// </summary>
public class CharacterMove: MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;

    private float lastGroundTime;

    [Header("Settings")]
    [SerializeField] private float moveSpeed = 1f;   // 움직이는 속도
    [SerializeField] private float rotationSpeed = 150f; // 캐릭터 회전 속도 
    [SerializeField] private float gravity = -9.81f; // 중력 값 (지구 중력 기준)
    [SerializeField] private float jumpHeight = 2f;  // 점프 높이 값
    [SerializeField] private float groundedBufferTime = 0.15f; // 점프 버퍼 시간 값

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        Rotation();
        Jump();
        Move();
    }

    /// <summary>
    /// 캐릭터의 Position을 W,A,S,D 키로 조작하는 기능
    /// </summary>
    private void Move()
    {
        var X = Input.GetAxis("Horizontal");
        var Z = Input.GetAxis("Vertical");

        Vector3 moveDir = transform.right * X + transform.forward * Z;
        controller.Move(((moveDir * moveSpeed) + velocity) * Time.deltaTime);
    }

    /// <summary>
    /// 'Space Bar' 를 이용한 점프 기능
    /// </summary>
    private void Jump()
    {
        if (controller.isGrounded)
        {
            lastGroundTime = Time.time;

            if (velocity.y < 0)
            {
                velocity.y = -2f;
            }
        }

        if (Input.GetButtonDown("Jump") && (Time.time - lastGroundTime < groundedBufferTime))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            lastGroundTime = 0;
        }
            
        velocity.y += gravity * Time.deltaTime;
    }

    /// <summary>
    /// 마우스 이동시 캐릭터 회전(Y축 기준 회전) 기능
    /// </summary>
    private void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, mouseX * rotationSpeed * Time.deltaTime);
    }
}
