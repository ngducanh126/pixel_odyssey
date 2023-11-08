using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public float fireSpeed = 2f; 
    private int jumpCount = 0;
    private int maxJump = 2; 
    private Rigidbody2D rb;
    private int score = 0;

    public GameObject firePrefab; 
    public Vector2 fireOffset = new Vector2(1.0f, 0);

    private float fireCooldown = 2f; // Cooldown duration in seconds
    private float lastFireTime = -2f; // Initialize to allow firing immediately at start

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
        Jump();

        // Check for fire input and cooldown
        if (Input.GetKeyDown(KeyCode.F) && Time.time - lastFireTime >= fireCooldown)
        {
            SpawnFire();
            lastFireTime = Time.time; // Update the last fire time to the current time
        }
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && jumpCount < maxJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // Reset y velocity before applying new force
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumpCount++;
        }
    }

    void SpawnFire()
    {
        // Spawn the fire object at the player's position + offset
        GameObject fireInstance = Instantiate(firePrefab, (Vector2)transform.position + fireOffset, Quaternion.identity);
        fireInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(fireSpeed, 0); // Set the velocity of the spawned fire
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Terrain"))
        {
            jumpCount = 0; // Reset jump counter when the player touches the ground
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UIManager.Instance.UpdateScore(score);

    }
}
