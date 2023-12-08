using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShadowLila : MonoBehaviour
{
    [Header("Idle")]
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private Vector2 idleDirection;
    [Header("ATK Up and Down")]
    [SerializeField] private float attackMovementSpeed = 15f;
    [SerializeField] private Vector2 attackMoveDirection;
    [Header("Attack Player")]
    [SerializeField] private float attackPlayerSpeed = 15f;
    private Transform player;
    [Header("Others")]
    [SerializeField] private Transform groundCheckUp;
    [SerializeField] private Transform groundCheckDown;
    [SerializeField] private Transform groundCheckWall;
    [SerializeField] private float groundCheckRadius = 2.5f;
    [SerializeField] private LayerMask groundLayer;
    private bool isTouchingUp;
    private bool isTouchingDown;
    private bool isTouchingWall;
    private bool isGoingUp = true;
    private bool isFacingRight = true;
    private float maxHealth = 100f;
    private float currentHealth;
    [SerializeField] private Image healthBar;
    private Rigidbody2D rb;
    private Animator animator;
    [Header("HP Drain")]
    private float hpDrainDMG = 1.5f;
    private float hpDrainTickInterval = 2.00f;
    private float hpDrainTimer = 0f;

    private void Start()
    {
        idleDirection.Normalize();
        attackMoveDirection.Normalize();

        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        CheckForCollisions();
        IdleState();
    }

    private void FixedUpdate()
    {
        if (currentHealth <= 0f)
        {
            DestroyShadowLila();
        }
        CheckForCollisions();
        HPDrainOnUpdate();
    }

    private void HPDrainOnUpdate()
    {
        hpDrainTimer += Time.deltaTime;
        if (hpDrainTimer >= hpDrainTickInterval)
        {
            currentHealth -= hpDrainDMG;
            healthBar.fillAmount = currentHealth / 100f;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            hpDrainTimer = 0f;
        }
    }

    private void CheckForCollisions()
    {
        isTouchingUp = Physics2D.OverlapCircle(groundCheckUp.position, groundCheckRadius, groundLayer);
        isTouchingDown = Physics2D.OverlapCircle(groundCheckDown.position, groundCheckRadius, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(groundCheckWall.position, groundCheckRadius, groundLayer);
    }

    public void DestroyShadowLila()
    {
        StartCoroutine(PlayAnimationBeforeDestroy());
        player.GetComponent<Player>().IsShadowLilaDefeated = true;
        Destroy(gameObject);
    }

    private IEnumerator PlayAnimationBeforeDestroy()
    {
        animator.Play("Death");
        yield return new WaitForSeconds(5.0f);
    }

    #region States
    private void IdleState()
    {
        if (isTouchingUp && isGoingUp)
        {
            ChangeDirection();
        }
        else if(isTouchingDown && !isGoingUp)
        {
            ChangeDirection();
        }
        if (isTouchingWall)
        {
            if (isFacingRight)
            {
                Flip();
            }
            else if (!isFacingRight)
            {
                Flip();
            }
        }
        rb.velocity = movementSpeed * idleDirection;
    }

    private void AttackState()
    {
        if (isTouchingUp && isGoingUp)
        {
            ChangeDirection();
        }
        else if (isTouchingDown && !isGoingUp)
        {
            ChangeDirection();
        }
        if (isTouchingWall)
        {
            if (isFacingRight)
            {
                Flip();
            }
            else if (!isFacingRight)
            {
                Flip();
            }
        }
        rb.velocity = attackMovementSpeed * attackMoveDirection;
    }
    #endregion
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        idleDirection.x *= -1;
        attackMoveDirection.x *= -1;
        transform.Rotate(0, 180, 0);
    }

    private void ChangeDirection()
    {
        isGoingUp = !isGoingUp;
        idleDirection.y *= -1;
        attackMoveDirection.y *= -1;
    }
}
