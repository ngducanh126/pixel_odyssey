using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject fireballPrefab; // Assign this in the inspector with your fireball prefab
    public float fireballSpeed = 10f; // Adjust the speed as needed
    [SerializeField] private Transform firePoint;

    private Animator animator;
    private SpriteRenderer spriteRenderer; // Add this

    void Start()
    {
        // Get the Animator and SpriteRenderer components attached to the player
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
    }

    void Update()
    {
        // Check if the 'S' key is pressed
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Trigger the attack animation
            animator.SetTrigger("attack");

            // Check the direction the player is facing to determine the firePoint position
            float directionMultiplier = spriteRenderer.flipX ? -1f : 1f;
            firePoint.localPosition = new Vector3(Mathf.Abs(firePoint.localPosition.x) * directionMultiplier, firePoint.localPosition.y, firePoint.localPosition.z);

            // Instantiate the fireball at the firePoint's position
            GameObject fireballInstance = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);

            // If shooting to the left, flip the fireball's sprite to face left
            if (spriteRenderer.flipX)
            {
                fireballInstance.transform.localScale = new Vector3(-1 * Mathf.Abs(fireballInstance.transform.localScale.x), 
                                                                    fireballInstance.transform.localScale.y, 
                                                                    fireballInstance.transform.localScale.z);
            }

            // Determine the direction to fire the fireball based on the sprite's flipX
            Vector2 fireDirection = spriteRenderer.flipX ? Vector2.left : Vector2.right;

            // Make the fireball move in the determined direction
            Rigidbody2D rb = fireballInstance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = fireDirection * fireballSpeed;
            }
        }
    }



}