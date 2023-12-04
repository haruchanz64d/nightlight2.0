using UnityEngine;
using System.Collections;
public class Player : MonoBehaviour
{
    [Header("Movement")]
    private float moveInput;
    [SerializeField] private float movementSpeed = 7.5f;
    public bool isFacingRight = true;
    [Header("Jump")]
    [SerializeField] private float jumpForce = 12f;
    private float jumpTimeCounter;
    [SerializeField] private float jumpTime = 0.35f;
    private bool isJumping;
    [SerializeField] private float maxFallSpeed = 12f;
    [Header("Ground Check")]
    [SerializeField] private float extraHeight = 0.25f;
    [SerializeField] private LayerMask layerMask;
    [Header("Dashing")]
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.25f;
    [SerializeField] private float dashCooldown = 0.5f;
    private float dashTimer = 0f;
    private bool isDashing = false;

    private Rigidbody2D rb;
    private Animator animator;
    private new Collider2D collider2D;
    private RaycastHit2D raycastHit2D;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();
        StartDirectionCheck();
    }

    private void Update()
    {
        Move();
        Jump();
        Dash();
        //Attack();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector3(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -maxFallSpeed, maxFallSpeed * 5f));

        if (moveInput > 0 || moveInput < 0) TurnCheck();
    }

    #region Movement
    private void Move()
    {
        moveInput = UserInput.instance.movementInput.x;
        if (moveInput > 0 || moveInput < 0) { animator.SetBool("isRunning", true); TurnCheck(); }
        else animator.SetBool("isRunning", false);
        rb.velocity = new Vector2(moveInput * movementSpeed, rb.velocity.y);
        TurnCheck();
    }

    private void Jump()
    {
        if (UserInput.instance.inputActions.Gameplay.Jump.WasPressedThisFrame() && IsGrounded())
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (UserInput.instance.inputActions.Gameplay.Jump.IsPressed())
        {
            if (jumpTimeCounter > 0 && isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else if (jumpTimeCounter == 0)
            {
                isJumping = false;
            }
            else
            {
                isJumping = false;
            }
        }

        if (UserInput.instance.inputActions.Gameplay.Jump.WasReleasedThisFrame())
        {
            isJumping = false;
        }
    }

    private void Dash()
    {
        if (UserInput.instance.inputActions.Gameplay.Dash.WasPressedThisFrame() && !isDashing && dashTimer <= 0f)
        {
            isDashing = true;
            dashTimer = dashDuration;
        }

        if (isDashing)
        {
            rb.AddForce(transform.right * dashSpeed, ForceMode2D.Impulse);
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0f) isDashing = false;
        }

        if (!isDashing)
        {
            dashTimer += Time.deltaTime;
            if (dashTimer >= dashCooldown) dashTimer = dashCooldown;
        }
        animator.SetBool("isDashing", isDashing);
    }
    #endregion

    #region Ground Check
    private bool IsGrounded()
    {
        raycastHit2D = Physics2D.BoxCast(collider2D.bounds.center, collider2D.bounds.size, 0f, Vector2.down, extraHeight, layerMask);
        if (raycastHit2D.collider != null) return true;
        else return false;
    }
    #endregion
    #region Turn Checks
    private void StartDirectionCheck()
    {
        if (transform.position.x > 0f) isFacingRight = true;
        else isFacingRight = false;
    }

    private void TurnCheck()
    {
        if (UserInput.instance.movementInput.x > 0 && !isFacingRight) Turn();
        else if (UserInput.instance.movementInput.x < 0 && isFacingRight) Turn();
    }

    private void Turn()
    {
        if (isFacingRight)
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
    #endregion

    #region Combat
    // Unused
    /*private void Attack()
    {
        if (UserInput.instance.inputActions.Gameplay.Attack.triggered)
        {
            Debug.Log($"Attacking...");
            animator.SetBool("isAttacking", true);
        }
    }*/
    #endregion
}