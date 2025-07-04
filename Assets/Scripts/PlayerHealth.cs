using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public delegate void InvulnerableEvent();
    public event InvulnerableEvent OnInvulnerable;
    public void NotifyInvulnerable() {
        if (OnInvulnerable != null) OnInvulnerable();
    }
        public void ClampHealth(float min, float max) {
        health = Mathf.Clamp(health, min, max);
        UpdateHealthBar();
    }
        public void SpeedBoostOnHighHealth(float threshold, float boost) {
        if (health > threshold) moveSpeed += boost;
    }
        public void ShakeCameraOnHeal(float intensity, float duration) {
        if (cameraShake != null) cameraShake.Shake(intensity, duration);
    }
        public void PoisonZoneEffect(float dps) {
        if (inHazard) TakeDamage(dps * Time.deltaTime);
    }
        public void SetHealthBarVisible(bool visible) {
        if (healthBar != null) healthBar.enabled = visible;
    }
        public void LoseHealthOnTimer(float amount, float interval) {
        StartCoroutine(TimerHealthLoss(amount, interval));
    }
    private IEnumerator TimerHealthLoss(float amount, float interval) {
        while (true) {
            TakeDamage(amount);
            yield return new WaitForSeconds(interval);
        }
    }
        public bool IsAtMaxHealth() {
        return health >= 100f;
    }
        public void PlayReviveAnimation() {
        if (anim != null) anim.SetTrigger("Revive");
    }
    public void PlayReviveSound(AudioClip clip) {
        if (clip != null) AudioSource.PlayClipAtPoint(clip, transform.position);
    }
        public void ShowFloatingDamage(float amount) {
        // Instantiate floating text prefab here
    }
        public void CollectHealthPickup(float amount) {
        Heal(amount);
    }
        public void ResetHealthBarVisuals() {
        if (healthBar != null) healthBar.color = Color.green;
    }
        public void SetHealthBarColor(Color color) {
        if (healthBar != null) healthBar.color = color;
    }
        public void PlayHealSound(AudioClip clip) {
        if (clip != null) AudioSource.PlayClipAtPoint(clip, transform.position);
    }
        private bool inHazard = false;
    public void SetHazardZone(bool enable) {
        inHazard = enable;
    }
    private void Update() {
        if (inHazard) TakeDamage(Time.deltaTime * 2f);
    }
        public delegate void Healed(float amount);
    public event Healed OnHealed;
    public void NotifyHealed(float amount) {
        if (OnHealed != null) OnHealed(amount);
    }
        public void ApplyDelayedDamage(float damage, float delay) {
        StartCoroutine(DelayedDamageRoutine(damage, delay));
    }
    private IEnumerator DelayedDamageRoutine(float damage, float delay) {
        yield return new WaitForSeconds(delay);
        TakeDamage(damage);
    }
        public void SetInvulnerable(bool enable) {
        isInvulnerable = enable;
    }
        private float armor = 0f;
    public void SetArmor(float value) {
        armor = value;
    }
    private void ApplyArmor(ref float damage) {
        damage = Mathf.Max(0, damage - armor);
    }
        public string GetHealthPercent() {
        return ((health / 100f) * 100f).ToString("F0") + "%";
    }
        public void Overheal(float extra) {
        health += extra;
        // Visual feedback could be added here
        UpdateHealthBar();
    }
        public void ReduceMaxHealth(float amount) {
        health = Mathf.Min(health, 100f - amount);
        UpdateHealthBar();
    }
        public void AddTempHealth(float tempAmount, float duration) {
        StartCoroutine(TempHealthRoutine(tempAmount, duration));
    }
    private IEnumerator TempHealthRoutine(float tempAmount, float duration) {
        health += tempAmount;
        UpdateHealthBar();
        yield return new WaitForSeconds(duration);
        health -= tempAmount;
        UpdateHealthBar();
    }
        public delegate void HealthZero();
    public event HealthZero OnHealthZero;
    private void CheckHealthZero() {
        if (health <= 0 && OnHealthZero != null) OnHealthZero();
    }
        public void RegenerateHealth(float amount, float duration) {
        StartCoroutine(RegenerateRoutine(amount, duration));
    }
    private IEnumerator RegenerateRoutine(float amount, float duration) {
        float timer = 0f;
        float perSecond = amount / duration;
        while (timer < duration) {
            Heal(perSecond * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }
        public void FullHeal() {
        health = 100f;
        UpdateHealthBar();
    }
        private bool bleeding = false;
    public void StartBleeding(float dps, float duration) {
        if (!bleeding) StartCoroutine(BleedRoutine(dps, duration));
    }
    private IEnumerator BleedRoutine(float dps, float duration) {
        bleeding = true;
        float timer = 0f;
        while (timer < duration) {
            TakeDamage(dps * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        bleeding = false;
    }
        private float shield = 0f;
    public void AddShield(float amount) {
        shield += amount;
    }
    private void AbsorbDamage(float damage) {
        if (shield > 0) {
            float absorbed = Mathf.Min(shield, damage);
            shield -= absorbed;
            damage -= absorbed;
        }
        if (damage > 0) TakeDamage(damage);
    }
        public void Revive(Vector2 position, float reviveHealth) {
        transform.position = position;
        health = reviveHealth;
        UpdateHealthBar();
    }
        public void ApplyPoison(float dps, float duration) {
        StartCoroutine(PoisonRoutine(dps, duration));
    }
    private IEnumerator PoisonRoutine(float dps, float duration) {
        float timer = 0f;
        while (timer < duration) {
            TakeDamage(dps * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }
        public Image healthBar;
    public float health = 100f; // Player's health
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    private Vector2 startPosition;
    public CameraShake cameraShake; // Reference to the CameraShake script
    private AudioSource deathSoundEffect; // Reference to the death sound audio source
    private bool isInvulnerable = false;
    [SerializeField] private AudioSource deathAudioSource; 
    [SerializeField] private AudioSource hitTrapAudioSource; 
    [SerializeField] private AudioSource hitMonsterAudioSource; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        // deathAudioSource= GetComponent<AudioSource>(); // Get the audio source component
        // hitTrapAudioSource = GetComponent<AudioSource>();
        // hitMonsterAudioSource = GetComponent<AudioSource>(); 
        startPosition = transform.position;


        if (rb == null) { Debug.LogError("Rigidbody2D component not found on the player."); }
        if (anim == null) { Debug.LogError("Animator component not found on the player."); }
        if (sprite == null) { Debug.LogError("SpriteRenderer component not found on the player."); }
    }

    void Update()
    {
        // Existing update logic
    }

    public void TakeDamage(float damage)
    {
        // Start the shake coroutine
        StartCoroutine(cameraShake.Shake(0.1f, 0.2f)); // Adjust duration and magnitude for subtlety

        if (isInvulnerable) return;
        Vector2 previousPosition = transform.position;

        health -= damage;
        Debug.Log("Health is now " + health);
        UpdateHealthBar();

        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(BecomeInvulnerable());
        }
    }

    IEnumerator BecomeInvulnerable()
    {
        isInvulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);

        // Turn red for 0.3 seconds
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.3f);

        // Rapid blinking for 1.2 seconds
        float blinkTime = 0.1f; // Time for each blink
        float blinkingDuration = 1.2f; // Total duration of blinking

        for (float i = 0; i < blinkingDuration; i += blinkTime)
        {
            sprite.enabled = !sprite.enabled; // Toggle visibility
            yield return new WaitForSeconds(blinkTime);
        }

        // Reset to normal
        sprite.enabled = true; // Ensure sprite is visible
        sprite.color = Color.white; // Reset color to white (original)
        Physics2D.IgnoreLayerCollision(10, 11, false);
        isInvulnerable = false;
    }


    void UpdateHealthBar()
    {
        float maxBarWidth = 300f;
        float currentWidth = maxBarWidth * (health / 100f);

        RectTransform rect = healthBar.rectTransform;
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWidth);
        float difference = maxBarWidth - currentWidth;
        rect.anchoredPosition = new Vector2(-difference / 2, rect.anchoredPosition.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        // if (collision.gameObject.CompareTag("Monster") && isInvulnerable == false)
        // {
        //     hitMonsterAudioSource.Play(); // Play the death sound effect
        //     TakeDamage(25f);
        // }
    }

    private void Die()
    {
        if (deathAudioSource != null)
        {
            deathAudioSource.Play(); // Play the death sound effect
        }
        rb.bodyType = RigidbodyType2D.Static;
        anim.SetTrigger("death");
    }

    public void Heal(float healAmount)
    {
        health += healAmount;
        if (health > 100f) health = 100f; // Cap the health at 100
        UpdateHealthBar();
        Debug.Log("Healed!!!!. Health is now " + health);
    }

        // In PlayerHealth script
    public void StartPowerRun(float duration)
    {
        StartCoroutine(PowerRun(duration));
    }

    private IEnumerator PowerRun(float duration)
    {
        isInvulnerable = true;
        
        // Store original color to revert back after Power Run
        Color originalColor = sprite.color;
        
        // Set player to semi-transparent to indicate Power Run state
        sprite.color = new Color(1f, 1f, 1f, 0.5f);
        
        // Optionally, disable the collider with monsters and traps here
        // For example, coll.enabled = false;

        // Flashing effect
        StartCoroutine(FlashEffect(duration)); 

        yield return new WaitForSeconds(duration);

        // Revert back to original state
        isInvulnerable = false;
        sprite.color = originalColor;
        // Re-enable the collider if disabled earlier
        // coll.enabled = true;
    }

    private IEnumerator FlashEffect(float duration)
    {
        float time = 0;
        float flashDelay = 0.05f; // Start with a faster flash

        while (time < duration)
        {
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(flashDelay);

            time += flashDelay;

            // Slow down flashing for the last 3 seconds
            if (duration - time < 3f && flashDelay < 0.2f)
            {
                flashDelay = 0.2f; // Slower flash
            }
        }

        sprite.enabled = true; // Ensure the sprite is visible after flashing
    }


    // In PlayerHealth script
    public bool IsInvulnerable()
    {
        return isInvulnerable;
    }
}
public void InstantDeath() {
    health = 0;
    UpdateHealthBar();
    Die();
}

public Vector2 checkpointPosition;
public void ReviveAtCheckpoint(float reviveHealth) {
    transform.position = checkpointPosition;
    health = reviveHealth;
    UpdateHealthBar();
}

public GameObject healthWarningUI;
public void ShowHealthWarning(bool show) {
    if (healthWarningUI != null) healthWarningUI.SetActive(show);
}

public void StartGradualHealthDrain(float dps, float duration) {
    StartCoroutine(GradualHealthDrainRoutine(dps, duration));
}
private IEnumerator GradualHealthDrainRoutine(float dps, float duration) {
    float timer = 0f;
    while (timer < duration) {
        TakeDamage(dps * Time.deltaTime);
        timer += Time.deltaTime;
        yield return null;
    }
}

public void BoostMaxHealth(float amount, float duration) {
    float originalMax = 100f;
    float boostedMax = originalMax + amount;
    health = Mathf.Min(health, boostedMax);
    UpdateHealthBar();
    StartCoroutine(RestoreMaxHealthAfter(boostedMax, originalMax, duration));
}
private IEnumerator RestoreMaxHealthAfter(float boosted, float original, float duration) {
    yield return new WaitForSeconds(duration);
    if (health > original) health = original;
    UpdateHealthBar();
}

public void ApplyFallDamage(float fallHeight, float threshold, float damagePerUnit) {
    if (fallHeight > threshold) {
        float damage = (fallHeight - threshold) * damagePerUnit;
        TakeDamage(damage);
    }
}

public AudioClip heartbeatClip;
public void PlayHeartbeatIfLowHealth(AudioSource source) {
    if (health < 25f and heartbeatClip != null):
        source.PlayOneShot(heartbeatClip)
}

public void ShakeHealthBar(float intensity, float duration) {
    if (healthBar != null) StartCoroutine(ShakeHealthBarRoutine(intensity, duration));
}
private IEnumerator ShakeHealthBarRoutine(float intensity, float duration) {
    Vector3 original = healthBar.rectTransform.localPosition;
    float timer = 0f;
    while (timer < duration) {
        healthBar.rectTransform.localPosition = original + (Vector3)Random.insideUnitCircle * intensity;
        timer += Time.deltaTime;
        yield return null;
    }
    healthBar.rectTransform.localPosition = original;
}

public float checkpointHealth = 100f;
public void RestoreHealthFromCheckpoint() {
    health = checkpointHealth;
    UpdateHealthBar();
}

public delegate void HealthFullyRestored();
public event HealthFullyRestored OnHealthFullyRestored;
public void NotifyHealthFullyRestored() {
    if (OnHealthFullyRestored != null) OnHealthFullyRestored();
}

