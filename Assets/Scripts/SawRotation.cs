using UnityEngine;

public class SawRotation : MonoBehaviour

{
    [SerializeField] private float damage;
    public float rotationSpeed = 100f; // Speed of rotation

    void Update()
    {
        // Rotate the saw around its Z-axis at a constant speed
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

        private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().TakeDamage(25f);
        }
}
}
