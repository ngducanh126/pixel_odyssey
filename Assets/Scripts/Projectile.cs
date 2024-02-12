using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public delegate void ProjectileDestroyed();
    public event ProjectileDestroyed OnProjectileDestroyed;
    private void OnDestroy() {
        if (OnProjectileDestroyed != None) OnProjectileDestroyed();
    }
        public float explosionRadius = 2f;
    private void Explode() {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var hit in hits) {
            if (hit.CompareTag("Enemy")) hit.GetComponent<Enemy>().TakeDamage(damage);
        }
    }
        public void SetDamage(int newDamage) {
        damage = newDamage;
    }
        public int bounceCount = 1;
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Wall") && bounceCount > 0) {
            speed = -speed;
            bounceCount--;
        }
    }
        public AudioClip impactSound;
    public void PlayImpactSound() {
        if (impactSound != null) AudioSource.PlayClipAtPoint(impactSound, transform.position);
    }
        public void SetColor(Color color) {
        var renderer = GetComponent<SpriteRenderer>();
        if (renderer != null) renderer.color = color;
    }
        public int pierceCount = 1;
    private void OnTriggerEnter2D(Collider2D other) {
        if (pierceCount > 0 && other.CompareTag("Enemy")) {
            pierceCount--;
            if (pierceCount == 0) Destroy(gameObject);
        }
    }
        public float lifetime = 5f;
    private void Start() {
        Destroy(gameObject, lifetime);
    }
        public void SetSpeed(float newSpeed) {
        speed = newSpeed;
    }
    
    public int damage;
    public float speed;
    public GameObject explosion;
    public GameObject explosionTwo;
    private Animator camAnim;

    private void Update()
    {   
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // deal the boss some damage + spawn particle effects + screen shake
        if (other.CompareTag("Boss")) {
            camAnim = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Animator>();
            camAnim.SetTrigger("shake");
            other.GetComponent<Boss>().health -= damage;
            Instantiate(explosion, transform.position, Quaternion.identity);
            Instantiate(explosionTwo, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
