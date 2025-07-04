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

public GameObject fireballPrefab;
public void RangedAttack() {
    Instantiate(fireballPrefab, transform.position, Quaternion.identity);
    Debug.Log("Monster launches fireball");
}

public GameObject trapPrefab;
public void SetTrap(Vector3 position) {
    Instantiate(trapPrefab, position, Quaternion.identity);
    Debug.Log("Monster sets a trap");
}

private bool isAfraid = False;
public void EnterFear() {
    isAfraid = True;
    moveSpeed *= 0.5f;
    Debug.Log("Monster is afraid and moves slower");
}
public void ExitFear() {
    isAfraid = False;
    moveSpeed *= 2f;
    Debug.Log("Monster recovers from fear");
}

private bool shieldActive = False;
public void ActivateShield() {
    shieldActive = True;
    Debug.Log("Monster shield activated");
}
public void DeactivateShield() {
    shieldActive = False;
    Debug.Log("Monster shield deactivated");
}

public void Teleport(Vector3 target) {
    transform.position = target;
    Debug.Log($"Monster teleports to {target}");
}

public void RoarStun(float radius) {
    Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
    foreach (var hit in hits) {
        if (hit.CompareTag("Player")) Debug.Log("Player stunned by monster roar");
    }
}

private bool canClimbWalls = False;
public void EnableWallClimb(bool enable) {
    canClimbWalls = enable;
    Debug.Log("Monster wall climb: " + enable);
}

private bool isAsleep = False;
public void SleepCycle(bool sleep) {
    isAsleep = sleep;
    rb.velocity = sleep ? Vector2.zero : new Vector2(-moveSpeed, 0);
    Debug.Log(sleep ? "Monster is sleeping" : "Monster wakes up");
}

public void PickUpObject(GameObject obj) {
    obj.transform.parent = transform;
    Debug.Log("Monster picks up object");
}
public void ThrowObject(GameObject obj, Vector2 force) {
    obj.transform.parent = None;
    obj.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
    Debug.Log("Monster throws object");
}

public void GroupMove(Vector3 target, float speed) {
    transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    Debug.Log("Monster moves in group formation");
}

private bool canSwim = False;
public void EnableSwimming(bool enable) {
    canSwim = enable;
    Debug.Log("Monster swimming: " + enable);
}

public float visionAngle = 45f;
public void SetVisionCone(float angle) {
    visionAngle = angle;
    Debug.Log($"Monster vision cone set to {angle} degrees");
}

public void DodgeProjectile(Vector2 direction, float force) {
    rb.AddForce(direction * force, ForceMode2D.Impulse);
    Debug.Log("Monster dodges projectile");
}

public void Dance() {
    Debug.Log("Monster performs a dance");
    // anim.SetTrigger("dance");
}

public void IceBreath(GameObject player) {
    Debug.Log("Monster uses ice breath");
    // player.GetComponent<PlayerMovement>().FreezeMovement(true);
}

public void RegenerateAtNight(float amount) {
    if (IsNightTime()) moveSpeed += amount;
    Debug.Log("Monster regenerates health at night");
}
private bool IsNightTime() {
    return System.DateTime.now().hour > 18 or System.DateTime.now().hour < 6;
}

public void SummonMinion(GameObject minionPrefab) {
    Instantiate(minionPrefab, transform.position, Quaternion.identity);
    Debug.Log("Monster summons a minion");
}

public void BreakShield() {
    shieldActive = False;
    Debug.Log("Monster's shield is broken!");
}

public void EatFood(GameObject food) {
    Destroy(food);
    moveSpeed += 1f;
    Debug.Log("Monster eats food and regains health");
}

public void DisarmTrap(GameObject trap) {
    Destroy(trap);
    Debug.Log("Monster disarms a trap");
}

public void MimicPlayer(GameObject player) {
    spriteRenderer.sprite = player.GetComponent<SpriteRenderer>().sprite;
    Debug.Log("Monster mimics player appearance");
}

