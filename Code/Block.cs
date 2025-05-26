using UnityEngine;

public class Block : MonoBehaviour
{
    public Sprite idleSprite;
    public Sprite blockSprite;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BoxCollider2D boxCollider;
    public float blockDuration = 0.5f; // How long the block lasts
    private float blockTimer = 0f;
    private bool isBlocking = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && !isBlocking)
        {
            StartBlocking();
        }

        if (isBlocking)
        {
            blockTimer -= Time.deltaTime;
            if (blockTimer <= 0)
            {
                StopBlocking();
            }
        }
    }

    void StartBlocking()
    {
        isBlocking = true;
        spriteRenderer.sprite = blockSprite;
        animator.enabled = false;
        boxCollider.isTrigger = false; // Set collider as a trigger during block
        blockTimer = blockDuration;
        Debug.Log("Blocking");
    }

    void StopBlocking()
    {
        isBlocking = false;
        spriteRenderer.sprite = idleSprite;
        animator.enabled = true;
        boxCollider.isTrigger = true; // Revert collider to normal after blocking
        Debug.Log("Stopped Blocking");
    }
}