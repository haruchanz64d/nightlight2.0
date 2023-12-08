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
    private float hpDrainDMG = 1.5f;
    private float hpDrainTickInterval = 2.0f;
    private float hpDrainTimer = 0f;
    [Header("Movement")]
    private bool isMovingRight = true;
    private float movementSpeed = 2f;
    private float summonInterval = 5f;
    private float summonTimer = 0f;
    private float minX = -5f;
    private float maxX = 5f;
    [SerializeField] private SceneHandler sceneHandler;
    [SerializeField] private GameObject darkOrbPrefab;
    [SerializeField] private Transform[] darkOrbSpawnpoint;

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
        if (isMovingRight)
        {
            transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);
        }

        if (transform.position.x >= maxX)
        {
            isMovingRight = false;
        }
        else if (transform.position.x <= minX)
        {
            isMovingRight = true;
        }

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
        int randomIndex = Random.Range(0, darkOrbSpawnpoint.Length);
        Transform chosenSpawnpoint = darkOrbSpawnpoint[randomIndex];
        Instantiate(darkOrbPrefab, chosenSpawnpoint.position, Quaternion.identity);
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
