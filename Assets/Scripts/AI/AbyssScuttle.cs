using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbyssScuttle : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 6.5f;
    private SpriteRenderer sr;
    private GameObject player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isDead = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (isDead) return;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);

        if (player.transform.position.x < transform.position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            float yOffset = collision.transform.position.y - transform.position.y;
            if (yOffset > 0.1f)
            {
                animator.Play("Death");
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                GetComponent<Collider2D>().enabled = false;
                isDead = true;

                collision.gameObject.GetComponent<Player>().GetEnemyKillCount += 1;

                Destroy(gameObject);
            }
            else
            {
                collision.gameObject.GetComponent<Player>().DestroyAndRespawn();
            }
        }
    }
}
