using Unity.VisualScripting;
using UnityEngine;

public class CharacterMoveByRigidBody : MonoBehaviour
{
    private Vector3 velocity;
    private float moveSpeed = 1f;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        velocity.x = h * moveSpeed;
        velocity.y = v * moveSpeed;
    }
}
