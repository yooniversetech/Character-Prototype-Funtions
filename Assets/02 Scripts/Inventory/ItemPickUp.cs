using Unity.VisualScripting;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemData itemData;
    public int amount = 1;

    [Header("아이템 자석 이동 설정")]
    public float magnetRange = 3f;
    public float moveSpeed = 5f;

    private Transform player;
    private Inventory targetInventory;
    private bool isBeingFulled = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        //targetInventory = player.GetComponent<PlayerController>().inventory; // 플레이어 참조를 통해서 가져오는 방향으로 수정 예상
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < magnetRange)
        {
            isBeingFulled = true;
        }

        if (isBeingFulled)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        int leftover = targetInventory.AddItem(itemData, amount);

        if (leftover == 0)
        {
            Destroy(gameObject);
        }
        else
        {
            amount = leftover;
            isBeingFulled = false;
        }
    }
}
