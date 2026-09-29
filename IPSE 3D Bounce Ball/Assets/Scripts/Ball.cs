using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    Rigidbody rigid;

    
    bool isFlying = false;

    public float FlyPower;
    public float JumpPower;
    public float MaxSpeed;
    public int StarCount;
    public float movePower = 5f;
    GameManager manager = new GameManager();
    
    void Awake()
    {
        rigid = GetComponent<Rigidbody>(); 
        manager = GetComponent<GameManager>();
        
    }

    void FixedUpdate() // W A S D로 움직이기
    {

        float h = Input.GetAxisRaw("Horizontal"); //수평방향(왼쪽, 오른쪽)
        float v = Input.GetAxisRaw("Vertical");   //수직방향(앞, 뒤)

        //X Y Z축을 카메라 기준으로 변경
        Vector3 direction = new Vector3(h, 0, v).normalized;
        direction = Camera.main.transform.TransformDirection(direction);
        direction.y = 0;

        rigid.AddForce(direction * movePower, ForceMode.Force);  // direction을 사용하고, ForceMode를 Force로 변경

        Vector2 horizontalVelocity = new Vector2(rigid.velocity.x, rigid.velocity.z);

        if (horizontalVelocity.magnitude > MaxSpeed)
        {
            horizontalVelocity = horizontalVelocity.normalized * MaxSpeed;
            rigid.velocity = new Vector3(horizontalVelocity.x, rigid.velocity.y, horizontalVelocity.y);
        }

        else if (isFlying) //날라갈 때 y축 속도를 0으로 계속 업데이트
        {
            rigid.velocity = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            rigid.AddForce(new Vector3(0, JumpPower, 0), ForceMode.Impulse);
            AudioManager.Instance.PlaySound("Bounce");
        }

        else if (collision.gameObject.tag == "JumpBlock")
        {
            foreach (ContactPoint point in collision.contacts)
            {
                if (Vector3.Dot(point.normal, Vector3.up) > 0.5)
                {
                    rigid.AddForce(new Vector3(0, 2 * JumpPower, 0), ForceMode.Impulse);
                    AudioManager.Instance.PlaySound("ItemBlock");
                }
            }
        }

        else if (collision.gameObject.tag == "OneStepBlock")
        {
            foreach (ContactPoint point in collision.contacts)
            {
                if (Vector3.Dot(point.normal, Vector3.up) > 0.5)
                {
                    rigid.AddForce(new Vector3(0, JumpPower, 0), ForceMode.Impulse);
                    AudioManager.Instance.PlaySound("Bounce");
                    collision.gameObject.SetActive(false);
                }
            }
        }
        else if (collision.gameObject.tag == "FlyBlock")
        {
            foreach (ContactPoint point in collision.contacts)
            {

                if (Vector3.Dot(point.normal, Vector3.up) > 0.5)
                {
                    isFlying = true;
                    AudioManager.Instance.PlaySound("ItemBlock");
                    Vector3 ObjectPosition = collision.gameObject.transform.position;
                    Vector3 StartPosition = ObjectPosition + new Vector3(0, 0, 1);
                    transform.position = StartPosition;


                    float flySpeed = 10f;
                    rigid.velocity = new Vector3(0, 0, flySpeed);


                }
            }
        }
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

    

    /*void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            gameObject.SetActive(false);
        }
    }*/


}
