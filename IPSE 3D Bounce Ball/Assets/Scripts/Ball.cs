using UnityEngine;

public class Ball : MonoBehaviour
{
    Rigidbody rigid;
    SphereCollider sphere;

    bool isFlying = false;
    Vector3 flyDirection = Vector3.forward;

    public float FlyPower;
    public float JumpPower;
    public float MaxSpeed;
    public int StarCount;
    public float movePower = 5f;
    public float flySpeed = 10f;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        sphere = GetComponent<SphereCollider>();
    }

    void Update()
    {
        //날아가는 중에 방향키를 새로 누르면 비행을 멈추고 떨어짐
        if (isFlying && (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")))
        {
            StopFlying();
        }
    }

    void FixedUpdate() // W A S D로 움직이기
    {
        //날아가는 중에는 중력과 입력을 무시하고 일직선으로 이동
        //(예전에는 MaxSpeed로 잘려서 느려지고, isFlying이 풀리지 않아 공중에 영원히 떠 있었음)
        if (isFlying)
        {
            rigid.velocity = flyDirection * flySpeed;
            return;
        }

        float h = Input.GetAxisRaw("Horizontal"); //수평방향(왼쪽, 오른쪽)
        float v = Input.GetAxisRaw("Vertical");   //수직방향(앞, 뒤)

        //카메라가 바라보는 수평 방향 기준으로 이동
        //(카메라가 위에서 내려다볼수록 앞뒤 이동이 느려지던 문제 수정: 카메라의 y축 회전만 사용)
        Quaternion yaw = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
        Vector3 direction = yaw * new Vector3(h, 0, v).normalized;

        rigid.AddForce(direction * movePower, ForceMode.Force);

        Vector2 horizontalVelocity = new Vector2(rigid.velocity.x, rigid.velocity.z);

        if (horizontalVelocity.magnitude > MaxSpeed)
        {
            horizontalVelocity = horizontalVelocity.normalized * MaxSpeed;
            rigid.velocity = new Vector3(horizontalVelocity.x, rigid.velocity.y, horizontalVelocity.y);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //날아가다가 무언가에 부딪히면 비행 종료 후 평소처럼 처리
        if (isFlying)
        {
            StopFlying();
        }

        if (collision.gameObject.tag == "Floor")
        {
            Bounce(JumpPower);
            AudioManager.Instance.PlaySound("Bounce");
        }

        //윗면을 밟았을 때만 블록 효과 발동
        //(예전에는 접촉점 개수만큼 반복해서 힘이 두세 번 더해져 가끔 훨씬 높이 튀었음)
        else if (collision.gameObject.tag == "JumpBlock" && IsLandedOnTop(collision))
        {
            Bounce(2 * JumpPower);
            AudioManager.Instance.PlaySound("ItemBlock");
        }

        else if (collision.gameObject.tag == "OneStepBlock" && IsLandedOnTop(collision))
        {
            Bounce(JumpPower);
            AudioManager.Instance.PlaySound("Bounce");
            collision.gameObject.SetActive(false);
        }

        else if (collision.gameObject.tag == "FlyBlock" && IsLandedOnTop(collision))
        {
            StartFlying(collision.collider.bounds);
            AudioManager.Instance.PlaySound("ItemBlock");
        }
    }

    bool IsLandedOnTop(Collision collision)
    {
        foreach (ContactPoint point in collision.contacts)
        {
            if (Vector3.Dot(point.normal, Vector3.up) > 0.5f)
                return true;
        }
        return false;
    }

    //떨어지던 속도가 남아 있으면 튀는 높이가 매번 달라지므로 세로 속도를 0으로 맞춘 뒤 튕김
    void Bounce(float power)
    {
        rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
        rigid.AddForce(new Vector3(0, power, 0), ForceMode.Impulse);
    }

    void StartFlying(Bounds block)
    {
        isFlying = true;
        rigid.useGravity = false;

        //블록 바로 위에서 출발 (예전에는 블록과 겹치는 위치로 순간이동했음)
        float radius = sphere != null ? sphere.radius * transform.lossyScale.y : 0.5f;
        transform.position = new Vector3(block.center.x, block.max.y + radius + 0.05f, block.center.z);
        rigid.velocity = flyDirection * flySpeed;
    }

    void StopFlying()
    {
        isFlying = false;
        rigid.useGravity = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Star")
        {
            StarCount++;
            AudioManager.Instance.PlaySound("GetStar");
            other.gameObject.SetActive(false);
        }
    }
}
