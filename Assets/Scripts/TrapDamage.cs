using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    private bool isPaused = False;
    public void PauseTrap() {
        isPaused = True;
        Debug.Log("Trap paused");
    }
    public void ResumeTrap() {
        isPaused = False;
        Debug.Log("Trap resumed");
    }
        public void PlayVisualEffect(ParticleSystem effect) {
        if (effect != null) effect.Play();
        Debug.Log("Trap visual effect played on trigger");
    }
        public void ActivateWithDelay(float delay) {
        Invoke("ActivateTrap", delay);
        Debug.Log($"Trap will activate after {delay} seconds");
    }
    private void ActivateTrap() {
        gameObject.SetActive(true);
        Debug.Log("Trap activated after delay");
    }
        public void SetTrapVolume(float volume) {
        hitTrapAudioSource.volume = Mathf.Clamp01(volume);
        Debug.Log($"Trap sound volume set to {volume}");
    }
        public void IgnoreInvulnerablePlayers(Collider2D collision) {
        var ph = collision.GetComponent<PlayerHealth>();
        if (ph != null && ph.IsInvulnerable()) {
            Debug.Log("Trap ignored invulnerable player");
            return;
        }
    }
        public int GetActivationCount() {
        return activationCount;
    }
        public void SetCustomActivationSound(AudioClip clip) {
        if (clip != null) hitTrapAudioSource.clip = clip;
        Debug.Log("Trap custom activation sound set");
    }
        public void ShowWarningUI(GameObject warningUI) {
        if (warningUI != null) warningUI.SetActive(true);
        Debug.Log("Trap warning UI shown");
    }
        public void RearmTrapAfterCooldown(float cooldown) {
        Invoke("RearmTrap", cooldown);
        Debug.Log($"Trap will rearm after {cooldown} seconds");
    }
    private void RearmTrap() {
        gameObject.SetActive(true);
        Debug.Log("Trap rearmed");
    }
        public void DisableTrapAfterExit(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            gameObject.SetActive(false);
            Debug.Log("Trap disabled after player exit");
        }
    }
        private float trapDamage = 25f;
    public void SetTrapDamage(float damage) {
        trapDamage = damage;
        Debug.Log($"Trap damage set to {damage}");
    }
        public void MuteTrapAudio(bool mute) {
        hitTrapAudioSource.mute = mute;
        Debug.Log($"Trap audio mute set to {mute}");
    }
        private float lastActivatedAt = 0f;
    public void LogActivationTime() {
        lastActivatedAt = Time.time;
        Debug.Log($"Trap activated at {lastActivatedAt}");
    }
        private GameObject trapOwner;
    public void SetTrapOwner(GameObject owner) {
        trapOwner = owner;
        Debug.Log($"Trap owner set to {owner?.name}");
    }
        public void DestroyTrap() {
        Destroy(gameObject);
        Debug.Log("Trap destroyed by player");
    }
        public void ResetTrap() {
        activationCount = 0;
        gameObject.SetActive(true);
        Debug.Log("Trap reset to default state");
    }
        public void FlashRedOnActivate(SpriteRenderer sr) {
        if (sr != null) {
            sr.color = Color.red;
            Invoke("ResetColor", 0.2f);
            Debug.Log("Trap flashes red on activation");
        }
    }
    private void ResetColor() {
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.color = Color.white;
    }
        private int activationCount = 0;
    public void DeactivateAfterUses(int maxUses) {
        activationCount += 1;
        if (activationCount >= maxUses) {
            gameObject.SetActive(false);
            Debug.Log("Trap deactivated after max uses");
        }
    }
        private float trapCooldown = 2f;
    private float lastActivationTime = -10f;
    public bool CanActivateTrap() {
        return Time.time - lastActivationTime > trapCooldown;
    }
        public void PlayUniqueTriggerSound(AudioClip clip) {
        if (clip != null) {
            hitTrapAudioSource.PlayOneShot(clip);
            Debug.Log("Trap played unique trigger sound");
        }
    }
        [SerializeField] private AudioSource hitTrapAudioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (!playerHealth.IsInvulnerable())
            {
                playerHealth.TakeDamage(25f);
                hitTrapAudioSource.Play();
            }
        }
    }
}
