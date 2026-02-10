using UnityEngine;

[CreateAssetMenu(menuName = "UI/UI State Rule")]
public class UIStateRule : ScriptableObject
{
    public bool allowPlayerInput;
    public bool pauseTime;
    public bool lockCamera;
}
