using UnityEngine;
using System.Collections;
public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D box;
    [SerializeField] private LayerMask layer;
    [SerializeField] private bool isFacingRight = true;
    private float movement;
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float movementSpeed = 7.5f;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        movement = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movement * movementSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.C) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }

        FlipAndAnimate();
    }

    private void FixedUpdate()
    {
       
    }

    private void FlipAndAnimate()
    {
        if (isFacingRight && movement < 0f || !isFacingRight && movement > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }

        animator.SetBool("isRunning", movement != 0f);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0f, Vector2.down, 0.1f, layer);
    }
}