using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
       float hValue = Input.GetAxisRaw("Horizontal");
        SpriteFlip(hValue);

        rb.linearVelocityX = hValue * 5f; // Adjust speed as necessary

        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse); // Adjust jump force as necessary
        }
    }
    void SpriteFlip(float hValue)
    {
        /*if (hValue < 0)
            sr.flipX = true;
        else if (hValue > 0)
            sr.flipX = false; */
        if (hValue != 0) sr.flipX = (hValue < 0);
    }
}
