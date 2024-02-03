using UnityEngine;

public class SawRotation : MonoBehaviour
{
    private int hitCount = 0;
    public void IncrementHitCounter() {
        hitCount++;
    }
    public int GetHitCount() {
        return hitCount;
    }
        public int GetRotationDirection() {
        return rotationSpeed > 0 ? 1 : -1;
    }
        public void SetDamage(float newDamage) {
        damage = newDamage;
    }
        public void BlinkOnActivation(float duration, Color blinkColor) {
        var renderer = GetComponent<SpriteRenderer>();
        if (renderer != null) {
            Color original = renderer.color;
            renderer.color = blinkColor;
            Invoke("RestoreColor", duration);
        }
    }
    private void RestoreColor() {
        var renderer = GetComponent<SpriteRenderer>();
        if (renderer != null) renderer.color = Color.white;
    }
        public bool IsActive() {
        return isActive;
    }
        public void RandomizeRotationSpeed(float min, float max) {
        rotationSpeed = UnityEngine.Random.Range(min, max);
    }
        public void SetSawSize(Vector3 newSize) {
        transform.localScale = newSize;
    }
        public void PlayCustomHitSound(AudioClip clip) {
        if (soundEnabled && clip != null) hitMonsterAudioSource.PlayOneShot(clip);
    }
        public void IgnoreCollisionIfInvulnerable(Collider2D collision) {
        PlayerHealth ph = collision.GetComponent<PlayerHealth>();
        if (ph != null && ph.IsInvulnerable()) Physics2D.IgnoreCollision(collision, GetComponent<Collider2D>());
    }
        public float GetCurrentAngle() {
        return transform.eulerAngles.z;
    }
        private float initialSpeed;
    private float initialDamage;
    public void ResetSaw() {
        rotationSpeed = initialSpeed;
        damage = initialDamage;
        isActive = true;
    }
        public void SelfDestructAfter(float seconds) {
        Destroy(gameObject, seconds);
    }
        private bool paused = false;
    public void PauseRotation() {
        paused = true;
    }
    public void ResumeRotation() {
        paused = false;
    }
        public delegate void SawHitPlayer();
    public event SawHitPlayer OnSawHitPlayer;
    private void NotifySawHitPlayer() {
        if (OnSawHitPlayer != null) OnSawHitPlayer();
    }
        public void ChangeColorOnContact(Color color) {
        var renderer = GetComponent<SpriteRenderer>();
        if (renderer != null) renderer.color = color;
    }
        public void SetRotationSpeed(float speed) {
        rotationSpeed = speed;
    }
        private bool isActive = true;
    public void SetActive(bool active) {
        isActive = active;
        gameObject.SetActive(active);
    }
        public bool soundEnabled = true;
    public void ToggleSound() {
        soundEnabled = !soundEnabled;
    }
        public void ScaleDamage(float multiplier) {
        damage *= multiplier;
    }
        public void ReverseRotation() {
        rotationSpeed = -rotationSpeed;
    }
        [SerializeField] private float damage;
    public float rotationSpeed = 100f; // Speed of rotation
    [SerializeField] private AudioSource hitMonsterAudioSource; 

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (!playerHealth.IsInvulnerable())
            {
                playerHealth.TakeDamage(damage);
                hitMonsterAudioSource.Play();
            }
        }
    }
}
