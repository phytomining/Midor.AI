using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravity = 9.81f; // Gravity value
    public LayerMask groundLayer; // Layer mask for the ground objects

    public Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if the object is grounded
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.1f, groundLayer);

        // Apply gravity if not grounded
        if (!isGrounded)
        {
            Debug.Log("Trying to move");
            rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        }
    }
}