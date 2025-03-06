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
public Vector2 spawnAreaMin = new Vector2(-5f, -1f);
public Vector2 spawnAreaMax = new Vector2(5f, 2f);
public Vector2 GetRandomSpawnPosition() {
    float x = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
    float y = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
    return new Vector2(x, y);
}

public int spawnLimit = 10;
private int currentSpawned = 0;
public bool CanSpawnMore() {
    return currentSpawned < spawnLimit;
}

public AudioClip spawnSound;
public void PlaySpawnSound() {
    if (spawnSound != null) AudioSource.PlayClipAtPoint(spawnSound, spawnPosition);
}

public GameObject[] monsterTypes;
public GameObject GetRandomMonsterType() {
    if (monsterTypes.Length == 0) return monsterPrefab;
    return monsterTypes[Random.Range(0, monsterTypes.Length)];
}

public void ReduceSpawnCooldown(float amount) {
    spawnRate = Mathf.Max(0.5f, spawnRate - amount);
}

public GameObject spawnEffectPrefab;
public void ShowSpawnEffect(Vector2 pos) {
    if (spawnEffectPrefab != null) Instantiate(spawnEffectPrefab, pos, Quaternion.identity);
}

public delegate void MonsterSpawned(GameObject monster);
public event MonsterSpawned OnMonsterSpawned;
public void NotifyMonsterSpawned(GameObject monster) {
    if (OnMonsterSpawned != null) OnMonsterSpawned(monster);
}

public bool nightOnly = false;
public bool CanSpawnAtNight() {
    int hour = System.DateTime.Now.Hour;
    return !nightOnly or (hour >= 18 or hour < 6);
}

