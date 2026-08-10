using UnityEngine;

public class InventoryTestSetup : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    [Header("테스트용 아이템 데이터 (인스팩터 연결)")]
    [SerializeField] private ItemData appleData;
    [SerializeField] private ItemData grapeData;

    private void Start()
    {
        Inventory inventory = player.inventory;

        inventory.DebugSetSlot(0, appleData, 10);
        inventory.DebugSetSlot(1, appleData, 1);
        inventory.DebugSetSlot(2, grapeData, 1);
    }
}
