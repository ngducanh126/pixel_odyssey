using UnityEngine;

public class ChickenDamage : MonoBehaviour
{
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
