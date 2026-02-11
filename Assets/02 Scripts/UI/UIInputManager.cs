using UnityEngine;

public class UIInputManager : MonoBehaviour
{
    [SerializeField] private GameObject InventoryUI;

    private void Update()
    {
        CheckUIInputs();
    }

    private void CheckUIInputs()
    {
        if (Input.GetKeyDown(KeyCode.I)) ToggleInventory();
        // 여기서 다른 UI 관련 입력들을 처리하는 로직 넣어두기 (가독성을 위함)
    }
    private void ToggleInventory()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (InventoryUI.activeSelf)
            {
                InventoryUI.SetActive(false);
            }
            else
            {
                InventoryUI.SetActive(true);
            }
        }
    }

    private void ToggleMap()
    {

    }

    private void ToggleSkill()
    {

    }

    private void ToggleSpecialAbility()
    {

    }

    private void ToggleMainMenu()
    {

    }
}
