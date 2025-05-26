using System;
using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class Hit : MonoBehaviour
{
    // UI Health Bar
    public Slider healthBar;

    // Health system
    public float maxHealth = 100f;
    private float currentHealth;
    public float damageTaken = 10f;

    // Sprites for hit animation
    public Sprite idleSprite;
    public Sprite hitSprite;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BoxCollider2D boxCollider;

    // Stun system
    public float hitDuration = 0.5f;
    private float hitTimer = 0f;
    private bool isHit = false;

    void Start()
    {
        // Initialize components
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Set full health at start
        currentHealth = maxHealth;

        // Update health bar UI (in case it wasn't full)
        if (healthBar != null)
            healthBar.value = currentHealth;
    }

    void Update()
    {
        // Handle stun timer
        if (isHit)
        {
            hitTimer -= Time.deltaTime;
            if (hitTimer <= 0)
            {
                StopHit();
            }
        }
    }

    public void TakeDamage(Collider2D enemyCollider)
    {
        if (isHit) return; // Prevents multiple hits while stunned

        if (boxCollider.bounds.Intersects(enemyCollider.bounds)) // Check collision
        {
            isHit = true;
            currentHealth -= damageTaken;

            // Update health bar
            if (healthBar != null)
                healthBar.value = currentHealth;

            spriteRenderer.sprite = hitSprite; // Change to hit sprite
            animator.enabled = false; // Disable animations while stunned

            hitTimer = hitDuration;

            Debug.Log($"Fighter has taken damage! Current Health: {currentHealth}");

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void StopHit()
    {
        isHit = false;
        spriteRenderer.sprite = idleSprite;
        animator.enabled = true; // Re-enable animations
        Debug.Log("Recovered from hit.");
    }

    void Die()
    {
        Debug.Log("You has been defeated.");
        Destroy(gameObject); // Replace with a death animation or respawn system later
    }

    internal void TakeDamage(float attackDamage)
    {
        throw new NotImplementedException();
    }
}
