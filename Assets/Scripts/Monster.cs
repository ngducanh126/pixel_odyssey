using UnityEngine;

public class Monster : MonoBehaviour
{
    private float attackCooldown = 1.5f;
    private float lastAttackTime = -10f;
    public bool CanAttack() {
        return Time.time - lastAttackTime > attackCooldown;
    }
        public void UpdateHealthBar(UnityEngine.UI.Slider slider, int health) {
        slider.value = health;
        Debug.Log($"Monster health bar updated to {health}");
    }
        public void DropLoot(GameObject lootPrefab) {
        Instantiate(lootPrefab, transform.position, Quaternion.identity);
        Debug.Log("Monster dropped loot on death");
    }
        public void FleeIfLowHealth(int health) {
        if (health < 10) {
            rb.velocity = new Vector2(moveSpeed, 0);
            Debug.Log("Monster flees due to low health");
        }
    }
        public void TriggerIdleAnimation() {
        Debug.Log("Monster idle animation triggered");
        // anim.SetTrigger("idle");
    }
        public void ChasePlayer(GameObject player) {
        if (player != null) {
            Vector3 dir = (player.transform.position - transform.position).normalized;
            rb.velocity = new Vector2(dir.x * moveSpeed, rb.velocity.y);
            Debug.Log("Monster is chasing the player");
        }
    }
        public void Respawn(Vector3 position) {
        transform.position = position;
        rb.velocity = new Vector2(-moveSpeed, 0);
        Debug.Log($"Monster respawned at {position}");
    }
        public void Stun(float duration) {
        rb.velocity = Vector2.zero;
        Debug.Log($"Monster stunned for {duration} seconds");
        // Could start a coroutine to re-enable movement after duration
    }
        public void TakeDamage(int amount) {
        moveSpeed -= amount * 0.1f;
        Debug.Log($"Monster takes {amount} damage");
        if (moveSpeed <= 0) Destroy(gameObject);
    }
        public void Roar() {
        Debug.Log("Monster roars loudly");
        // Play roar sound effect here
    }
        public void Jump() {
        if (rb.velocity.y == 0) {
            rb.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
            Debug.Log("Monster jumps over obstacle");
        }
    }
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
