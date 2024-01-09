using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public void Interact(GameObject npc) {
        if (npc != null) {
            Debug.Log($"Player interacts with: {npc.name}");
            // Example: npc.GetComponent<NPC>().Talk();
        }
    }
        public void Sprint(bool isSprinting) {
        speed = isSprinting ? 10f : 5f;
        anim.SetBool("isSprinting", isSprinting);
        Debug.Log($"Player sprinting: {isSprinting}");
    }
        public void Respawn(Vector3 position) {
        transform.position = position;
        health = 100;
        anim.SetTrigger("respawn");
        Debug.Log($"Player respawned at {position}");
    }
        public void TakeDamage(int damage) {
        health -= damage;
        anim.SetTrigger("hurt");
        Debug.Log($"Player takes {damage} damage");
        if (health <= 0) {
            anim.SetTrigger("dead");
        }
    }
        public void CollectCoin(int amount) {
        int coins = PlayerPrefs.GetInt("coins", 0);
        coins += amount;
        PlayerPrefs.SetInt("coins", coins);
        Debug.Log($"Player collected {amount} coins");
    }
        public void SetHealth(int value) {
        health = value;
        Debug.Log($"Player health set to: {value}");
        if (health <= 0) {
            anim.SetTrigger("dead");
        }
    }
        public void Jump() {
        if (rb.velocity.y == 0) {
            rb.AddForce(Vector2.up * 7f, ForceMode2D.Impulse);
            anim.SetTrigger("jump");
            Debug.Log("Player jumps");
        }
    }
        public void Move(float horizontal) {
        float moveAmount = horizontal * speed * Time.deltaTime;
        rb.MovePosition(rb.position + new Vector2(moveAmount, 0));
        Debug.Log($"Player moves with input: {horizontal}");
    }
    
    public int health;
    public float speed;

    private Animator anim;
    private Rigidbody2D rb;
    private Vector2 moveVelocity;


    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        moveVelocity = moveInput.normalized * speed;

        if (moveInput != Vector2.zero)
        {
            anim.SetBool("isRunning", true);
        }
        else {
            anim.SetBool("isRunning", false);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
    }
}
