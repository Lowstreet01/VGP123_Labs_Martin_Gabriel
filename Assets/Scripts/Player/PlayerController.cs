using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{   // Variables.

    private bool isGrounded = false;
    [SerializeField] private float groundCheckRadius = 0.02f;
    private LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Collider2D col;

    private Vector2 groundCheckPos;

    Vector2 GetGroundCheckPos()
    {
        return new Vector2(col.bounds.min.x + col.bounds.extents.x, col.bounds.min.y);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();

        groundLayer = LayerMask.GetMask("Ground");
    }

    // Update is called once per frame.
    void Update()
    {   // Different variables called for the different components.
       float hValue = Input.GetAxisRaw("Horizontal");
        Debug.Log("Ground Check Position: " + groundCheckPos);
        SpriteFlip(hValue);
        isGrounded = Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, groundLayer);
        
        // Adjust speed as necessary.
        rb.linearVelocityX = hValue * 5f; 
        
        // Adjust jump force as necessary.
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse); 
        }
    }
    // Flip the sprite based on horizontal input.
    void SpriteFlip(float hValue)
    {
        if (hValue != 0) sr.flipX = (hValue < 0);
    }
}
