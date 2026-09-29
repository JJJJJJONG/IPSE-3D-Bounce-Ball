using UnityEngine;

public class ObjectBounce : MonoBehaviour
{
    public float rotatespeed = 10;
    public float amplitude = 1;
    Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        //회전
        transform.Rotate(new Vector3(0,10,0) * rotatespeed * Time.deltaTime, Space.Self);

        // 위아래로 움직임
        //예전에는 매 프레임 조금씩 더해서 프레임레이트에 따라 높이가 달라지고 위치가 점점 밀렸음 -> 시작 위치 기준으로 계산
        Vector3 position = startPosition;
        position.y += Mathf.Sin(Time.time * 3) * 0.1f * amplitude;
        transform.position = position;
    }
}
