using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public void SetAttackPower(float power) {
        animator.SetFloat("attackPower", power);
    }
        private bool attackLocked = false;
    public void LockAttackAfterDamage(float duration) {
        attackLocked = true;
        Invoke("UnlockAttack", duration);
    }
    private void UnlockAttack() {
        attackLocked = false;
    }
        public void BlendAttackAnimations(string animA, string animB, float blend) {
        animator.CrossFade(animA, blend);
        animator.CrossFade(animB, blend);
    }
        public void AttackWhileAirborne() {
        if (!IsGrounded()) animator.SetTrigger("airAttack");
    }
        public GameObject trailPrefab;
    public void ShowWeaponTrail() {
        if (trailPrefab != null) Instantiate(trailPrefab, firePoint.position, Quaternion.identity);
    }
        private bool isDodging = false;
    public void CancelAttackOnDodge() {
        if (isDodging) animator.ResetTrigger("attack");
    }
        public void HeavyAttackStun(GameObject enemy, float stunDuration) {
        EnemyAI ai = enemy.GetComponent<EnemyAI>();
        if (ai != null) ai.Stun(stunDuration);
    }
        public float critChance = 0.1f;
    public void TryCriticalHit() {
        if (Random.value < critChance) Debug.Log("Critical hit!");
    }
        public AudioClip attackSound;
    public void PlayAttackSound() {
        if (attackSound != null) AudioSource.PlayClipAtPoint(attackSound, transform.position);
    }
        public void AttackTowardsMouse() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - transform.position).normalized;
        // Use direction for attack
    }
        public void KnockbackEnemy(GameObject enemy, float force) {
        Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
        if (rb != null) rb.AddForce(Vector2.right * force, ForceMode2D.Impulse);
    }
        public void ChargedAttack(float chargeTime) {
        float power = Mathf.Clamp(chargeTime, 0.5f, 2f);
        animator.SetFloat("charge", power);
        // Apply extra damage based on charge
    }
        private int comboStep = 0;
    public void ComboAttack() {
        comboStep = (comboStep + 1) % 3;
        animator.SetInteger("comboStep", comboStep);
    }
        private float arrowCooldown = 1f;
    private float lastArrowTime = -1f;
    public void ShootArrow(GameObject arrowPrefab) {
        if (Time.time - lastArrowTime >= arrowCooldown) {
            Instantiate(arrowPrefab, firePoint.position, Quaternion.identity);
            lastArrowTime = Time.time;
        }
    }
        public void MeleeAttack() {
        animator.SetTrigger("melee");
        // Detect enemies in range and apply damage
    }
        public GameObject fireballPrefab; 
    public float fireballSpeed = 10f; 
    public float maxDistance = 10f; // Set your desired maximum distance
    [SerializeField] private Transform firePoint;
    

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("attack");

            float directionMultiplier = spriteRenderer.flipX ? -1f : 1f;
            firePoint.localPosition = new Vector3(Mathf.Abs(firePoint.localPosition.x) * directionMultiplier, firePoint.localPosition.y, firePoint.localPosition.z);

            GameObject fireballInstance = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);

            if (spriteRenderer.flipX)
            {
                fireballInstance.transform.localScale = new Vector3(-1 * Mathf.Abs(fireballInstance.transform.localScale.x),
                                                                    fireballInstance.transform.localScale.y,
                                                                    fireballInstance.transform.localScale.z);
            }

            Vector2 fireDirection = spriteRenderer.flipX ? Vector2.left : Vector2.right;

            Rigidbody2D rb = fireballInstance.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = fireDirection * fireballSpeed;

                // Destroy the fireball after it travels a certain distance
                Destroy(fireballInstance, maxDistance / fireballSpeed);
            }
        }
    }
}
