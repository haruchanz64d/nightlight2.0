using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Magus : MonoBehaviour
{
    private float maxHealth = 100f;
    private float currentHealth;
    [SerializeField] private Image healthBar;
    private Player player;
    private Rigidbody2D rb;
    private Animator animator;
    [Header("HP Drain")]
    private float hpDrainDMG = 0.5f;
    private float hpDrainTickInterval = 2.5f;
    private float hpDrainTimer = 0f;
    [Header("Movement")]
    private float movementSpeed = 2.5f;
    private float summonInterval = 5f;
    private float summonTimer = 0f;
    [SerializeField] private Transform[] waypoints;
    private int currentWaypointIndex;
    [SerializeField] private SceneHandler sceneHandler;
    [SerializeField] private GameObject darkOrbPrefab;
    [SerializeField] private Transform[] darkOrbSpawnpoints;
    [SerializeField] private GameObject scuttlePrefab;

    private void Awake()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void FixedUpdate()
    {
        if (currentHealth <= 0f)
        {
            DestroyMagus();
        }

        HPDrainOnUpdate();
        HandleMovement();
    }

    private void HandleMovement()
    {
        float adjustedMovementSpeed = movementSpeed;

        adjustedMovementSpeed += (maxHealth - currentHealth) * 0.05f;

        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.5f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, adjustedMovementSpeed * Time.deltaTime);

        summonTimer += Time.deltaTime;
        if (summonTimer >= summonInterval)
        {
            SummonDarkOrb();
            summonTimer = 0f;
        }
    }

    private void SummonDarkOrb()
    {
        rb.velocity = Vector2.zero;
        animator.Play("Attack");

        int randomSpawnIndex = Random.Range(0, darkOrbSpawnpoints.Length);
        Transform chosenSpawnpoint = darkOrbSpawnpoints[randomSpawnIndex];
        Instantiate(darkOrbPrefab, chosenSpawnpoint.position, Quaternion.identity);

        if (currentHealth <= 50f)
        {
            int numberOfOrbs = Random.Range(0, darkOrbSpawnpoints.Length);
            for (int i = 0; i < numberOfOrbs; i++)
            {
                darkOrbPrefab.GetComponent<Rigidbody2D>().gravityScale *= 2f;
                int randomIndex = Random.Range(0, darkOrbSpawnpoints.Length);
                Transform spawnPoint = darkOrbSpawnpoints[randomIndex];
                Instantiate(darkOrbPrefab, spawnPoint.position, Quaternion.identity);
            }

            int numberOfScuttles = Random.Range(1, 5);
            for (int i = 0; i < numberOfScuttles; i++)
            {
                Vector2 randomPosition = new Vector2(transform.position.x + Random.Range(-5f, 5f), transform.position.y + Random.Range(2f, 4f));
                Instantiate(scuttlePrefab, randomPosition, Quaternion.identity);
            }
            summonInterval *= 0.5f;
        }
        summonTimer = Mathf.RoundToInt(summonTimer);
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
    public void DestroyMagus()
    {
        StartCoroutine(PlayAnimationBeforeDestroy());
        player.IsMagusDefeated = true;
        Destroy(gameObject);
        sceneHandler.LoadScene();
    }

    private IEnumerator PlayAnimationBeforeDestroy()
    {
        animator.Play("Death");
        yield return new WaitForSeconds(5.0f);
    }
}
