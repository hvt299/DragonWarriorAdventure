using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;     // tốc độ chạy
    public float jumpForce = 12f;    // lực nhảy

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // --- Di chuyển trái phải ---
        moveInput = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // --- Lật hướng ---
        if (moveInput != 0)
            sr.flipX = moveInput < 0;

        // --- Nhảy ---
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }
}
