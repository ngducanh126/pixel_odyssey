using UnityEngine;

public class Fireball : MonoBehaviour
{
    private Animator anim;
    private bool hasExploded;
    [SerializeField] private AudioSource fireHitAudioSource; 
    private BoxCollider2D boxCollider;

    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
        void OnCollisionEnter2D(Collision2D collision)
    {
        // Check to ensure the explosion only happens once
        if (!hasExploded)
        {
            fireHitAudioSource.Play();
            // Trigger the explosion animation
            anim.SetTrigger("explode");
            
            // Set hasExploded to true to prevent multiple triggers
            hasExploded = true;

            // Optionally, disable the fireball's movement and collider so it stays in place for the animation
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
            }
            Collider2D collider = GetComponent<Collider2D>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            // Destroy the fireball after a short delay to allow the animation to play
            Destroy(gameObject, 1f); // Adjust the delay to match the length of your explosion animation
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Monster")) {
            hasExploded = true;
            boxCollider.enabled = false;
            anim.SetTrigger("explode");
            other.GetComponent<Boss>().health -= 10;
            Destroy(gameObject, 1f);
    }
    }
    // This method can be called by the AnimationEvent
    public void Deactivate()
    {
        // Add any deactivation logic here, e.g., disabling the GameObject
        gameObject.SetActive(false);
    }
}
