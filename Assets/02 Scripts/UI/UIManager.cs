using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private Dictionary<System.Type, IUI> uiMap;

    private void Awake()
    {
        uiMap = new Dictionary<System.Type, IUI>();

        foreach (var ui in GetComponentsInChildren<IUI>(true))
        {
            uiMap.Add(ui.GetType(), ui);
        }
    }

    public void Show<T>() where T : IUI
    {
        uiMap[typeof(T)].ShowUI();
    }

    public void Hide<T>() where T : IUI
    {
        uiMap[typeof(T)].HideUI();
    }

    [SerializeField] private InventoryUI inventoryUI;

    private void Start()
    {
        inventoryUI.HideUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUI.isOpen)
                inventoryUI.HideUI();
            else
                inventoryUI.ShowUI();
        }
    }
}
