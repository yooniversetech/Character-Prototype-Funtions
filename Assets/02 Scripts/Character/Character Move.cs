using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 점프, WASD기반 움직임, 마우스 사용시 회전
/// </summary>
public class CharacterMove: MonoBehaviour
{
    #region 필드 
    private CharacterController controller;
    private Vector3 velocity;

    [Header("Referance")]
    [SerializeField] private Transform cameraTransform;

    [Header("Settings")]
    [SerializeField] private float moveSpeed = 1f;   // 움직이는 속도
    [SerializeField] private float turnSmoothTime = 0.1f; // 캐릭터 회전 속도 
    [SerializeField] private float gravity = -9.81f; // 중력 값 (지구 중력 기준)
    [SerializeField] private float jumpHeight = 2f;  // 점프 높이 값
    [SerializeField] private float groundedBufferTime = 0.15f; // 점프 버퍼 시간 값

    private float lastGroundTime;
    private float turnSmoothVelocity;
    #endregion

    #region 유니티 콜백함수
    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    private void Update()
    {
        CalcGravityAndJump();
        Move();
        controller.Move(velocity * Time.deltaTime);
    }
    #endregion

    #region 현제 스크립트에서 사용할 함수
    /// <summary>
    /// 캐릭터의 Position을 W,A,S,D 키로 조작하는 기능
    /// </summary>
    private void Move()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(h, 0, v).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            targetAngle += cameraTransform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
        }
    }

    private void CalcGravityAndJump()
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
    #endregion
}
