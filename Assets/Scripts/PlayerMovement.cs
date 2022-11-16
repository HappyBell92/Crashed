using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float rotationSpeed;

    

    public float groundDrag;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask WhatIsGround;
    public bool grounded;

    //[Header("Slope Handling")]
    //public float maxSlopeAngle;
    //public float steepSlopeAngle;
    //private RaycastHit slopeHit;
    //private bool exitingSlope;

    public Transform orientation;

    private float vertical;
    private float horizontal;
    private float rotateAround;

    float horizontalInput;
    float verticalInput;

    Vector3 movementDirection;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState
    {
        walking,
        sprinting,
        air
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        readyToJump = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        // Ground Check
        grounded = Physics.Raycast(transform.position, -transform.up, playerHeight * 0.5f + 0.2f, WhatIsGround);
        Debug.DrawRay(transform.position, -transform.up, Color.green, Time.deltaTime);

        MyInput();
        SpeedControl();
        StateHandler();
        

        // Handle drag
        if (grounded)
            rb.drag = groundDrag;
        else
            rb.drag = 0;

        //if (SteepSlope())
        //{
        //rb.AddForce(Vector3.down * 15f, ForceMode.Force);
        //}

        

    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // when to jump
        if (Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

    }

    private void StateHandler()
    {
        
        // Mode - sprinting
        if(grounded && Input.GetKey(sprintKey))
        {
            Debug.Log("Sprinting");
            state = MovementState.sprinting;
            moveSpeed = sprintSpeed;
        }

        // Mode - walking
        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        // Mode - air
        else
        {
            state = MovementState.air;
        }
    }

    private void MovePlayer()
    {
        // Calculate movement direction;
        movementDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        rotateAround = Input.GetAxis("Mouse X");

        // On slope
        //if (OnSlope() && !exitingSlope)
        //{
        //rb.AddForce(GetSlopeMoveDirection() * moveSpeed * 20f, ForceMode.Force);

        //if (rb.velocity.y > 0)
        //rb.AddForce(Vector3.down * 80f, ForceMode.Force);
        //}

        // On ground
        if (grounded)
        {
            //rb.AddForce(movementDirection * moveSpeed * 10f, ForceMode.Force);
            rb.velocity = (transform.forward * vertical) * moveSpeed;
            rb.velocity = (transform.right * horizontal) * moveSpeed;
            transform.Rotate((transform.up * rotateAround) * rotationSpeed * Time.deltaTime);


        }




        // In air
        //else
        //{
        //rb.AddForce(movementDirection * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        //rb.velocity = (movementDirection) * moveSpeed * airMultiplier;
        //}


        // Turn gravity off on slope
        //rb.useGravity = !OnSlope();
    }

    private void SpeedControl()
    {
        // Limit speed on slope
        //if (OnSlope() && !exitingSlope)
        //{
            //if (rb.velocity.magnitude > moveSpeed)
                //rb.velocity = rb.velocity.normalized * moveSpeed;
        //}

        // Limit speed on ground or in air
        //else
        //{
            //Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            // Limit Velocity if needed
            //if (flatVel.magnitude > moveSpeed)
            //{
                //Vector3 limitedVel = flatVel.normalized * moveSpeed;
                //rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            //}
        //}

        
    }

    private void Jump()
    {
        //exitingSlope = true;

        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;

        //exitingSlope = false;
    }

    //private bool OnSlope()
    //{
        //if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.4f))
        //{
            //float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            //return angle < maxSlopeAngle && angle != 0;
        //}

        //return false;
    //}

    //private bool SteepSlope()
    //{
        //if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.5f))
        //{
            //float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            //return angle > steepSlopeAngle && angle != 0;
        //}

        //return false;
    //}

    //private Vector3 GetSlopeMoveDirection()
    //{
        //return Vector3.ProjectOnPlane(movementDirection, slopeHit.normal).normalized;
    //}
}
