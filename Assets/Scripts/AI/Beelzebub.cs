using UnityEngine;
using System.Collections;

public class Beelzebub : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed = 2.0f;
    private float moveInterval = 2.0f; // Time between movement changes
    private bool facingRight = true;
    private float nextMovementTime = 0.0f; // Time of next movement change
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        nextMovementTime = Time.time + moveInterval;
    }

    void Update()
    {
        if (Time.time >= nextMovementTime)
        {
            MoveRandomly();
            FlipFacingDirection();
            nextMovementTime = Time.time + moveInterval;
        }
    }

    private void MoveRandomly()
    {
        float direction = Random.Range(-1.0f, 1.0f);
        rb.velocity = new Vector2(direction * speed, rb.velocity.y);
    }

    private void FlipFacingDirection()
    {
        if (rb.velocity.x < 0 && facingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180f, 0f);
            facingRight = false;
        }
        else if (rb.velocity.x > 0 && !facingRight)
        {
            transform.rotation = Quaternion.Euler(0, 0f, 0f);
            facingRight = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.Play("Death");
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            GetComponent<Collider2D>().enabled = false;
            collision.gameObject.GetComponent<Player>().DestroyAndRespawn();
            DestroyEnemy();
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
