using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform playerTransform;
    public float mouseSensitivity = 500f;
    //시작 위치가 공 뒤쪽 위(약 16도)라서 16도 넘게 내려가면 카메라가 바닥 밑으로 들어감
    public float minPitch = -10f;
    public float maxPitch = 60f;
    Vector3 offset;
    float xRotation = 0f;
    float yRotation = 0f;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - playerTransform.position;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minPitch, maxPitch);
        yRotation += mouseX;
    }

    //공이 움직인 뒤에 따라가야 화면이 떨리지 않음
    private void LateUpdate()
    {
        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        transform.position = playerTransform.position + rotation * offset;
        transform.LookAt(playerTransform.position);
    }
}
