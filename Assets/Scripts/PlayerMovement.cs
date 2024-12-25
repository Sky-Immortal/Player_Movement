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

    private PlayerInputActions inputActions;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Initialize input actions
        inputActions = new PlayerInputActions();

        // Subscribe to input events
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        inputActions.Player.Jump.performed += ctx => Jump();
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

        // Check if grounded
        if (groundCheck != null)
        {
            bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
            if (isGrounded)
                isJumping = false;
        }
    }

    private void Jump()
    {
        if (!isJumping)
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
