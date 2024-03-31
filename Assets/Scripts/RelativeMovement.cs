using UnityEngine;

public class RelativeMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;

    public GameObject orientation;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Calculate movement direction based on object's facing direction
        Vector3 forwardDirection = orientation.transform.forward;
        Vector3 rightDirection = orientation.transform.right;

        // Reset Y component to prevent movement in Y-axis
        forwardDirection.y = 0f;
        rightDirection.y = 0f;

        forwardDirection.Normalize();
        rightDirection.Normalize();
        
        // Move the object based on WASD input
        Vector3 movement = new Vector3(0, rb.velocity.y * 0.5f, 0);
        if (Input.GetKey(KeyCode.W))
        {
            movement += forwardDirection;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement -= forwardDirection;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += rightDirection;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement -= rightDirection;
        }

        // Normalize movement vector to prevent diagonal speed boost
        if (movement.magnitude > 1f)
        {
            movement.Normalize();
        }

        // Apply movement to the Rigidbody
        rb.velocity = movement * moveSpeed;
    }
}