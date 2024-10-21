using UnityEngine;

public class CameraCointroller : MonoBehaviour
{
    public Transform player;       // Reference to the player's transform
    public Vector3 offset;         // Offset from the player
    public float rotationSpeed = 5f;  // Speed of camera rotation
    public float zoomSpeed = 10f;  // Speed of zooming
    public float minZoom = 5f;     // Minimum zoom distance
    public float maxZoom = 20f;    // Maximum zoom distance

    private float currentRotationY = 45f; // Current rotation angle around Y axis
    private Camera cameraComponent;

    void Start()
    {
        // Get the camera component to adjust orthographic size for zoom
        cameraComponent = GetComponent<Camera>();
    }
    void LateUpdate()
    {
        // Handle camera rotation around the player (Y-axis only)
        if (Input.GetMouseButton(2)) // Middle mouse button
        {
            float horizontalRotation = Input.GetAxis("Mouse X") * rotationSpeed;
            currentRotationY += horizontalRotation;  // Rotate around Y-axis only
        }

        // Handle zooming in and out by adjusting orthographic size
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        cameraComponent.orthographicSize -= scroll * zoomSpeed;
        cameraComponent.orthographicSize = Mathf.Clamp(cameraComponent.orthographicSize, minZoom, maxZoom); // Clamp zoom

        // Keep the camera's isometric angle locked at 45 degrees along the X-axis
        Quaternion rotation = Quaternion.Euler(45f, currentRotationY, 0f); // Lock X at 45 degrees, rotate Y
        Vector3 desiredPosition = player.position + rotation * offset;

        // Set camera position and make it look at the player
        transform.position = desiredPosition;
        transform.LookAt(player.position + Vector3.up * offset.y);  // Keeps the camera looking at the player
    }
}

