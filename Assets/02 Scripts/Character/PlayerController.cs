using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private CharacterMove characterMove;
    public Inventory inventory;
    //public EquipmentManager equipmentManager; // 아이템 장착 로직 (별도 클래스 추가 예정)
    //public WeaponUser weaponUser; // 무기 사용 로직 (별도 클래스 추가 예정)

    private void Awake()
    {
        inventory = new Inventory(36);
    }

    private void Start()
    {
        //equipmentManager = new EquipmentManager(inventory);
        //weaponUser = new WeaponUser(equipmentManager);
    }
}
