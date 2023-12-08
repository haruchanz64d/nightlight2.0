using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stingbite : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private float destroyAfterDelay;
    [SerializeField] private float moveInterval = 2.0f; // Time between movement changes
    private bool facingRight = true;
    private float nextMovementTime = 0.0f; // Time of next movement change
    private Transform player;
    [SerializeField] private Transform rayOrigin;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isDead = false;

    private RaycastHit2D hit;
    private Vector2 rayDirection;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rayDirection = facingRight ? Vector2.right : Vector2.left;
    }

    private void Update()
    {
        if (Time.time >= nextMovementTime)
        {
            MoveRandomly();
            nextMovementTime = Time.time + moveInterval;
        }

        Flip();
    }

    private void MoveRandomly()
    {
        float direction = Random.Range(-1.0f, 1.0f);
        rb.velocity = new Vector2(direction * movementSpeed, rb.velocity.y);
    }

    private void Flip()
    {
        if (rb.velocity.x < 0 && facingRight)
        {
            transform.rotation = Quaternion.Euler(0, 0f, 0f);
            facingRight = false;
            rayDirection = Vector2.left;
        }
        else if (rb.velocity.x > 0 && !facingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0f);
            facingRight = true;
            rayDirection = Vector2.right;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.transform.position.y > transform.position.y + 0.5f)
        {
            animator.Play("Death");
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            GetComponent<Collider2D>().enabled = false;
            isDead = true;

            collision.gameObject.GetComponent<Player>().GetEnemyKillCount += 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.Play("Death");
            collision.gameObject.GetComponent<Player>().DestroyAndRespawn();
        }
    }
}
