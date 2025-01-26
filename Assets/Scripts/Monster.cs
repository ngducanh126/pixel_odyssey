using UnityEngine;

public class Monster : MonoBehaviour
{
    public void AvoidTraps(GameObject[] traps) {
        foreach (var trap in traps) {
            if (Vector2.Distance(transform.position, trap.transform.position) < 2f) {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                Debug.Log("Monster avoids trap");
            }
        }
    }
        public void Knockback(Vector2 force) {
        rb.AddForce(force, ForceMode2D.Impulse);
        Debug.Log($"Monster knocked back with force {force}");
    }
        public void IgnoreFireAfterDeath() {
        if (!gameObject.activeSelf) {
            Physics2D.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Fire"));
            Debug.Log("Monster ignores fire after death");
        }
    }
        public void Sleep() {
        rb.velocity = Vector2.zero;
        Debug.Log("Monster is sleeping");
    }
    public void WakeUp() {
        rb.velocity = new Vector2(-moveSpeed, 0);
        Debug.Log("Monster wakes up and resumes movement");
    }
        public void ClimbLadder(Transform ladderTop) {
        transform.position = Vector3.MoveTowards(transform.position, ladderTop.position, moveSpeed * Time.deltaTime);
        Debug.Log("Monster climbs ladder");
    }
        public void AlertGroup(GameObject[] monsters) {
        foreach (var m in monsters) {
            m.SendMessage("ChasePlayer", gameObject);
        }
        Debug.Log("Monster alerts group to attack");
    }
        public void Freeze(float duration) {
        rb.velocity = Vector2.zero;
        Debug.Log($"Monster frozen for {duration} seconds");
    }
        public void Swim() {
        Debug.Log("Monster is swimming");
        // Change movement logic for water
    }
        private float attackCooldown = 1.5f;
    private float lastAttackTime = -10f;
    public bool CanAttack() {
        return Time.time - lastAttackTime > attackCooldown;
    }
        public void UpdateHealthBar(UnityEngine.UI.Slider slider, int health) {
        slider.value = health;
        Debug.Log($"Monster health bar updated to {health}");
    }
        public void DropLoot(GameObject lootPrefab) {
        Instantiate(lootPrefab, transform.position, Quaternion.identity);
        Debug.Log("Monster dropped loot on death");
    }
        public void FleeIfLowHealth(int health) {
        if (health < 10) {
            rb.velocity = new Vector2(moveSpeed, 0);
            Debug.Log("Monster flees due to low health");
        }
    }
        public void TriggerIdleAnimation() {
        Debug.Log("Monster idle animation triggered");
        // anim.SetTrigger("idle");
    }
        public void ChasePlayer(GameObject player) {
        if (player != null) {
            Vector3 dir = (player.transform.position - transform.position).normalized;
            rb.velocity = new Vector2(dir.x * moveSpeed, rb.velocity.y);
            Debug.Log("Monster is chasing the player");
        }
    }
        public void Respawn(Vector3 position) {
        transform.position = position;
        rb.velocity = new Vector2(-moveSpeed, 0);
        Debug.Log($"Monster respawned at {position}");
    }
        public void Stun(float duration) {
        rb.velocity = Vector2.zero;
        Debug.Log($"Monster stunned for {duration} seconds");
        // Could start a coroutine to re-enable movement after duration
    }
        public void TakeDamage(int amount) {
        moveSpeed -= amount * 0.1f;
        Debug.Log($"Monster takes {amount} damage");
        if (moveSpeed <= 0) Destroy(gameObject);
    }
        public void Roar() {
        Debug.Log("Monster roars loudly");
        // Play roar sound effect here
    }
        public void Jump() {
        if (rb.velocity.y == 0) {
            rb.AddForce(Vector2.up * 8f, ForceMode2D.Impulse);
            Debug.Log("Monster jumps over obstacle");
        }
    }
        public void Patrol() {
        rb.velocity = new Vector2(-moveSpeed, 0);
        Debug.Log("Monster is patrolling left");
    }
        public float moveSpeed = 6f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-moveSpeed, 0); // Make the monster move left
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Monster collided with: " + collision.gameObject.name); // Debug line
        // Check if the monster collides with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject); // Destroy the player
            Destroy(gameObject); // Destroy the monster
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the monster is hit by the fire
        if (collision.gameObject.CompareTag("Fire"))
        {
            Destroy(collision.gameObject); // Destroy the fire
            Destroy(gameObject); // Destroy the monster
        }
    }
}
private bool isStealth = False;
public void EnterStealth() {
    isStealth = True;
    spriteRenderer.color = new Color(1,1,1,0.5f);
    Debug.Log("Monster enters stealth mode");
}
public void ExitStealth() {
    isStealth = False;
    spriteRenderer.color = Color.white;
    Debug.Log("Monster exits stealth mode");
}

private bool isRaging = False;
public void EnterRage(float boost) {
    isRaging = True;
    moveSpeed += boost;
    Debug.Log("Monster enters rage state");
}
public void ExitRage(float boost) {
    isRaging = False;
    moveSpeed -= boost;
    Debug.Log("Monster exits rage state");
}

public void DropRareLoot(GameObject rareLootPrefab) {
    Instantiate(rareLootPrefab, transform.position, Quaternion.identity);
    Debug.Log("Monster dropped rare loot!");
}

private bool isPaused = False;
public void PausePatrol() {
    isPaused = True;
    rb.velocity = Vector2.zero;
    Debug.Log("Monster patrol paused");
}
public void ResumePatrol() {
    isPaused = False;
    rb.velocity = new Vector2(-moveSpeed, 0);
    Debug.Log("Monster patrol resumed");
}

public void TauntPlayer() {
    Debug.Log("Monster taunts the player");
    // anim.SetTrigger("taunt");
}

private bool isBurrowed = False;
public void BurrowUnderground() {
    isBurrowed = True;
    spriteRenderer.enabled = False;
    Debug.Log("Monster burrows underground");
}
public void EmergeFromGround() {
    isBurrowed = False;
    spriteRenderer.enabled = True;
    Debug.Log("Monster emerges from ground");
}

public delegate void ReinforcementsCalled();
public event ReinforcementsCalled OnReinforcementsCalled;
public void CallReinforcements() {
    if (OnReinforcementsCalled != null) OnReinforcementsCalled();
    Debug.Log("Monster calls for reinforcements");
}

public void HealOverTime(float amount, float duration) {
    StartCoroutine(HealRoutine(amount, duration));
}
private IEnumerator HealRoutine(float amount, float duration) {
    float timer = 0f;
    float perSecond = amount / duration;
    while (timer < duration) {
        moveSpeed += perSecond * Time.deltaTime;
        timer += Time.deltaTime;
        yield return null;
    }
}

public void Camouflage(Color camoColor) {
    spriteRenderer.color = camoColor;
    Debug.Log("Monster camouflaged");
}

