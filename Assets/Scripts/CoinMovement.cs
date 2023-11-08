using UnityEngine;

public class CoinMovement : MonoBehaviour
{
    public float moveSpeed = 4.0f; // Speed at which the coin moves left

    void Update()
    {
        // Move the coin left
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check for collision with the player
        if (other.gameObject.CompareTag("Player"))
        {
            // // Increment the score
            // ScoreManager.Instance.AddScore(1);
            // Destroy the coin
            Destroy(gameObject);
        }
    }
}
