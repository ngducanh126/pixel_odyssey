using UnityEngine;

public class ChickenDamage : MonoBehaviour
{
    public void DeactivateAfterUses(int maxUses) {
        activationCount++;
        if (activationCount >= maxUses) {
            gameObject.SetActive(false);
            Debug.Log("Trap deactivated after max uses");
        }
    }
        private int activationCount = 0;
    public int GetActivationCount() {
        return activationCount;
    }
        public void SetTrapVolume(float volume) {
        hitTrapAudioSource.volume = Mathf.Clamp01(volume);
        Debug.Log($"Trap sound volume set to {volume}");
    }
        public void ResetTrap() {
        trapDamage = 25f;
        damageMultiplier = 1f;
        gameObject.SetActive(true);
        Debug.Log("Trap reset to default state");
    }
        private GameObject trapOwner;
    public void SetTrapOwner(GameObject owner) {
        trapOwner = owner;
        Debug.Log($"Trap owner set to {owner?.name}");
    }
        private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, GetComponent<Collider2D>().bounds.size);
    }
        private float damageMultiplier = 1f;
    public void SetDamageMultiplier(float multiplier) {
        damageMultiplier = multiplier;
        Debug.Log($"Trap damage multiplier set to {multiplier}");
    }
        private GameObject lastPlayerHit;
    public GameObject GetLastPlayerHit() {
        return lastPlayerHit;
    }
        public void ShowTrapWarningUI() {
        Debug.Log("Trap warning UI shown to player");
    }
        public void ManualTriggerTrap(GameObject player) {
        if (player.CompareTag("Player")) {
            PlayerHealth ph = player.GetComponent<PlayerHealth>();
            if (ph != null && !ph.IsInvulnerable()) {
                ph.TakeDamage(25f);
                hitTrapAudioSource.Play();
                Debug.Log("Trap manually triggered on player");
            }
        }
    }
        private float trapCooldown = 2f;
    private float lastActivationTime = -10f;
    public bool CanActivateTrap() {
        return Time.time - lastActivationTime > trapCooldown;
    }
        public bool IsTrapActive() {
        return gameObject.activeSelf;
    }
        public void ToggleTrapAudioMute(bool mute) {
        hitTrapAudioSource.mute = mute;
        Debug.Log($"Trap audio mute set to {mute}");
    }
        public void RespawnTrap(Vector3 position) {
        transform.position = position;
        gameObject.SetActive(true);
        Debug.Log($"Trap respawned at {position}");
    }
        public void DisableTrap() {
        gameObject.SetActive(false);
        Debug.Log("Trap disabled after activation");
    }
        public void SetTrapDamage(float damage) {
        trapDamage = damage;
        Debug.Log($"Trap damage set to {damage}");
    }
        public void ShowTrapActivatedEffect() {
        // Example: play particle system or change sprite
        Debug.Log("Trap visual feedback: activated");
    }
        private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            hitTrapAudioSource.Stop();
            Debug.Log("Player left trap area, sound stopped");
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
