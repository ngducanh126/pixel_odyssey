using UnityEngine;

public class ChickenHit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Trap")) {
            Destroy(gameObject);
            Debug.Log("Chicken destroyed by trap");
        }
    }
        private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the "Fireball" tag
        if (collision.gameObject.CompareTag("Fireball"))
        {
            Destroy(gameObject); // Destroy the chicken
        }
    }
}
