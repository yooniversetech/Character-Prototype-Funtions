using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController characterController;
    private CharacterState characterState;

    private bool isAttacking;
    private bool isGrounded;
    private static readonly int SpeedHash = Animator.StringToHash("Walk");
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int JumpHash = Animator.StringToHash("Jump");

    private void Update()
    {
        isGrounded = characterController.isGrounded;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float speed = new Vector2(horizontal, vertical).magnitude;

        animator.SetFloat(SpeedHash, speed);

        AttackAnimation();
        JumpAnimation();
    }

    private void JumpAnimation()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isAttacking)
        {
            animator.SetTrigger(JumpHash);
        }
    }

    private void AttackAnimation()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            animator.SetTrigger(AttackHash);
            isAttacking = true;
        }
    }

    private void OnAttackAnimationEnd()
    {
        isAttacking = false;
    }
}
