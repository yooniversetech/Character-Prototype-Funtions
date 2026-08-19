using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour 
{
    public static event Action<bool> OnUIModeChanged; // UI 모드 변경 이벤트
    private static readonly HashSet<object> _openUIs = new HashSet<object>();

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        UpdateCursorState();
    }
    public static void RegisterUIOpen(object ui)
    {
        _openUIs.Add(ui);
        UpdateCursorState();
    }

    public static void RegisterUIClose(object ui)
    {
        _openUIs.Remove(ui);
        UpdateCursorState();
    }

    public static void UpdateCursorState()
    {
        bool isUIActive = _openUIs.Count > 0;

        Cursor.lockState = isUIActive ?
            CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isUIActive;

        OnUIModeChanged?.Invoke(isUIActive);
    }
}
