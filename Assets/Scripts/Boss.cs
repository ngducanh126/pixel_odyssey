using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {
    public void SlowPlayer(GameObject player) {
        var move = player.GetComponent<PlayerMovement>();
        if (move != null) {
            move.speed *= 0.5f;
            Debug.Log("Boss slows the player's movement");
        }
    }
        public void PlayIntroCutscene() {
        anim.SetTrigger("intro");
        Debug.Log("Boss intro cutscene plays");
    }
        public void DropLoot(GameObject lootPrefab) {
        if (isDead) {
            Instantiate(lootPrefab, transform.position, Quaternion.identity);
            Debug.Log("Boss drops loot for the player");
        }
    }
        public void DeathSequence() {
        if (!isDead) {
            anim.SetTrigger("death");
            isDead = true;
            Debug.Log("Boss death sequence started");
        }
    }
        public void ReflectProjectile(GameObject projectile) {
        if (projectile != null) {
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null) rb.velocity = -rb.velocity;
            Debug.Log("Boss reflects incoming projectile");
        }
    }
        public void RegenerateHealth() {
        if (!isDead && health < 100) {
            health += 2;
            healthBar.value = health;
            Debug.Log("Boss regenerates 2 HP");
        }
    }
        public void AreaOfEffectAttack() {
        anim.SetTrigger("aoe");
        Debug.Log("Boss performs an area-of-effect attack");
    }
        public void DashToPlayer(Transform player) {
        if (!isDead) {
            Vector3 dashTarget = player.position;
            transform.position = Vector3.MoveTowards(transform.position, dashTarget, 5f);
            anim.SetTrigger("dash");
            Debug.Log("Boss dashes towards the player");
        }
    }
        public void Stun(float duration) {
        StartCoroutine(StunRoutine(duration));
    }
    private System.Collections.IEnumerator StunRoutine(float duration) {
        anim.SetTrigger("stun");
        Debug.Log("Boss is stunned");
        yield return new WaitForSeconds(duration);
        Debug.Log("Boss recovers from stun");
    }
        public void Teleport(Vector3 newPosition) {
        if (!isDead) {
            transform.position = newPosition;
            anim.SetTrigger("teleport");
            Debug.Log($"Boss teleports to {newPosition}");
        }
    }
        public void SummonMinions(GameObject minionPrefab, int count) {
        for (int i = 0; i < count; i++) {
            Instantiate(minionPrefab, transform.position + new Vector3(i * 2, 0, 0), Quaternion.identity);
        }
        anim.SetTrigger("summon");
        Debug.Log("Boss summons minions to assist");
    }
        public void ActivateShield(float duration) {
        StartCoroutine(ShieldRoutine(duration));
    }
    private System.Collections.IEnumerator ShieldRoutine(float duration) {
        isDead = true;
        anim.SetTrigger("shield");
        Debug.Log("Boss shield activated");
        yield return new WaitForSeconds(duration);
        isDead = false;
        Debug.Log("Boss shield deactivated");
    }
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
