using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //점프 파워
    public float jumpPower;
    //공의 최대 속도
    public float maxSpeed;
    //움직이는 힘
    public float movePower;

    Rigidbody rb;
    AudioSource audio;
    public AudioClip bounce;

    private void Start()
    {   //변수 초기화
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //수평방향(X)
        float h = Input.GetAxisRaw("Horizontal");
        //수직방향(Z)
        float v =Input.GetAxisRaw("Vertical"); 
        //벡터를 카메라의 로컬 좌표계 기준으로 변환
        Vector3 direction = Camera.main.transform.TransformDirection(h, 0,v);
        //공에 힘 가하기
        rb.AddForce(movePower * direction, ForceMode.Force);
        //수평방향 속도
        Vector2 horizontalVelocity = new Vector2(rb.velocity.x,rb.velocity.z);
        //최대속력 제한
        if(horizontalVelocity.magnitude > maxSpeed) 
        {
            horizontalVelocity = horizontalVelocity.normalized * maxSpeed;
            rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, rb.velocity.z);
        }

        /*float h = Input.GetAxisRaw("Horizontal"); //수평방향
        float v =Input.GetAxisRaw("Vertical"); //수직방향
        Vector3 direction = new Vector3(h,0, v).normalized; 
        direction=Camera.main.transform.TransformDirection(direction);
        direction.y = 0;*/
    }

    void OnCollisionEnter(Collision collision)
    {
        //공과 바닥이 닿으면
        if(collision.gameObject.name == "Floor") 
        {
            //y축 방향으로 튕긴다
            rb.AddForce(new Vector3(0,jumpPower,0), ForceMode.Impulse); 
            
        }
    }

    public void LoadAudioClips()
    {
        bounce = Resources.Load<AudioClip>("Sound/bounce");
    }

    public void PlaySound(string clipName)
    {
        switch (clipName)
        {
            case "Bounce":
                audio.clip = bounce;
                break;
    
        }
        audio.Play();
    }



}
