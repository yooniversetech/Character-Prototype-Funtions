using UnityEngine;

public class ItemFloatEffect : MonoBehaviour
{
    [SerializeField] private float bobSpeed = 2f;
    [SerializeField] private float bobHeight = 0.1f;
    [SerializeField] private float defortRotSpeed = 55f;  // 일반 아이템 회전속도
    //[SerializeField] private float artRotSpeed = 20f;   // 추후 보스, 아티팩트 회전 속도는 다르게 제어

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

   
    void Update()
    {
        ItemBopping();
        ItemRotate();
    }

    private void ItemBopping()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    private void ItemRotate()
    {
        transform.Rotate(0f, defortRotSpeed * Time.deltaTime, 0f, Space.World);
    }

    public void ResetOrigin(Vector3 pos)
    {
        startPos = pos;
    }
}
