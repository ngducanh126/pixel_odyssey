using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 2f;
    [SerializeField] private float fireSpeed = 2f;

    [SerializeField] private LayerMask jumpableGround;

    public GameObject firePrefab; 
    public Vector2 fireOffset = new Vector2(1.0f, 0);

    private float fireCooldown = 2f; // Cooldown duration in seconds
    private float lastFireTime = -2f; // Initialize to allow firing immediately at start

    private enum MovementState { idle, running, jumping, falling }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.F) && Time.time - lastFireTime >= fireCooldown)
        {
            SpawnFire();
            lastFireTime = Time.time; // Update the last fire time to the current time
        }

       UpdateAnimationState();
    }

    void SpawnFire()
    {
        // Spawn the fire object at the player's position + offset
        GameObject fireInstance = Instantiate(firePrefab, (Vector2)transform.position + fireOffset, Quaternion.identity);
        fireInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(fireSpeed, 0); // Set the velocity of the spawned fire
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
}
