


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Image healthBar;
    public float health = 100f; // Player's health
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    private Vector2 startPosition;

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
        if (isInvulnerable) return;
        Vector2 previousPosition = transform.position;

        health -= damage;
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
        for (float i = 0; i < 1.5; i++)
        {
            sprite.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(1);
            sprite.color = Color.white;
            yield return new WaitForSeconds(1);
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        isInvulnerable = false;
    }

    void UpdateHealthBar()
    {
        float maxBarWidth = 800f;
        float currentWidth = maxBarWidth * (health / 100f);

        RectTransform rect = healthBar.rectTransform;
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentWidth);
        float difference = maxBarWidth - currentWidth;
        rect.anchoredPosition = new Vector2(-difference / 2, rect.anchoredPosition.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Monster") && isInvulnerable == false)
        {
            hitMonsterAudioSource.Play(); // Play the death sound effect
            TakeDamage(25f);
        }
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
}