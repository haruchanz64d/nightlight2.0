using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stingbite : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private float destroyAfterDelay;
    [SerializeField] private bool isMovingRight = false;
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isDead = false;

    private RaycastHit2D hit;
    private Vector2 rayDirection;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rayDirection = isMovingRight ? Vector2.right : Vector2.left;
    }

    private void Update()
    {
        if (player == null)
        {
            return;
        }

        float distanceToPlayer = Vector2.Distance(player.position, transform.position);

        // Raycast check
        if (Physics2D.Raycast(transform.position, rayDirection, Mathf.Infinity, LayerMask.GetMask("Player")))
        {
            animator.Play("Attack");
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
        }
        else if (distanceToPlayer > 0f)
        {
            animator.Play("Patrol");
        }

        Flip();
    }

    private void Flip()
    {
        if (transform.position.x > player.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0f);
            rayDirection = Vector2.left;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
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
