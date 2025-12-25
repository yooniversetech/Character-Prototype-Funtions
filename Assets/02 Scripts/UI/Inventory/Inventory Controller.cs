using UnityEngine;

public class InventoryController : MonoBehaviour
{
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
