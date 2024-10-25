using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotionControls : MonoBehaviour
{
    public float linearSpeed = 5f;
    public float turningSpeed = 100f;
    public float gravity = -9.81f;       // Gravity value
    private Animator animator;           // Animator component
    private CharacterController charController;  // CharacterController component
    private Vector3 moveDirection = Vector3.zero;  // Movement direction
    private float speed;  // Used to track player's current speed
    private bool isGrounded;  // Check if player is on the ground

    void Start()
    {
        // Get the Animator and CharacterController components attached to the player
        animator = GetComponent<Animator>();
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleMovement();
        ApplyGravity();
        UpdateAnimator();
    }

    private void HandleMovement()
    {
        // Reset horizontal movement direction and speed each frame
        moveDirection.x = 0f;
        moveDirection.z = 0f;
        speed = 0f;

        // Move forward and backward
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += transform.forward;
            speed = linearSpeed;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDirection -= transform.forward;
            speed = linearSpeed;
        }

        // Rotate left and right
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, -turningSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, turningSpeed * Time.deltaTime);
        }

        // Use CharacterController to move the character
        charController.Move(moveDirection * linearSpeed * Time.deltaTime);
    }

    private void ApplyGravity()
    {
        // Check if the player is grounded
        isGrounded = charController.isGrounded;

        if (isGrounded && moveDirection.y < 0)
        {
            moveDirection.y = 0f;  // Reset vertical movement when grounded
        }

        // Apply gravity to vertical movement
        moveDirection.y += gravity * Time.deltaTime;

        // Apply vertical movement using CharacterController
        charController.Move(moveDirection * Time.deltaTime);
    }

    private void UpdateAnimator()
    {
        // Update animator parameters based on movement
        animator.SetBool("isMoving", speed > 0);  // Player is moving if speed > 0, otherwise idle
    }
}
