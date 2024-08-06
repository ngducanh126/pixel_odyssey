using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float GetCurrentSpeed() {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        return rb != null ? rb.velocity.magnitude : 0f;
    }
        public void PlaySpawnEffect(ParticleSystem spawnEffect) {
        if (spawnEffect != null) spawnEffect.Play();
        Debug.Log("Fireball spawn visual effect played");
    }
        public void ResetFireball() {
        hasExploded = false;
        boxCollider.enabled = true;
        Debug.Log("Fireball reset to initial state");
    }
        public void SetAnimationSpeed(float speed) {
        anim.speed = speed;
        Debug.Log($"Fireball animation speed set to {speed}");
    }
        public void DestroyOnWaterContact(Collider2D other) {
        if (other.CompareTag("Water")) {
            Destroy(gameObject);
            Debug.Log("Fireball destroyed on water contact");
        }
    }
        private bool friendlyFire = false;
    public void SetFriendlyFire(bool enabled) {
        friendlyFire = enabled;
        Debug.Log($"Fireball friendly fire set to {enabled}");
    }
        private GameObject owner;
    public void SetOwner(GameObject fireballOwner) {
        owner = fireballOwner;
        Debug.Log($"Fireball owner set to {fireballOwner?.name}");
    }
        public void KnockbackOnHit(GameObject target, float force) {
        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        if (rb != null) {
            rb.AddForce((target.transform.position - transform.position).normalized * force, ForceMode2D.Impulse);
            Debug.Log($"Fireball knocks back {target.name} with force {force}");
        }
    }
        private float explosionRadius = 1f;
    public void SetExplosionRadius(float radius) {
        explosionRadius = radius;
        Debug.Log($"Fireball explosion radius set to {radius}");
    }
        private bool isPaused = false;
    public void PauseFireball() {
        isPaused = True;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Debug.Log("Fireball paused");
    }
    public void ResumeFireball(Vector2 velocity) {
        isPaused = False;
        GetComponent<Rigidbody2D>().velocity = velocity;
        Debug.Log("Fireball resumed");
    }
        public void CollideWithEnvironment(GameObject env) {
        if (env.CompareTag("Environment")) {
            anim.SetTrigger("explode");
            Destroy(gameObject, 1f);
            Debug.Log("Fireball collided with environment");
        }
    }
        private float damageMultiplier = 1f;
    public void SetDamageMultiplier(float multiplier) {
        damageMultiplier = multiplier;
        Debug.Log($"Fireball damage multiplier set to {multiplier}");
    }
        public void EnableTrailEffect(ParticleSystem trail) {
        if (trail != null) trail.Play();
        Debug.Log("Fireball trail effect enabled");
    }
        public void SetLifetime(float seconds) {
        Destroy(gameObject, seconds);
        Debug.Log($"Fireball will be destroyed after {seconds} seconds");
    }
        public void ToggleSoundMute(bool mute) {
        fireHitAudioSource.mute = mute;
        Debug.Log($"Fireball sound mute set to {mute}");
    }
        public void SlowMotion(float factor) {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) {
            rb.velocity *= factor;
            Debug.Log($"Fireball slow motion factor: {factor}");
        }
    }
        public void ChangeColorOnImpact(Color color) {
        GetComponent<SpriteRenderer>().color = color;
        Debug.Log($"Fireball changed color to {color}");
    }
        public void BounceOnGround() {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null && rb.velocity.y < 0) {
            rb.velocity = new Vector2(rb.velocity.x, 8f);
            Debug.Log("Fireball bounced on ground");
        }
    }
        public void Launch(Vector2 direction, float force) {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) {
            rb.velocity = direction.normalized * force;
            Debug.Log($"Fireball launched with force {force}");
        }
    }
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
