using UnityEngine;
using System.Collections;
namespace LunarflyArts
{
    [RequireComponent(typeof(CapsuleCollider2D), typeof(Collision), typeof(Rigidbody2D))]
    public class Movement : Player
    {
        private Animator animator;
        private Rigidbody2D rb;
        private float horizontalMovement;
        private Collision collision;
        private float movementSpeed = 10f;
        private float jumpForce = 12f;
        private bool isFacingRight = false;

        private bool canDash = true;
        public bool ResetDash() { return canDash = true; }
        private bool isDashing;
        private float dashingForce = 25f;
        private float dashingTime = 0.25f;
        private float dashingCooldown = 1f;

        private bool isJumping;
        private float coyoteTime = 0.2f;
        private float coyoteTimeCounter;
        private float jumpBufferTime = 0.2f;
        private float jumpBufferCounter;

        private TrailRenderer tr;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            tr = GetComponent<TrailRenderer>();
            collision = GetComponent<Collision>();
            animator = GetComponent<Animator>();
        }

        private void Update()
        {
            if (isDashing) return;

            horizontalMovement = inputManager.MovementInput;
            if (horizontalMovement == 0)
            {
                animator.SetBool("isRunning", false);
            }
            else
            {
                animator.SetBool("isRunning", true);
            }
            if (collision.IsGrounded())
            {
                coyoteTimeCounter = coyoteTime;
            }
            else
            {
                coyoteTimeCounter -= Time.deltaTime;
            }

            if (inputManager.JumpPressed)
            {
                jumpBufferCounter = jumpBufferTime;
            }
            else
            {
                jumpBufferCounter -= Time.deltaTime;
            }

            if (coyoteTimeCounter > 0f && jumpBufferCounter > 0f && !isJumping)
            {
                HandleJumpWithCoyoteTime();
            }

            if (inputManager.JumpReleased && rb.velocity.y > 0f)
            {
                HandleQuickJump();
            }
            if (inputManager.DashInput && canDash)
            {
                StartCoroutine(Dash());
            }
        }

        private void FixedUpdate()
        {
            if (isDashing) return;
            HandleMovement();

            if (horizontalMovement > 0 && !isFacingRight)
            {
                Flip();
            }
            else if (horizontalMovement < 0 && isFacingRight)
            {
                Flip();
            }
        }
        private void HandleMovement()
        {
            rb.velocity = new Vector2(horizontalMovement * movementSpeed, rb.velocity.y);
        }

        private void HandleJumpWithCoyoteTime()
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpBufferCounter = 0;
            StartCoroutine(JumpCooldown());
        }

        private void HandleQuickJump()
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
        private void Flip()
        {
            if(isFacingRight)
            {
                Vector3 rotate = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
                transform.rotation = Quaternion.Euler(rotate);
                isFacingRight = !isFacingRight;
            }
            else
            {
                Vector3 rotate = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
                transform.rotation = Quaternion.Euler(rotate);
                isFacingRight = !isFacingRight;
            } 
                
        }
        private IEnumerator Dash()
        {
            canDash = false;
            isDashing = true;
            float originalGravity = rb.gravityScale;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(transform.localScale.x * dashingForce, 0f);
            tr.emitting = true;
            yield return new WaitForSeconds(dashingTime);
            tr.emitting = false;
            rb.gravityScale = originalGravity;
            isDashing = false;
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
        }

        private IEnumerator JumpCooldown()
        {
            isJumping = true;
            yield return new WaitForSeconds(0.4f);
            isJumping = false;
        }
    }
}
