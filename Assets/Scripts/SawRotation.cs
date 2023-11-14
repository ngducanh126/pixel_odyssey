using UnityEngine;

public class SawRotation : MonoBehaviour
{
    public float rotationSpeed = 100f; // Speed of rotation

    void Update()
    {
        // Rotate the saw around its Z-axis at a constant speed
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
