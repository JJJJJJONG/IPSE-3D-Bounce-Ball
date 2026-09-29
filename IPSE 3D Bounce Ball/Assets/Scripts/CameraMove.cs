using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform playerTransform;
    public float mouseSensitivity = 500f;
    Vector3 offset;
    float xRotation = 0f;
    float yRotation = 0f;

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - playerTransform.position;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        yRotation += mouseX;

        Quaternion rotation = Quaternion.Euler(xRotation, yRotation, 0f);
        transform.position = playerTransform.position + rotation * offset;
        transform.LookAt(playerTransform.position);
    }
}
