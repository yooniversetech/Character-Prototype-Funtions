using UnityEngine;

public interface IUI
{
    bool isOpen { get; }
    void HideUI();
    void ShowUI();
}
