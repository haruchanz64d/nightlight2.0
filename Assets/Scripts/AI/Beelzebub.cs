using UnityEngine;
using System.Collections;
public class Beelzebub : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float detectionDelay = 0.5f;
    [SerializeField] private float chargeSpeed = 3.5f;
    private Animator animator;
    private bool isPlayerDetected;
    private float detectionTimer;
    private float movementTimer;
    private bool isMovingRight;

    private void Start()
    {
        isPlayerDetected = false;
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        movementTimer = Time.time;
        isMovingRight = true;
    }

    private void Update()
    {
        if (isPlayerDetected)
        {
            detectionTimer += Time.deltaTime;
            if (detectionTimer >= detectionDelay)
            {
                Vector3 direction = player.transform.position - transform.position;
                transform.Translate(direction * chargeSpeed * Time.deltaTime);

                animator.SetBool("isCharging", true);
            }
        }
        else
        {
            if (Time.time - movementTimer >= 5.0f)
            {
                isMovingRight = !isMovingRight;
                transform.eulerAngles = new Vector3(0, -180, 0);
                movementTimer = Time.time;
            }

            transform.Translate(isMovingRight ? Vector2.right : Vector2.left * movementSpeed * Time.deltaTime); // Move based on direction
            animator.SetBool("isCharging", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerDetected = true;
            detectionTimer = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerDetected = false;
            detectionTimer = 0f;
            animator.SetBool("isCharging", false);
            animator.SetBool("isIdle", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("isExplodeUponImpact", true);

            StartCoroutine(DestroyAfterDelay(1.5f));
        }
    }

   private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
