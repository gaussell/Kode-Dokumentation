using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite hookSprite;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float cooldownTimer = 0f;
    public float cooldownTime = 0.5f;
    public float AttackDamage = 12f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.enabled = false; // Disable hitbox by default
    }

    void Update()
    {
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.H) && cooldownTimer <= 0)
        {
            StartCoroutine(PunchRoutine());
            cooldownTimer = cooldownTime;
            Debug.Log("Hook punch executed!");
        }
    }

    IEnumerator PunchRoutine()
    {
        animator.enabled = false;
        spriteRenderer.sprite = hookSprite;
        boxCollider.enabled = true;

        yield return new WaitForSeconds(0.2f); // Hitbox active

        boxCollider.enabled = false;
        spriteRenderer.sprite = idleSprite;
        animator.enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Hook landed!");
            other.GetComponent<Hit>()?.TakeDamage(AttackDamage);
        }
    }
}
