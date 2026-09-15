using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private static readonly int SpeedHash = Animator.StringToHash("Walk");
    private static readonly int AttackHash = Animator.StringToHash("Attack");

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        float speed = new Vector2(horizontal, vertical).magnitude;

        animator.SetFloat(SpeedHash, speed);

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger(AttackHash);
        }
    }
}
