using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject monsterPrefab; // Assign your monster prefab here in the inspector
    public float spawnRate = 5.0f; // How often monsters are spawned
    public Vector2 spawnPosition = new Vector2(4f, -0.48f); // Fixed spawn position

    private void Start()
    {
        // Start spawning monsters at a regular interval
        InvokeRepeating(nameof(SpawnMonster), 0, spawnRate);
    }

    void SpawnMonster()
    {
        // Instantiate the monster at the fixed position
        Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
    }
}
