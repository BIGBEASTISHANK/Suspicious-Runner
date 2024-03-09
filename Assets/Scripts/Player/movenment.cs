using UnityEngine;

public class movenment : MonoBehaviour
{
    // VARIABLES
    /////////////////
    // Player Movenment
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    private float moveSpeed;

    // Gravity
    [SerializeField] private float gravity;
    [SerializeField] private float groundCheckSize;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private bool isGrounded;
    private Vector3 velocity;

    // Jumping
    [SerializeField] private float jump;

    // Crouch
    [SerializeField] private float crouchSize;
    [SerializeField] private float unCrouchSize;
    private bool isCrouched = false;

    // Others
    private CharacterController controller;

    // REFFFERNCE
    //////////////////////
    // RUN ONCE AT START
    private void Start()
    {
        // Getting Character controller from the children.
        controller = GetComponentInChildren<CharacterController>();
    }

    // UPDATE EVERY FRAME
    private void Update()
    {
        // MOVENMENT FUNCTION
        Move();
    }

    private void Move()
    {
        // Checking if player is grounded.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckSize, groundLayer);

        // If player is grounded then stop applying gravity.
        if (isGrounded && velocity.y < 0)
        {
            // Setting velocity to -2 for being on safe side.
            velocity.y = -2;
        }

        // VARIABLE
        float moveX = Input.GetAxis("Vertical");
        float moveZ = -Input.GetAxis("Horizontal");

        // Getting where to move the player.
        Vector3 move = new Vector3(moveX, 0, moveZ);
        // Making it so it move on player's X and Z axis insted of global axis.
        move = transform.TransformDirection(move);

        // Condition for walking and running speed
        if (move != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        {
            // Move with walk speed.
            Walk();
        }
        else if (move != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        {
            // Move with walk speed.
            Run();
        }

        // Checking if player is on ground.
        if (isGrounded)
        {
            // Make Player jump.
            Jump();
        }

        // Make Player Crouch
        Crouch();

        // Adding either move speed or run speed.
        move *= moveSpeed;
        // Moving Player
        controller.Move(move * Time.deltaTime);

        // Increasing velocity with respect of gravity.
        velocity.y += gravity * Time.deltaTime;
        // Applying gravity on player.
        controller.Move(velocity * Time.deltaTime);
    }

    // WALK FUNCTION
    private void Walk()
    {
        if (isCrouched)
        {
            // Setting move speed to walk speed but slower.
            moveSpeed = walkSpeed / 3;
        }
        else
        {
            // Setting move speed to walk speed.
            moveSpeed = walkSpeed;
        }
    }

    // RUN FUNCTION
    private void Run()
    {
        if (isCrouched)
        {
            // Setting move speed to run speed but slower.
            moveSpeed = runSpeed / 3;
        }
        else
        {
            // Setting move speed to run speed.
            moveSpeed = runSpeed;
        }
    }

    // JUMP FUNCTION
    private void Jump()
    {
        // If player use jump button.
        if (Input.GetButtonDown("Jump") && !isCrouched)
        {
            // Changing velocity to make player jump
            velocity.y = Mathf.Sqrt(jump * -2 * gravity);
        }
    }

    // CROUCH FUNCTION
    private void Crouch()
    {
        // If Left alt is pressed down make player crouch.
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            // Making Player crouch.
            transform.localScale = new Vector3(transform.localScale.x, crouchSize, transform.localScale.z);
            // Crouch true
            isCrouched = true;
        }
        // When left alt is released make player normal.
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            // Making Player normal.
            transform.localScale = new Vector3(transform.localScale.x, unCrouchSize, transform.localScale.z);
            // Crouch False
            isCrouched = false;
        }
    }
}
