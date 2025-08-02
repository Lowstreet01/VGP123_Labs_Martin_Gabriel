using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer), typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{   // Variables.

    [SerializeField] private bool isGrounded = false;
    
    [SerializeField] private float groundCheckRadius = 0.02f;
    private LayerMask groundLayer;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Collider2D col;
    private Animator anim;

    [SerializeField] private int maxJumpCount = 2; 
    private int jumpCount = 1;

    private Vector2 groundCheckPos => new Vector2(col.bounds.min.x + col.bounds.extents.x, col.bounds.min.y);


    // Start is called once before the first execution of Update after the MonoBehaviour is created.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();

        groundLayer = LayerMask.GetMask("Ground");

        if (groundLayer == 0)
        {
            Debug.LogWarning("Ground layer not set. Please set the Ground layer in the LayerMask.");
        }
        
    }

    // Update is called once per frame.
    void Update()
    {   // Different variables called for the different components.
      
        float hValue = Input.GetAxisRaw("Horizontal");
        //Debug.Log("Ground Check Position: " + groundCheckPos);
        SpriteFlip(hValue);
        
        
        // Adjust speed as necessary.
        rb.linearVelocityX = hValue * 5f; 
        
        // Adjust jump force as necessary.
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumpCount)
        {
            
            rb.AddForce(Vector2.up * 7f, ForceMode2D.Impulse);
            jumpCount++;
            //debug.log("Jump Count: " + jumpCOunt);
        }
        isGrounded = Physics2D.OverlapCircle(groundCheckPos, groundCheckRadius, groundLayer);
        if (isGrounded)
        {
            jumpCount = 1;
        }
        // Update animator parameters.
        anim.SetFloat("hValue", Mathf.Abs(hValue));
        anim.SetBool("isGrounded", isGrounded);
    }
    // Flip the sprite based on horizontal input.
    void SpriteFlip(float hValue)
    {
        if (hValue != 0) sr.flipX = (hValue < 0);
    }
}
