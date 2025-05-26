using UnityEngine;
using System.Collections;

public class Piloting : MonoBehaviour
{
    public float speed = 5f;
    public float leftBoundary = -7f;
    public float rightBoundary = 7f;

    public Sprite idleSprite;
    public Sprite dashForwardSprite;
    public Sprite dashRetreatSprite;
    public Sprite duckSprite;

    public float dashDistance = 2f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public float duckDuration = 0.5f;

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;
    private bool isBusy = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (isBusy) return;

        Vector3 currentPosition = transform.position;

        // Movement
        if (Input.GetKey(KeyCode.A) && currentPosition.x > leftBoundary)
            transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.D) && currentPosition.x < rightBoundary)
            transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Dash Right
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(HandleDash(Vector3.right, dashForwardSprite));
        }

        // Dash Left
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartCoroutine(HandleDash(Vector3.left, dashRetreatSprite));
        }

        // Duck
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(HandleDuck());
        }

        // Clamp position
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftBoundary, rightBoundary),
            transform.position.y,
            transform.position.z
        );
    }

    IEnumerator HandleDash(Vector3 direction, Sprite dashSprite)
    {
        isBusy = true;
        spriteRenderer.sprite = dashSprite;

        float elapsed = 0f;
        float movePerFrame = dashDistance / dashDuration;

        while (elapsed < dashDuration)
        {
            transform.Translate(direction * movePerFrame * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        spriteRenderer.sprite = idleSprite;
        yield return new WaitForSeconds(dashCooldown);
        isBusy = false;
    }

    IEnumerator HandleDuck()
    {
        isBusy = true; 
        Animator animator = GetComponent<Animator>(); 
        animator.enabled = false; 
        boxCollider.isTrigger = false;
        spriteRenderer.sprite = duckSprite;

        float elapsed = 0f;

        while (elapsed < dashDuration)
        {           
            elapsed += Time.deltaTime;
            yield return null;
        }

        animator.enabled = true;
        boxCollider.isTrigger = true;
        spriteRenderer.sprite = idleSprite;
        yield return new WaitForSeconds(dashCooldown);
        isBusy = false;
    }
}
