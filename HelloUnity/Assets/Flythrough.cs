using UnityEngine;

public class Flythrough : MonoBehaviour
{
    public float cameraSpeed = 10.0f; 
    public float lookSensitivity = 2.0f;  // Mouse sensitivity for looking around

    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    void Update()
    {
        // Mouse input for rotating the camera
        rotationX += Input.GetAxis("Mouse X") * lookSensitivity;

        rotationY -= Input.GetAxis("Mouse Y") * lookSensitivity;
        rotationY = Mathf.Clamp(rotationY, -90, 90);

        transform.localRotation = Quaternion.Euler(rotationY, rotationX, 0);

        // WASD inpu
        Vector3 forwardMovement = transform.forward * Input.GetAxis("Vertical") * cameraSpeed * Time.deltaTime;
        Vector3 rightMovement = transform.right * Input.GetAxis("Horizontal") * cameraSpeed * Time.deltaTime;

        transform.position += forwardMovement + rightMovement;
    }

    public void SyncRotationWithCurrentView(Quaternion currentRotation)
    {
        Vector3 eulerRotation = currentRotation.eulerAngles;
        rotationX = eulerRotation.y;
        rotationY = eulerRotation.x;
    }
}



