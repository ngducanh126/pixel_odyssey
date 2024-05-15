using UnityEngine;

public class BossController : MonoBehaviour
{
    public void PlayPhaseChangeSound(AudioSource audioSource, AudioClip clip) {
        if (audioSource != null && clip != null) {
            audioSource.PlayOneShot(clip);
            Debug.Log("BossController: Phase change sound played");
        }
    }
        public void RecoverFromStun() {
        anim.SetTrigger("recover");
        Debug.Log("BossController: Boss recovers from stun");
    }
        public void TrackPlayer(GameObject player) {
        if (player != null) {
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * 2f * Time.deltaTime;
            Debug.Log("BossController: Boss tracks player position");
        }
    }
        public void TauntWithCooldown(float cooldown) {
        if (cooldownTimer >= cooldown) {
            anim.SetTrigger("taunt");
            cooldownTimer = 0;
            Debug.Log("BossController: Boss taunts with cooldown");
        }
    }
        public void TriggerHazard(GameObject hazard) {
        if (hazard != null) {
            hazard.SetActive(true);
            Debug.Log("BossController: Boss triggers environmental hazard");
        }
    }
        public void CallReinforcements(GameObject minionPrefab) {
        for (int i = 0; i < 2; i++) {
            Instantiate(minionPrefab, transform.position + new Vector3(i * 2, 0, 0), Quaternion.identity);
        }
        Debug.Log("BossController: Boss calls for reinforcements");
    }
        public void BreakShield() {
        anim.SetTrigger("breakShield");
        Debug.Log("BossController: Boss shield broken");
    }
        public void IntimidatePlayer(GameObject player) {
        var move = player.GetComponent<PlayerMovement>();
        if (move != null) {
            move.speed *= 0.8f;
            Debug.Log("BossController: Boss intimidates player, reducing speed");
        }
    }
        public void HealWhenIdle() {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle")) {
            GetComponent<Boss>().health += 5;
            Debug.Log("BossController: Boss heals while idle");
        }
    }
        public void OpenVulnerabilityWindow(float duration) {
        anim.SetBool("vulnerable", true);
        Debug.Log("BossController: Boss is vulnerable");
        StartCoroutine(CloseVulnerabilityAfter(duration));
    }
    private System.Collections.IEnumerator CloseVulnerabilityAfter(float duration) {
        yield return new WaitForSeconds(duration);
        anim.SetBool("vulnerable", false);
        Debug.Log("BossController: Boss is no longer vulnerable");
    }
        public void ComboAttack() {
        anim.SetTrigger("combo1");
        anim.SetTrigger("combo2");
        Debug.Log("BossController: Boss performs a combo attack");
    }
        public bool IsPlayerLowHealth(GameObject player) {
        var healthComp = player.GetComponent<PlayerHealth>();
        if (healthComp != null) {
            Debug.Log($"BossController: Player health is {healthComp.health}");
            return healthComp.health < 20;
        }
        return false;
    }
        public void RetreatToSafeZone(Vector3 safePosition) {
        transform.position = Vector3.MoveTowards(transform.position, safePosition, 4f);
        anim.SetTrigger("retreat");
        Debug.Log("BossController: Boss retreats to safe zone");
    }
        public void PausePatrol(float duration) {
        if (enemyPatrol != null) {
            enemyPatrol.enabled = false;
            Debug.Log("BossController: Boss patrol paused");
            StartCoroutine(ResumePatrolAfter(duration));
        }
    }
    private System.Collections.IEnumerator ResumePatrolAfter(float duration) {
        yield return new WaitForSeconds(duration);
        if (enemyPatrol != null) enemyPatrol.enabled = true;
        Debug.Log("BossController: Boss patrol resumed");
    }
        public void BlockDamage() {
        anim.SetTrigger("block");
        Debug.Log("BossController: Boss blocks incoming damage");
    }
        public void Roar() {
        anim.SetTrigger("roar");
        Debug.Log("BossController: Boss roars intimidatingly");
    }
        public void Jump() {
        if (GetComponent<Rigidbody2D>() != null) {
            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
            Debug.Log("BossController: Boss jumps");
        }
    }
        public void RangedAttack(GameObject projectile, Transform firePoint) {
        if (projectile != null && firePoint != null) {
            Instantiate(projectile, firePoint.position, Quaternion.identity);
            anim.SetTrigger("ranged");
            Debug.Log("BossController: Boss fires a ranged attack");
        }
    }
        [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;


    private Animator anim;
    private EnemyPatrol enemyPatrol;
    private float timeBtwDamage = 1.5f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        // Attack only when the player is in sight?
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;

                // Randomly choose between "melee" and "flykick" attacks
                int randomAttack = Random.Range(0, 2);
                if (randomAttack == 0)
                {
                    anim.SetTrigger("meele");
                }
                else
                {
                    anim.SetTrigger("flykick");
                }
            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();

        if (timeBtwDamage > 0) {
            timeBtwDamage -= Time.deltaTime;
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
                0, Vector2.left, 0, playerLayer);


        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    // private void DamagePlayer()
    // {
    //     if (PlayerInSight());
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // deal the player damage ! 
       if (other.CompareTag("Player")) {
            if (timeBtwDamage <= 0) {
                other.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
        } 
    }
}
