using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float sensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f;
    private void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void LateUpdate()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        float rotateX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float rotateY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= rotateY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerBody.Rotate(Vector3.up * rotateX);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
