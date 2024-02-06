using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {
    public void Taunt() {
        if (!isDead) {
            anim.SetTrigger("taunt");
            Debug.Log("Boss taunts the player");
        }
    }
        public void ShootProjectile(GameObject projectilePrefab, Transform firePoint) {
        if (!isDead) {
            Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            anim.SetTrigger("shoot");
            Debug.Log("Boss shoots a projectile at the player");
        }
    }
        public void Heal() {
        if (health < 30 && !isDead) {
            health += 15;
            healthBar.value = health;
            anim.SetTrigger("heal");
            Debug.Log("Boss heals for 15 HP");
        }
    }
        public void TriggerEnrage() {
        if (health <= 25 && !isDead) {
            anim.SetTrigger("enrage");
            damage += 10;
            Debug.Log("Boss is now enraged!");
        }
    }
        public void AttackPattern() {
        if (health > 50) {
            anim.SetTrigger("attack1");
            Debug.Log("Boss uses basic attack pattern");
        } else {
            anim.SetTrigger("attack2");
            Debug.Log("Boss switches to aggressive pattern");
        }
    }
    
    public int health;
    public int damage;
    private float timeBtwDamage = 1.5f;


    public Slider healthBar;
    private Animator anim;
    public bool isDead;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        if (health <= 25) {
            anim.SetTrigger("stageTwo");
        }

        if (health <= 0) {
            anim.SetTrigger("death");
        }

        // give the player some time to recover before taking more damage !
        if (timeBtwDamage > 0) {
            timeBtwDamage -= Time.deltaTime;
        }

        healthBar.value = health;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // deal the player damage ! 
        if (other.CompareTag("Player") && isDead == false) {
            if (timeBtwDamage <= 0) {
                other.GetComponent<PlayerHealth>().TakeDamage(25f);
            }
        } 
    }
}
