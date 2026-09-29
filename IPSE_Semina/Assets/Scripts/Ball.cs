using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //점프 파워
    public float jumpPower;
    //공의 최대 속도
    public float maxSpeed;
    //공의 동력
    public float movePower;

    Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() // W A S D로 움직이기
    {

        float h = Input.GetAxisRaw("Horizontal"); //수평방향(왼쪽, 오른쪽)
        float v = Input.GetAxisRaw("Vertical");   //수직방향(앞, 뒤)

        //X Y Z축을 카메라 기준으로 변경
        Vector3 direction = new Vector3(h, 0, v).normalized;
        direction = Camera.main.transform.TransformDirection(direction);
        direction.y = 0;

        rb.AddForce(direction * movePower, ForceMode.Force);  // direction을 사용하고, ForceMode를 Force로 변경

        Vector2 horizontalVelocity = new Vector2(rb.velocity.x, rb.velocity.z);

        if (horizontalVelocity.magnitude > maxSpeed)
        {
            horizontalVelocity = horizontalVelocity.normalized * maxSpeed;
            rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.y);
        }

   

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }

    }


    
}
