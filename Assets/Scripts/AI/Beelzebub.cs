using UnityEngine;
using System.Collections;

public class Beelzebub : MonoBehaviour
{
    [SerializeField] private float lineOfSight = 5.0f;
    [SerializeField] private float movementSpeed = 10.0f;
    [SerializeField] private float destroyAfterDelay = 0.5f;
    private Transform player;
    private Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (player == null)
            return;
        float distanceToPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceToPlayer < lineOfSight)
        {
            animator.Play("Attack");
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, movementSpeed * Time.deltaTime);
        }
        else if (distanceToPlayer > lineOfSight)
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
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && collision.transform.position.y > transform.position.y + 0.5f)
        {
            animator.Play("Death");
            collision.gameObject.GetComponent<Player>().IsDead = true;
        }
    }

    public void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
