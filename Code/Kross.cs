using UnityEngine;

public class Kross : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite krossSprite;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public float cooldownTime = 0.7f; // Cooldown time in seconds
    private float cooldownTimer = 0f; // Tracks cooldown state 
    private BoxCollider2D BoxCollider2D; // Hitbox
    public float KrossDuration = 0.3f; // How long the Kross lasts 
    private float KrossTimer = 0f;
    private bool isKrossing = false;
    public float AttackDamage = 7f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // If cooldown is over, allow punch
        if (Input.GetKeyDown(KeyCode.K) && cooldownTimer <= 0)
        {
            StartKrossing();
        }
        if (isKrossing)
        {
            KrossTimer -= Time.deltaTime;
            if (KrossTimer <= 0)
            {
                StopKrossing();
            }
        }
    }

    void StartKrossing()
    {
        isKrossing = true;
        spriteRenderer.sprite = krossSprite;
        animator.enabled = false;
        KrossTimer = KrossDuration;
        Debug.Log("Krossing");
    } 
    void StopKrossing()
    {
        isKrossing = false; 
        spriteRenderer.sprite = idleSprite; 
        animator.enabled = true;
        Debug.Log("Stopped Krossing");
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