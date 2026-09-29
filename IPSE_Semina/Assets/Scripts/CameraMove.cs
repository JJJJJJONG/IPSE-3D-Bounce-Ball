using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform playerTransform;
    public float mouseSensitivity = 500;
    Vector3 offset;
    float xRotation = 0;
    float yRotation = 0;

    private void Awake()
    {
        playerTransform = GameObject.Find("Ball").transform;
        offset = transform.position - playerTransform.position; //카메라의 위치 - 공의 위치 
        Cursor.lockState = CursorLockMode.Locked; //화면 커서 잠금
        
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; 
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY; //아래로 내리면 Y값 - -- + 시계
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //y축 회전을 -90 90까지로 지정
        yRotation += mouseX; //왼오 왼오

        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0f); //유닡티에서는 회전값을 쿼터니언으로 표현
        transform.position = playerTransform.position + rotation * offset;
        transform.LookAt(playerTransform.position);
    }
}
