using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float sprintSpeed = 8f;
    public float acceleration = 2f;
    private float currentSpeed;
    bool isSprinting;
    public float rotationSpeed = 720f;  // How fast the player rotates (degrees per second)
    private Vector3 moveDirection; // dir player is moving
    public Transform cameraTransform;  // Reference to the camera transform


    void Start() {
        currentSpeed = moveSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        HandleInput();
        //Move the player based on input if theres movement
        if (moveDirection.magnitude > 0.1f){
            MovePlayer(moveDirection);
        }
        else{
            // Immediately stop movement when no input is detected
            currentSpeed = 0f;
        } 
    }

    private void HandleInput(){
        // Get player input
        float moveX = Input.GetAxis("Horizontal"); // A/D or left right arrow
        float moveZ = Input.GetAxis("Vertical"); // W/S or Up/Down Arrow
        //Create a movement vector based on input
        moveDirection = new Vector3(moveX, 0, moveZ);
        // Create a movement vector relative to the camera's forward and right direction
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        // Since we're in an isometric or top-down view, we only care about the X and Z plane
        forward.y = 0f;  // Ignore the Y axis (no movement up/down)
        right.y = 0f;    // Ignore the Y axis (no movement up/down)
        forward.Normalize();
        right.Normalize();
        moveDirection = (forward * moveZ + right * moveX).normalized;
        // Check if the player is holding the Shift key for sprinting
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        // Smoothly transition the current speed from moveSpeed to sprintSpeed (or vice versa)
        if (isSprinting && (moveX != 0 || moveZ != 0)){
            float targetSpeed = isSprinting ? sprintSpeed : moveSpeed;
            currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, acceleration * Time.deltaTime);
        }
        else if(!isSprinting && (moveX != 0 || moveZ != 0)){
            currentSpeed = moveSpeed;
        }

    }
    public void MovePlayer(Vector3 direction){
            Vector3 movement = direction * currentSpeed * Time.deltaTime;
            transform.Translate(movement, Space.World);
            RotatePlayer(direction);

    }

    private void RotatePlayer(Vector3 direction){
        if(direction != Vector3.zero){
            //Instantly rotate the player to face the direction of movement
            //transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            //Smoooth Rotation
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
