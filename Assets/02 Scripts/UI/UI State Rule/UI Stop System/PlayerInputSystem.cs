using UnityEngine;

public class PlayerInputSystem : MonoBehaviour
{
    private bool inputEnabled = true;

    public void SetEnabled(bool enabled)
    {
        inputEnabled = enabled;
    }

    public bool CanProcessInput()
    {
        return inputEnabled;
    }
}
