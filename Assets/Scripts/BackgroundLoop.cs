using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public float speed = 1.0f;   // Speed at which the background moves
    private Vector2 startPosition;   // To store the starting position
    private float width;   // Width of the background sprite

    private void Start()
    {
        startPosition = transform.position;
        width = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void Update()
    {
        // Move the background to the left
        float newPosition = Mathf.Repeat(Time.time * speed, width);
        transform.position = startPosition + Vector2.left * newPosition;
    }
}
