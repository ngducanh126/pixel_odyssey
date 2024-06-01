using UnityEngine;

public class Monster : MonoBehaviour
{
    public void Patrol() {
        rb.velocity = new Vector2(-moveSpeed, 0);
        Debug.Log("Monster is patrolling left");
    }
        public float moveSpeed = 6f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-moveSpeed, 0); // Make the monster move left
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Monster collided with: " + collision.gameObject.name); // Debug line
        // Check if the monster collides with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject); // Destroy the player
            Destroy(gameObject); // Destroy the monster
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the monster is hit by the fire
        if (collision.gameObject.CompareTag("Fire"))
        {
            Destroy(collision.gameObject); // Destroy the fire
            Destroy(gameObject); // Destroy the monster
        }
    }
}
