// using UnityEngine;

// public class TerrainSpawner : MonoBehaviour
// {
//     public GameObject terrainPrefab; // Assign your terrain prefab here in the inspector
//     public GameObject coinPrefab;    // Assign your coin prefab here in the inspector
//     public float spawnRate = 2.0f;   // How often terrains are spawned
//     public float moveSpeed = 5f;     // Speed at which terrains move left
//     public int coinsToSpawn = 3;     // Number of coins to spawn on each terrain

//     private void Start()
//     {
//         // Start spawning terrains at a regular interval
//         InvokeRepeating(nameof(SpawnTerrain), 0, spawnRate);
//     }

//     private void Update()
//     {
//         // Move all terrains that are children of this GameObject
//         foreach (Transform child in transform)
//         {
//             child.position += Vector3.left * moveSpeed * Time.deltaTime; // Move left by moveSpeed units per second

//             // Destroy the terrain when it's far enough off-screen
//             if (child.position.x < -10) // Adjust this value to your game's needs
//             {
//                 Destroy(child.gameObject);
//             }
//         }
//     }

//     void SpawnTerrain()
//     {
//         // Set the spawn position with a fixed x and random y
//         Vector3 spawnPosition = new Vector3(4.2f, Random.Range(-0.3f, 0.65f), 0);

//         // Instantiate the terrain
//         GameObject newTerrain = Instantiate(terrainPrefab, spawnPosition, Quaternion.identity, transform);

//         // Set a random length (scale on the x-axis) for the terrain
//         float randomLength = Random.Range(4f, 12f);
//         newTerrain.transform.localScale = new Vector3(randomLength, newTerrain.transform.localScale.y, newTerrain.transform.localScale.z);

//         // Spawn coins on the terrain
//         SpawnCoinsOnTerrain(newTerrain.transform, randomLength);
//     }

//     void SpawnCoinsOnTerrain(Transform terrainTransform, float terrainLength)
//     {
//         // Determine the y position for coins, positioned above the terrain
//         float coinY = terrainTransform.position.y + 0.5f; // Adjust as needed

//         // Distribute the coins evenly across the terrain
//         for (int i = 0; i < coinsToSpawn; i++)
//         {
//             float coinX = terrainTransform.position.x - terrainLength / 2 + terrainLength / (coinsToSpawn + 1) * (i + 1);
//             Vector3 coinPosition = new Vector3(coinX, coinY, 0);
//             GameObject coin = Instantiate(coinPrefab, coinPosition, Quaternion.identity, terrainTransform);

//             // Set the scale of the coin to 0.2 for x and y
//             coin.transform.localScale = new Vector3(0.2f, 0.2f, 1f);
//         }
//     }

// }
