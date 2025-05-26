using UnityEngine;

public class Jab : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite JabSprite;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public float cooldownTime = 0.7f; // Cooldown time in seconds
    private float cooldownTimer = 0f; // Tracks cooldown state 
    private BoxCollider2D BoxCollider2D; // Hitbox
    public float JabDuration = 0.3f; // How long the Kross lasts 
    private float JabTimer = 0f;
    private bool isJabbing = false;
    public float AttackDamage = 5f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // If cooldown is over, allow punch
        if (Input.GetKeyDown(KeyCode.J) && cooldownTimer <= 0)
        {
            StartJabbing();
        }
        if (isJabbing)
        {
            JabTimer -= Time.deltaTime;
            if (JabTimer <= 0)
            {
                StopJabbing();
            }
        }
    }

    void StartJabbing()
    {
        isJabbing = true;
        spriteRenderer.sprite = JabSprite;
        animator.enabled = false;
        JabTimer = JabDuration;
        Debug.Log("Jab");
    }
    void StopJabbing()
    {
        isJabbing = false;
        spriteRenderer.sprite = idleSprite;
        animator.enabled = true;
        Debug.Log("Stopped Jabbing");
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