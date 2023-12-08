using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scuttle : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    private bool isMovingRight = true;

    [SerializeField] private Transform groundCheck;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isDead = false;
    [SerializeField] private LayerMask layer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (isDead) return;
        transform.Translate(Vector2.right * movementSpeed * Time.deltaTime);

        RaycastHit2D hit2D = Physics2D.Raycast(groundCheck.position, Vector2.down, 2.0f, layer);
        if (hit2D.collider == false)
        {
            if (!IsGrounded()) return;
            if (isMovingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                isMovingRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isMovingRight = true;
            }
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

    private bool IsGrounded() { return rb.IsTouchingLayers(layer); }
}
