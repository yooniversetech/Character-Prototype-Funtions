using UnityEngine;

public abstract class BaseUI : MonoBehaviour, IUIToggle, ICloseable
{
    // 사운드 클립 필드
    [SerializeField] protected AudioClip openSound;
    [SerializeField] protected AudioClip closeSound;

    // UI 상태 필드
    private bool isOpen;

    // UI 토글 메서드
    public virtual void Toggle()
    {
        if (isOpen) CloseUI();
        else OpenUI();
    }

    // UI 열기 메서드
    public virtual void OpenUI()
    {
        if (isOpen) return;

        isOpen = true;
        CursorManager.RegisterUIOpen();
    }

    // UI 닫기 메서드
    public virtual void CloseUI()
    {
        if (!isOpen) return;

        isOpen = false;
        CursorManager.RegisterUIClose();
    }
}
