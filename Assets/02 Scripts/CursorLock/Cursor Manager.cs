using System;
using System.Collections;
using UnityEngine;

public class CursorManager : MonoBehaviour 
{
    public static event Action<bool> OnUIModeChanged; // UI 모드 변경 이벤트
    private static int _openUICount = 0; // 열린 UI의 개수를 추적하는 정적 변수

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        UpdateCursorState();
    }
    public static void RegisterUIOpen()
    {
        _openUICount++;
        UpdateCursorState();
    }

    public static void RegisterUIClose()
    {
        _openUICount = Mathf.Max(0, _openUICount - 1);
        UpdateCursorState();
    }

    public static void UpdateCursorState()
    {
        bool isUIActive = _openUICount > 0;

        Cursor.lockState = isUIActive ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isUIActive;

        OnUIModeChanged?.Invoke(isUIActive);
    }
}
