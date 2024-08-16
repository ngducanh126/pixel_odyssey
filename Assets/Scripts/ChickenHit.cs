using UnityEngine;

public class ChickenHit : MonoBehaviour
{
    public void Stun(float duration) {
        StartCoroutine(StunRoutine(duration));
    }
    private System.Collections.IEnumerator StunRoutine(float duration) {
        Debug.Log("Chicken stunned");
        yield return new WaitForSeconds(duration);
        Debug.Log("Chicken recovered from stun");
    }
        private int health = 1;
    public void SetHealth(int value) {
        health = value;
        Debug.Log($"Chicken health set to {value}");
    }
    public void TakeDamage(int amount) {
        health -= amount;
        if (health <= 0) Destroy(gameObject);
        Debug.Log($"Chicken took {amount} damage");
    }
        public void PlayHitSound(AudioSource audio) {
        if (audio != null) audio.Play();
        Debug.Log("Chicken hit sound played");
    }
        public void Knockback(Vector2 force) {
        var rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.AddForce(force, ForceMode2D.Impulse);
        Debug.Log($"Chicken knocked back with force {force}");
    }
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
