using UnityEngine;

public class ItemFloatEffect : MonoBehaviour
{
    [SerializeField] private float bobSpeed = 2f;
    [SerializeField] private float bobHeight = 0.1f;
    [SerializeField] private float defualtRotSpeed = 55f;  // 일반 아이템 회전속도
    //[SerializeField] private float artRotSpeed = 20f;   // 추후 보스, 아티팩트 회전 속도는 다르게 제어

    private Vector3 startPos; // 아이템의 시작 위치를 위한 필드

    void Start()
    {
        startPos = transform.position; // startPos에 이 클래스가 컴포넌트로 추가된 오브젝트의 위치값을 대입
    }

   
    void Update()
    {
        ItemBopping();
        ItemRotate();
    }

    /// <summary>
    /// 아이템 오브젝트 위,아래 진자운동 연출
    /// </summary>
    private void ItemBopping()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    /// <summary>
    /// 아이템 오브젝트 회전 연출
    /// </summary>
    private void ItemRotate()
    {
        transform.Rotate(0f, defualtRotSpeed * Time.deltaTime, 0f, Space.World);
    }


    /// <summary>
    /// 현재 아이템의 위치를 변경하는 함수
    /// </summary>
    /// <param name="pos"></param> 
    public void ResetOrigin(Vector3 pos) 
    {
        startPos = pos; // 전달 받은 인자값을 startPos에 대입하므로써 현재 아이템의 위치를 갱신
    }
}
