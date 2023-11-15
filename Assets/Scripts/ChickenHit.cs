using UnityEngine;

public class ChickenHit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collided object has the "Fireball" tag
        if (collision.gameObject.CompareTag("Fireball"))
        {
            Destroy(gameObject); // Destroy the chicken
        }
    }
}
