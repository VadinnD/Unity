using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Travel speed
    public float jumpForce = 5f; // Jump Power
    private float gravity = -9.8f; // Gravity
    private bool canMove = true; // Flag allowing movement
    private CharacterController characterController; // Reference to the CharacterController component
    private Vector3 velocity; // Velocity vector

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (canMove)
        {
            Move();
        }
    }

    void Move()
    {
        float moveX = Input.GetAxis("Horizontal"); // Get input horizontally
        float moveZ = Input.GetAxis("Vertical"); // Get input vertically

        Vector3 move = transform.right * moveX + transform.forward * moveZ; // Compose the motion vector

        characterController.Move(move * speed * Time.deltaTime); // Move the character

        // Jump and gravity
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && characterController.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity); // Calculate and apply vertical speed for the jump
        }

        velocity.y += gravity * Time.deltaTime; // Apply gravity
        characterController.Move(velocity * Time.deltaTime); // Apply vertical movement
    }

    public void EnableMovement()
    {
        canMove = true; // Enable movement
    }

    public void DisableMovement()
    {
        canMove = false; // Disable motion
    }
}
