using UnityEngine;

public abstract class BaseUI : MonoBehaviour, IUIToggle
{
    // 사운드 클립 필드
    [SerializeField] protected AudioClip openSound;
    [SerializeField] protected AudioClip closeSound;

    // UI 상태 필드
    private bool isOpen;

    // UI 토글 메서드
    public virtual void Toggle()
    {
        if (isOpen) Close();
        else Open();
    }

    // UI 열기 메서드
    public virtual void Open()
    {
        if (isOpen) return;

        isOpen = true;
        gameObject.SetActive(true);
    }

    // UI 닫기 메서드
    public virtual void Close()
    {
        if (!isOpen) return;

        isOpen = false;
        gameObject.SetActive(false);
    }
}
