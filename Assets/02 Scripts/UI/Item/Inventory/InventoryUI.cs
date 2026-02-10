using UnityEngine;

public class InventoryUI : MonoBehaviour, IUI
{
    public bool isOpen => gameObject.activeSelf;
    
    public void ShowUI()
    {
        gameObject.SetActive(true);
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }
}
