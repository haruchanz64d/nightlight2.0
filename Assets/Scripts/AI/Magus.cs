using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Magus : MonoBehaviour
{
    private bool isSummoningOrbs;
    private float maxHealth = 100f;
    private float currentHealth;
    [SerializeField] private Image healthBar;
    private Player player;
    private Rigidbody2D rb;
    private Animator animator;
    [Header("HP Drain")]
    private float hpDrainDMG = 2.5f;
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
        StartCoroutine(PlayAttackAnimation());

        rb.velocity = Vector2.zero;

        List<int> randomSpawnIndices = new List<int>();
        for (int i = 0; i < darkOrbSpawnpoints.Length; i++)
        {
            randomSpawnIndices.Add(Random.Range(0, darkOrbSpawnpoints.Length));
        }

        int numberOfOrbs = currentHealth <= 50f ? Random.Range(2, 5) : 1;

        for (int i = 0; i < numberOfOrbs; i++)
        {
            int randomIndex = randomSpawnIndices[i];
            Transform spawnPoint = darkOrbSpawnpoints[randomIndex];

            Instantiate(darkOrbPrefab, spawnPoint.position, Quaternion.identity).GetComponent<DarkOrb>().EnableScuttleSpawning = (currentHealth <= 50f);
        }

        summonTimer = Mathf.FloorToInt(summonTimer * (currentHealth <= 50f ? 0.5f : 1f));
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

    private IEnumerator PlayAttackAnimation()
    {
        animator.Play("Attack");
        yield return new WaitForSeconds(1.0f);
    }

    private IEnumerator PlayAnimationBeforeDestroy()
    {
        animator.Play("Death");
        yield return new WaitForSeconds(5.0f);
    }
}
