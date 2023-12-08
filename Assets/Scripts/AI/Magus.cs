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
    private float hpDrainDMG = 10f;
    private float hpDrainTickInterval = 1.0f;
    private float hpDrainTimer = 0f;
    private bool isDead = false;

    private void Start()
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
    }

    private IEnumerator PlayAnimationBeforeDestroy()
    {
        animator.Play("Death");
        yield return new WaitForSeconds(5.0f);
    }
}
