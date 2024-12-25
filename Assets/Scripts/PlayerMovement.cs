using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public GameObject gameOverUI; // Reference to the Game Over UI element

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool isJumping;
    private bool isGrounded; // Declare isGrounded at class level
    private Animator animator; // Reference to the Animator component

    private PlayerInputActions inputActions;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Initialize Animator reference

        // Initialize input actions
        inputActions = new PlayerInputActions();

        // Subscribe to input events
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        inputActions.Player.Jump.performed += ctx => Jump();
    }

    private void Flip()
{
    if (moveInput.x > 0)
    {
        transform.localScale = new Vector3(3, 3, 1); // Facing right
    }
    else if (moveInput.x < 0)
    {
        transform.localScale = new Vector3(-3, 3, 1); // Facing left
    }
}

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void Update()
    {
        // Move the player
    Vector2 movement = new Vector2(moveInput.x * speed, rb.velocity.y);
    rb.velocity = movement;

    // Set animator parameters
    animator.SetBool("Run", moveInput.x != 0 && isGrounded); // Set "Run" based on movement and grounded state
    animator.SetBool("Jump", !isGrounded); // Set "Jump" based on grounded state

    // Check if grounded
    if (groundCheck != null)
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        if (isGrounded)
            isJumping = false;
    }

    // Flip the player based on movement direction
    Flip();
    }

    public bool IsJumping() // Method to check if the player is jumping
{
    return isJumping;
}

    private void Jump()
{
    if (isGrounded && !isJumping) // Allow jump only if grounded
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        isJumping = true;
    }
}

    private void OnDrawGizmosSelected()
    {
        // Visualize ground check
        if (groundCheck != null)
            Gizmos.DrawWireSphere(groundCheck.position, 0.1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            if (gameOverUI != null)
            {
                gameOverUI.SetActive(true); // Enable the Game Over UI element
            }
        }
    }
}
