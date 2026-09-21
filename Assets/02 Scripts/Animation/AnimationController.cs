using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private CharacterState characterState;

    private static readonly int SpeedHash = Animator.StringToHash("Walk");
    private static readonly int AttackHash = Animator.StringToHash("Attack");
    private static readonly int JumpHash = Animator.StringToHash("Jump");

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float speed = new Vector2(horizontal, vertical).magnitude;

        animator.SetFloat(SpeedHash, speed);

        AttackAnimation();
        JumpAnimation();
    }

    private void JumpAnimation()
    {
        if (Input.GetKeyDown(KeyCode.Space) && characterState.IsGrounded && !characterState.IsAttacking)
        {
            animator.SetTrigger(JumpHash);
        }
    }

    private void AttackAnimation()
    {
        if (Input.GetMouseButtonDown(0) && !characterState.IsAttacking)
        {
            animator.SetTrigger(AttackHash);
            characterState.IsAttacking = true;
        }
    }

    public void OnAttackAnimationEnd()
    {
        characterState.IsAttacking = false;
    }

    public void OnJumpAnimationEnd()
    {
        characterState.IsGrounded = true;
    }
}
