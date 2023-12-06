using UnityEngine;
using System.Collections;
public class Player : MonoBehaviour
{
    private float movement;
    [SerializeField] private float movementSpeed = 16f;
    [SerializeField] private float jumpForce = 15f;
    private bool doubleJump;
    private bool isJumping;
    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    [SerializeField] private float jumpBufferTime = 0.2f;
    private float jumpBufferCounter;
    private bool isFacingRight = true;
    [SerializeField] private LayerMask layer;
    private Rigidbody2D rb;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        movement = Input.GetAxisRaw("Horizontal");
        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (IsGrounded() && !Input.GetKey(KeyCode.C))
        {
            doubleJump = false;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            jumpBufferCounter = jumpBufferTime;
            if (IsGrounded() || doubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                doubleJump = !doubleJump;
            }
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            jumpBufferCounter = 0f;

            StartCoroutine(JumpCooldown());
        }
        if (Input.GetKeyUp(KeyCode.C) && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.2f);

            coyoteTimeCounter = 0f;
        }

        FlipAndAnimate();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(movement * movementSpeed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return rb.IsTouchingLayers(layer);
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

    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Scuttle") || collision.gameObject.CompareTag("Stingbite") || collision.gameObject.CompareTag("Beelzebub"))
        {
           
        }
    }
}