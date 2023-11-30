using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 7.5f;
    [Header("Jump")]
    [SerializeField] private float jumpForce = 12f;
    [SerializeField] private float jumpTime = 0.35f;
    private bool isFacingRight;
    private Rigidbody2D rb;
    private Animator animator;
    private float moveInput;
    private bool isJumping;
    private bool isFalling;
    private float jumpTimeCounter;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartDirectionCheck();
    }

    private void Update()
    {
        Move();
        Jump();
    }

    #region Movement
    private void Move()
    {
        moveInput = UserInput.instance.movementInput.x;
        if (moveInput > 0 || moveInput < 0) { animator.SetBool("isRunning", true); TurnCheck(); }
        else animator.SetBool("isRunning", false);
        rb.velocity = new Vector2(moveInput * movementSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        // Se il pulsante è stato premuto in questo fotogramma.
        if (UserInput.instance.inputActions.Gameplay.Jump.WasPressedThisFrame())
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Se il pulsante è stato tenuto premuto.
        if (UserInput.instance.inputActions.Gameplay.Jump.IsPressed())
        {
            if(jumpTimeCounter > 0 && isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }

        // Se il pulsante è stato rilasciato in questo fotogramma.
        if (UserInput.instance.inputActions.Gameplay.Jump.WasReleasedThisFrame())
        {
            isJumping = false;
        }
    }
    #endregion

    #region Turn Checks
    private void StartDirectionCheck()
    {
        if(transform.position.x > 0f) isFacingRight = true;
        else isFacingRight = false;
    }

    private void TurnCheck()
    {
        if (UserInput.instance.movementInput.x > 0 && !isFacingRight) Turn();
        else if (UserInput.instance.movementInput.x < 0 && isFacingRight) Turn();
    }

    private void Turn()
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
    #endregion
}
