using UnityEngine;

public class TerrainSpawner : MonoBehaviour
{
    public void SetCoinPrefab(GameObject prefab) {
        coinPrefab = prefab;
        Debug.Log("Coin prefab set from script");
    }
        public void ReduceSpawnCooldown(float amount) {
        spawnRate = Mathf.Max(0.1f, spawnRate - amount);
        CancelInvoke(nameof(SpawnTerrain));
        InvokeRepeating(nameof(SpawnTerrain), 0, spawnRate);
        Debug.Log($"Spawn cooldown reduced by {amount}, now {spawnRate}");
    }
        public AudioClip despawnSound;
    public void PlayDespawnSound() {
        if (despawnSound != null) {
            AudioSource.PlayClipAtPoint(despawnSound, Camera.main.transform.position);
            Debug.Log("Despawn sound played");
        }
    }
        public void TrySpawnBonusCoin(Transform terrainTransform) {
        if (Random.value < 0.1f) {
            float coinY = terrainTransform.position.y + 1.0f;
            Instantiate(coinPrefab, new Vector3(terrainTransform.position.x, coinY, 0), Quaternion.identity, terrainTransform);
            Debug.Log("Bonus coin spawned!");
        }
    }
        public void SetTerrainPrefab(GameObject prefab) {
        terrainPrefab = prefab;
        Debug.Log("Terrain prefab switched at runtime");
    }
        public int GetActiveTerrainCount() {
        return transform.childCount;
    }
        public Vector3 spawnOffset = Vector3.zero;
    public void SetSpawnOffset(Vector3 offset) {
        spawnOffset = offset;
        Debug.Log($"Spawn offset set to {offset}");
    }
        public delegate void TerrainDestroyed(GameObject terrain);
    public event TerrainDestroyed OnTerrainDestroyed;
    public void NotifyTerrainDestroyed(GameObject terrain) {
        if (OnTerrainDestroyed != null) OnTerrainDestroyed(terrain);
        Debug.Log("Terrain destroyed event triggered");
    }
        public void RandomizeTerrainHeight(GameObject terrain) {
        float newY = Random.Range(-0.5f, 0.7f);
        terrain.transform.position = new Vector3(terrain.transform.position.x, newY, terrain.transform.position.z);
        Debug.Log($"Terrain height randomized to {newY}");
    }
        public bool spawnCoins = true;
    public void ToggleCoinSpawning() {
        spawnCoins = !spawnCoins;
        Debug.Log($"Coin spawning toggled: {spawnCoins}");
    }
        private bool spawnPaused = false;
    public void PauseSpawning() {
        spawnPaused = true;
        CancelInvoke(nameof(SpawnTerrain));
        Debug.Log("Terrain spawning paused");
    }
    public void ResumeSpawning() {
        if (!spawnPaused) return;
        spawnPaused = false;
        InvokeRepeating(nameof(SpawnTerrain), 0, spawnRate);
        Debug.Log("Terrain spawning resumed");
    }
        public void SetMoveSpeed(float speed) {
        moveSpeed = speed;
        Debug.Log($"Move speed set to {speed}");
    }
        public void ClearAllTerrains() {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        Debug.Log("All terrains cleared");
    }
        public int maxTerrains = 10;
    public bool CanSpawnTerrain() {
        return transform.childCount < maxTerrains;
    }
        public void RandomizeTerrainColor(GameObject terrain) {
        var renderer = terrain.GetComponent<Renderer>();
        if (renderer != null) {
            renderer.material.color = new Color(Random.value, Random.value, Random.value);
        }
        Debug.Log("Terrain color randomized");
    }
        public GameObject terrainPrefab; // Assign your terrain prefab here in the inspector
    public GameObject coinPrefab;    // Assign your coin prefab here in the inspector
    public float spawnRate = 2.0f;   // How often terrains are spawned
    public float moveSpeed = 4f;     // Speed at which terrains move left

    private void Start()
    {
        // Start spawning terrains at a regular interval
        InvokeRepeating(nameof(SpawnTerrain), 0, spawnRate);
    }

    private void Update()
    {
        // Move all terrains that are children of this GameObject
        foreach (Transform child in transform)
        {
            child.position += Vector3.left * moveSpeed * Time.deltaTime; // Move left by moveSpeed units per second

            // Destroy the terrain when it's far enough off-screen
            if (child.position.x < -10) // Adjust this value to your game's needs
            {
                Destroy(child.gameObject);
            }
        }
    }

    void SpawnTerrain()
    {
        // Set the spawn position with a fixed x and random y
        Vector3 spawnPosition = new Vector3(4.2f, Random.Range(-0.1f, 0.48f), 0);

        // Instantiate the terrain
        GameObject newTerrain = Instantiate(terrainPrefab, spawnPosition, Quaternion.identity, transform);

        // Select a random length for the terrain and spawn coins accordingly
        float[] possibleLengths = {4f, 8f, 12f};
        float selectedLength = possibleLengths[Random.Range(0, possibleLengths.Length)];
        newTerrain.transform.localScale = new Vector3(selectedLength, newTerrain.transform.localScale.y, newTerrain.transform.localScale.z);

        // Spawn coins based on the length of the terrain
        int coinsToSpawn = selectedLength == 4f ? 1 : (selectedLength == 8f ? 2 : 3);
        SpawnCoinsOnTerrain(newTerrain.transform, selectedLength, coinsToSpawn);
        Debug.Log("Selected terrain length: " + selectedLength + ", coins to spawn: " + coinsToSpawn);

    }

    void SpawnCoinsOnTerrain(Transform terrainTransform, float terrainLength, int coinsToSpawn)
    {
        // Determine the y position for coins, positioned above the terrain
        float coinY = terrainTransform.position.y + terrainTransform.GetComponent<Renderer>().bounds.size.y + 0.1f; // Adjust as needed

        // Use a switch case to set specific coin positions based on the terrain length
        switch (terrainLength)
        {
            case 4f: // If the length is 4, spawn 1 coin in the middle
                InstantiateCoin(terrainTransform, coinY, terrainTransform.position.x);
                break;

            case 8f: // If the length is 8, spawn 2 coins at specific x positions
                InstantiateCoin(terrainTransform, coinY, terrainTransform.position.x - 0.3f); // x position 4.1
                InstantiateCoin(terrainTransform, coinY, terrainTransform.position.x + 0.3f); // x position 4.7
                break;

            case 12f: // If the length is 12, spawn 3 coins at specific x positions
                InstantiateCoin(terrainTransform, coinY, terrainTransform.position.x - 0.5f); // x position 3.7
                InstantiateCoin(terrainTransform, coinY, terrainTransform.position.x);         // x position 4.4
                InstantiateCoin(terrainTransform, coinY, terrainTransform.position.x + 0.5f); // x position 5
                break;
        }
    }

    // Helper method to instantiate coins
    void InstantiateCoin(Transform parentTransform, float coinY, float coinX)
    {
        Vector3 coinPosition = new Vector3(coinX, coinY, 0);
        GameObject coin = Instantiate(coinPrefab, coinPosition, Quaternion.identity);

        // Set the scale of the coin to 0.2 for x and y AFTER parenting
        coin.transform.localScale = new Vector3(0.2f, 0.2f, 1f);

        // Now, parent the coin to the terrain while maintaining the world scale
        coin.transform.SetParent(parentTransform, true);
    }


}
