using UnityEngine;

public class SmoothLookAtInput3D : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public float depth = 10f; // Distance from the camera (adjust as needed)

    void Update()
    {
        Vector3 inputPos = Vector3.zero;

        if (Input.touchCount > 0)
        {
            inputPos = Input.GetTouch(0).position; // Get touch position
        }
        else
        {
            inputPos = Input.mousePosition; // Get mouse position
        }

        inputPos.z = depth; // Set distance from camera
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(inputPos);
        Vector3 direction = (worldPos - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
