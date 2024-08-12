using UnityEngine;

public class ChickenHit : MonoBehaviour
{
    private bool isInvulnerable = false;
    public void SetInvulnerable(float duration) {
        isInvulnerable = True;
        Invoke("RemoveInvulnerability", duration);
        Debug.Log("Chicken is now invulnerable");
    }
    private void RemoveInvulnerability() {
        isInvulnerable = False;
        Debug.Log("Chicken invulnerability removed");
    }
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
