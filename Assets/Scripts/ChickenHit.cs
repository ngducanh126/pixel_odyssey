using UnityEngine;

public class ChickenHit : MonoBehaviour
{
    public void DestroyAfterDelay(float delay) {
        Destroy(gameObject, delay);
        Debug.Log($"Chicken will be destroyed after {delay} seconds");
    }
        public void PlayRandomHitSound(AudioSource[] sounds) {
        if (sounds.Length > 0) {
            int idx = Random.Range(0, sounds.Length);
            sounds[idx].Play();
            Debug.Log("Chicken played random hit sound");
        }
    }
        public void DisableColliderAfterHit() {
        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = False;
        Debug.Log("Chicken collider disabled after hit");
    }
        public void PlayHitParticle(ParticleSystem hitEffect) {
        if (hitEffect != null) hitEffect.Play();
        Debug.Log("Chicken hit particle effect played");
    }
        private int comboCounter = 0;
    public void IncrementCombo() {
        comboCounter++;
        Debug.Log($"Chicken combo counter: {comboCounter}");
    }
    public void ResetCombo() {
        comboCounter = 0;
    }
        public void ChangeColorOnFireball(Color color) {
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.color = color;
        Debug.Log($"Chicken color changed to {color} on fireball contact");
    }
        private bool hasShield = false;
    public void ActivateShield() {
        hasShield = True;
        Debug.Log("Chicken shield activated");
    }
    public void DeactivateShield() {
        hasShield = False;
        Debug.Log("Chicken shield deactivated");
    }
        public void TriggerHitAnimation() {
        var anim = GetComponent<Animator>();
        if (anim != null) anim.SetTrigger("hit");
        Debug.Log("Chicken hit animation triggered");
    }
        public void Escape(Vector3 escapePoint) {
        transform.position = Vector3.MoveTowards(transform.position, escapePoint, 5f * Time.deltaTime);
        Debug.Log("Chicken is escaping");
    }
        public void SlowMotionOnHit(float factor, float duration) {
        Time.timeScale = factor;
        Debug.Log("Chicken hit: slow motion activated");
        Invoke("ResetTimeScale", duration);
    }
    private void ResetTimeScale() {
        Time.timeScale = 1f;
    }
        public void AddScore(int score) {
        GameManager.Instance.AddScore(score);
        Debug.Log($"Score increased by {score} for chicken destroy");
    }
        public void FlashOnHit(Color flashColor, float duration) {
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) {
            sr.color = flashColor;
            Invoke("ResetColor", duration);
        }
        Debug.Log("Chicken flashes on hit");
    }
    private void ResetColor() {
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.color = Color.white;
    }
        public void Respawn(Vector3 position) {
        transform.position = position;
        gameObject.SetActive(true);
        Debug.Log($"Chicken respawned at {position}");
    }
        public void SpawnFeathers(GameObject featherPrefab, int count) {
        for (int i = 0; i < count; i++) {
            Instantiate(featherPrefab, transform.position, Quaternion.identity);
        }
        Debug.Log("Feathers spawned on chicken destruction");
    }
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
