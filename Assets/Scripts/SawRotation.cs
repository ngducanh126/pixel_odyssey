using UnityEngine;

public class SawRotation : MonoBehaviour
{
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
