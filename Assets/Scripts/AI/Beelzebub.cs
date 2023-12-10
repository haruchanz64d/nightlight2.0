using UnityEngine;
using System.Collections;

public class Beelzebub : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;
    private SpriteRenderer sr;

    [SerializeField] private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isDead = false;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (isDead) return;

        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.5f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, movementSpeed * Time.deltaTime);

        if (waypoints[currentWaypointIndex].transform.position.x > transform.position.x)
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
