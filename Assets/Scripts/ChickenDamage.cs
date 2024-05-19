using UnityEngine;

public class ChickenDamage : MonoBehaviour
{
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
