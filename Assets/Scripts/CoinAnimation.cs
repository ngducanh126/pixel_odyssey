using UnityEngine;

public class CoinAnimation : MonoBehaviour
{
    public float rotationSpeed = 100f; // Rotation speed
    public float bounceHeight = 0.5f; // Max height of bounce
    public float bounceSpeed = 2f; // Speed of bounce

    private Vector3 startPosition;
    private float timer;

    void Start()
    {
        // Save the starting position of the coin
        startPosition = transform.position;
    }

    void Update()
    {
        // Handle rotation
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        // Handle bouncing
        timer += bounceSpeed * Time.deltaTime;
        float height = Mathf.Sin(timer) * bounceHeight;
        transform.position = startPosition + new Vector3(0, height, 0);
    }
}
