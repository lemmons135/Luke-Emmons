using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    float movementX;
    [SerializeField] float speed = 6;
    [SerializeField] GameObject playerColliderDetector;

    public int maxJumpCount = 2;
    private int jumpCount;

    bool isGrounded = false;
    bool jumpKeyPressed = false;
    public float jumpForce = 10f;

    [SerializeField] Rigidbody2D rb;

    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        animator = GetComponent<Animator>();
        jumpCount = maxJumpCount;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("walking", movementX != 0f);
    }

    void OnMove(InputValue value)
    {
        movementX = Mathf.Round(value.Get<Vector2>().x);
    }

    void OnJump()
    {
        jumpKeyPressed = true;
        //
        isGrounded = false;
    }

    void FixedUpdate()
    {
        float XmoveDistance = movementX * speed;
        //isGrounded = playerColliderDetector.GetComponent<PlayerColliderLogic>().GetIsGrounded();

        // if the player wants to jump and can still jump, jump
        if (jumpKeyPressed && jumpCount > 0)
        {
            // if the player is already in the air, then make velocity zero before jumping
            if (!isGrounded)
            { 
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            }

            // jump
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            // player jumped and the input is over no double jumps with one key press
            jumpCount--;
            jumpKeyPressed = false;
        }

        transform.position = new Vector2(transform.position.x + XmoveDistance, transform.position.y);

        /*
        if (isGrounded)
        {
            resetJump();
        }
        */
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("ground"))
        {
            isGrounded = true;
            resetJump();
        }
    }

    void resetJump()
    {
        jumpCount = maxJumpCount;
        jumpKeyPressed = false;
    }
}
