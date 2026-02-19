using System;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public static event Action<bool> OnUIModeChanged;

    private void Start()
    {
        SetUIMode(false);
    }

    public static void SetUIMode(bool isUIOpen)
    {
        OnUIModeChanged?.Invoke(isUIOpen);

        Cursor.lockState = isUIOpen ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isUIOpen;
    }
}
