

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private bool stunned = false;
    public void Stun(float duration) {
        stunned = true;
        StartCoroutine(RemoveStun(duration));
    }
    private IEnumerator RemoveStun(float duration) {
        yield return new WaitForSeconds(duration);
        stunned = false;
    }
        public void PlayCustomAnimation(string animName) {
        if (anim != null) anim.Play(animName);
    }
        private bool onLadder = false;
    public void SetOnLadder(bool enable) {
        onLadder = enable;
    }
        public void SetGravityScale(float scale) {
        rb.gravityScale = scale;
    }
        public void AutoMove(float speed, float duration) {
        StartCoroutine(AutoMoveRoutine(speed, duration));
    }
    private IEnumerator AutoMoveRoutine(float speed, float duration) {
        float timer = 0f;
        while (timer < duration) {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }
        private bool controlsLocked = false;
    public void LockControls(bool lockState) {
        controlsLocked = lockState;
    }
        public void VariableJump(float holdTime) {
        float jumpStrength = Mathf.Lerp(jumpForce * 0.5f, jumpForce, holdTime);
        rb.velocity = new Vector2(rb.velocity.x, jumpStrength);
    }
        public void FlipSprite(bool facingRight) {
        sprite.flipX = !facingRight;
    }
        private bool invincible = false;
    public void SetInvincibility(float duration) {
        invincible = true;
        StartCoroutine(RemoveInvincibility(duration));
    }
    private IEnumerator RemoveInvincibility(float duration) {
        yield return new WaitForSeconds(duration);
        invincible = false;
    }
        public void TriggerLandingAnimation() {
        if (anim != null) anim.SetTrigger("Land");
    }
        private bool onMovingPlatform = false;
    public void SetOnMovingPlatform(bool enable) {
        onMovingPlatform = enable;
    }
        public void TeleportToCheckpoint(Vector3 checkpoint) {
        transform.position = checkpoint;
    }
        private float stamina = 100f;
    public void UseStamina(float amount) {
        stamina = Mathf.Max(0, stamina - amount);
    }
    public void RecoverStamina(float amount) {
        stamina = Mathf.Min(100f, stamina + amount);
    }
        public void Slide(float slideSpeed, float duration) {
        StartCoroutine(SlideRoutine(slideSpeed, duration));
    }
    private IEnumerator SlideRoutine(float slideSpeed, float duration) {
        float originalSpeed = moveSpeed;
        moveSpeed = slideSpeed;
        yield return new WaitForSeconds(duration);
        moveSpeed = originalSpeed;
    }
        public void TriggerSlowMotion(float duration) {
        Time.timeScale = 0.5f;
        StartCoroutine(RestoreTimeScale(duration));
    }
    private IEnumerator RestoreTimeScale(float duration) {
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }
        private bool movementFrozen = false;
    public void FreezeMovement(bool freeze) {
        movementFrozen = freeze;
        rb.velocity = freeze ? Vector2.zero : rb.velocity;
    }
        public void SetAirControl(float multiplier) {
        moveSpeed *= multiplier;
    }
        public void GroundPound(float poundForce) {
        rb.velocity = new Vector2(rb.velocity.x, -poundForce);
    }
        private bool isSwimming = false;
    public void SetSwimming(bool enable) {
        isSwimming = enable;
        rb.gravityScale = enable ? 0.2f : 1f;
    }
        private bool isSprinting = false;
    public void ToggleSprint(bool enable) {
        isSprinting = enable;
        moveSpeed = enable ? moveSpeed * 1.5f : moveSpeed / 1.5f;
    }
        public void ApplyKnockback(Vector2 force) {
        rb.AddForce(force, ForceMode2D.Impulse);
    }
        public void GrabLedge(Transform ledge) {
        rb.velocity = Vector2.zero;
        transform.position = ledge.position;
    }
    public void ClimbLedge(Vector3 climbOffset) {
        transform.position += climbOffset;
    }
        private int jumpCount = 0;
    public int maxJumps = 2;
    public void DoubleJump() {
        if (jumpCount < maxJumps) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.contacts[0].normal.y > 0.5f) jumpCount = 0;
    }
        private bool isCrouching = false;
    public void Crouch(bool enable) {
        isCrouching = enable;
        coll.size = enable ? new Vector2(coll.size.x, coll.size.y / 2) : new Vector2(coll.size.x, coll.size.y * 2);
    }
        public void WallJump(float forceX, float forceY) {
        rb.velocity = new Vector2(forceX, forceY);
    }
        public void Dash(float dashForce, float duration) {
        StartCoroutine(DashRoutine(dashForce, duration));
    }
    private IEnumerator DashRoutine(float dashForce, float duration) {
        float originalSpeed = moveSpeed;
        moveSpeed = dashForce;
        yield return new WaitForSeconds(duration);
        moveSpeed = originalSpeed;
    }
        private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;
    


    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;
    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask trapLayer;

    private enum MovementState { idle, running, jumping, falling }
    [SerializeField] private AudioSource jumpAudioSource; 

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            PlayJumpSound(); // Play jump sound
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private void PlayJumpSound()
    {
        if (jumpAudioSource != null && jumpAudioSource.clip != null)
        {
            // Debug.Log("jumping audio soruce played");
            jumpAudioSource.Play();
        }
    }

    // In PlayerMovement script
    public void ModifySpeed(float multiplier, float duration)
    {
        StartCoroutine(AdjustSpeed(multiplier, duration));
    }

    private IEnumerator AdjustSpeed(float multiplier, float duration)
    {
        float originalSpeed = moveSpeed;
        moveSpeed *= multiplier;

        yield return new WaitForSeconds(5f); // Change to 5 seconds

        moveSpeed = originalSpeed;
    }



}

