using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class InventoryUI : BaseUI
{
    [Header("컴포넌트 연결 대상")]
    [SerializeField] private PlayerController player;
    [SerializeField] private InventorySlotUI[] slotUIElements;

    [Header("설정")]
    [SerializeField] private KeyCode toggleKey = KeyCode.I;
    [SerializeField] private GameObject inventoryPanelRoot;

    private Inventory playerInventory;
    private bool isOpen = false;

    public event Action OnIventoryOpened;
    public event Action OnIventoryClosed;

    private void Start()
    {
        playerInventory = player.inventory;
        inventoryPanelRoot.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            if (isOpen) Close();
            else Open();
        }
    }

    private void Open()
    {
        isOpen = true;
        inventoryPanelRoot.SetActive(true);

        BindAllSlots();

        OnIventoryOpened?.Invoke();
    }

    private void Close()
    {
        isOpen = false;
        inventoryPanelRoot.SetActive(false);

        OnIventoryClosed?.Invoke();
    }

    private void BindAllSlots()
    {
        int count = Mathf.Min(slotUIElements.Length, playerInventory.Slots.Count);

        for (int i = 0; i < count; i++)
        {
            slotUIElements[i].Bind(playerInventory.Slots[i], playerInventory, i);
        }
    }
}
