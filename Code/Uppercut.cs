using UnityEngine;

public class Uppercut : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite UppercutSprite;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public float cooldownTime = 0.7f; // Cooldown time in seconds
    private float cooldownTimer = 0f; // Tracks cooldown state 
    private BoxCollider2D BoxCollider2D; // Hitbox
    public float UppercutDuration = 0.3f; // How long the Kross lasts 
    private float UppercutTimer = 0f;
    private bool isStriking = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // If cooldown is over, allow punch
        if (Input.GetKeyDown(KeyCode.U) && cooldownTimer <= 0)
        {
            StartUppercut();
        }
        if (isStriking)
        {
            UppercutTimer -= Time.deltaTime;
            if (UppercutTimer <= 0)
            {
                StopUppercut();
            }
        }
    }

    void StartUppercut()
    {
        isStriking = true;
        spriteRenderer.sprite = UppercutSprite;
        animator.enabled = false;
        UppercutTimer = UppercutDuration;
        Debug.Log("Striking");
    }
    void StopUppercut()
    {
        isStriking = false;
        spriteRenderer.sprite = idleSprite;
        animator.enabled = true;
        Debug.Log("Stopped Striking");
    }
}