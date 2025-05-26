using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 5f; // Speed of movement
    public float leftBoundary = -8f; // Adjust for your screen's left edge
    public float rightBoundary = 8f; // Adjust for your screen's right edge

    void Start()
    {
        Debug.Log("Dempsey is ready to stay on screen!");
    }

    void Update()
    {
        // Get the current position of Dempsey
        Vector3 currentPosition = transform.position;

        // Moving left (A key) within boundary
        if (Input.GetKey(KeyCode.A) && currentPosition.x > leftBoundary)
        {
            transform.Translate(-speed * Time.deltaTime, 0, 0);
        }

        // Moving right (D key) within boundary
        if (Input.GetKey(KeyCode.D) && currentPosition.x < rightBoundary)
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }

        // Clamp position as a safety net to prevent going out of bounds
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftBoundary, rightBoundary),
            transform.position.y,
            transform.position.z
        );
    }
}
