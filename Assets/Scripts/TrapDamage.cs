using UnityEngine;

public class TrapDamage : MonoBehaviour
{
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
