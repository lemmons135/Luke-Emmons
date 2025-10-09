using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{
    float movementX;
    [SerializeField] float speed = 6;

    public int maxJumpCount = 2;
    private int jumpCount;

    bool isGrounded = false;
    bool jumpKeyPressed = false;
    public float jumpForce = 10f;

    [SerializeField] Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {
        jumpCount = maxJumpCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMove(InputValue value)
    {
        movementX = Mathf.Round(value.Get<Vector2>().x);
    }

    void OnJump()
    {
        jumpKeyPressed = true;
        isGrounded = false;
    }

    void OnFire()
    {
        Debug.Log("Player x coord: " + transform.position.x);
    }

    void FixedUpdate()
    {
        float XmoveDistance = movementX * speed * Time.fixedDeltaTime;

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
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            resetJump();
        }
    }

    void resetJump()
    {
        jumpCount = maxJumpCount;
        isGrounded = true;
        jumpKeyPressed = false;
    }
}
