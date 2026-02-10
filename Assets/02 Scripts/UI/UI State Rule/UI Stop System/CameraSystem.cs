using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    private bool locked;

    public void SetLocked(bool isLocked)
    {
        locked = isLocked;
    }

    private void LateUpdate()
    {
        if (locked) return;
    }
}
