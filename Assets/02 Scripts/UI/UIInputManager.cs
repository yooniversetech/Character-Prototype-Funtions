using UnityEngine;

public class UIInputManager : MonoBehaviour
{
    // 인벤토리 UI 오브젝트 참조
    [SerializeField] private GameObject InventoryUI;

    // BaseUI 상속 받는 UI 클래스들 참조
    [SerializeField] private BaseUI inventory;
    //[SerializeField] private BaseUI map;

    private void Update()
    {
        CheckUIInputs();
    }

    // 여기서 다른 UI 관련 입력들을 모두 처리 (가독성을 위함)
    private void CheckUIInputs()
    {
        if (Input.GetKeyDown(KeyCode.I)) inventory.Toggle();
    }
}
