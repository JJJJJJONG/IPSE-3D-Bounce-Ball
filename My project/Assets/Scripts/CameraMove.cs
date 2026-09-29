using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    Transform playerTransform;
    public float mouseSesitivity;
    Vector3 offset;
    float xRotation = 0;
    float yRotation = 0;

    private void Awake()
    {
        playerTransform = GameObject.Find("Ball").transform;
        offset = transform.position - playerTransform.position;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        //마우스 움직임을 시간에 따라 입력받음
        float mouseX = Input.GetAxis("Mouse X")* mouseSesitivity* Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSesitivity * Time.deltaTime;
        
        //X축 회전과 Y축회전
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        yRotation += mouseX;

        //오일러 각을 쿼터니언으로 변환
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0);
        //카메라의 위치
        transform.position = playerTransform.position + (rotation * offset);
        //카메라의 앞을 공으로 설정
        transform.LookAt(playerTransform.position);
    }
}
