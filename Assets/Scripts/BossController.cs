using UnityEngine;
using UnityEngine.UI;
public class BossController : MonoBehaviour
{
    public LayerMask playerLayer; 
    public float trackingRange = 10f;
    public float movementSpeed = 5f;
    public int maxHealth = 100; 
    public Slider healthBar;

    private Transform player;
    private bool isAlive = true;
    private int currentHealth;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Physics2D.IgnoreLayerCollision(gameObject.layer, playerLayer); 
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isAlive)
        {
            TrackPlayer();
        }
        healthBar.value = currentHealth;
    }

    void TrackPlayer()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= trackingRange)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * movementSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isAlive) return;

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        isAlive = false;
        Destroy(gameObject);
        LevelClear(); // Call the function to clear the level
    }

    void LevelClear()
    {
        // Add code here to handle level clearing (e.g., load next level, show victory screen, etc.)
        Debug.Log("Level Cleared!");
    }
}
