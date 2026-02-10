using UnityEngine;

public class TimeSystem : MonoBehaviour
{
    public void SetPaused(bool paused)
    {
        Time.timeScale = paused ? 0f : 1f;
    }
}
