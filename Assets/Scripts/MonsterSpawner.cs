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

private Queue<GameObject> spawnQueue = new Queue<GameObject>();
public void EnqueueMonster(GameObject prefab) {
    spawnQueue.Enqueue(prefab);
}
public void SpawnFromQueue() {
    if (spawnQueue.Count > 0) Instantiate(spawnQueue.Dequeue(), GetRandomSpawnPosition(), Quaternion.identity);
}

public float proximityRadius = 5f;
public void SpawnOnPlayerProximity(Transform player) {
    if (Vector2.Distance(player.position, spawnPosition) < proximityRadius) SpawnMonster();
}

private List<GameObject> spawnedMonsters = new List<GameObject>();
public void ClearAllMonsters() {
    foreach (var m in spawnedMonsters) Destroy(m);
    spawnedMonsters.Clear();
}

public GameObject bossPrefab;
public void SpawnBoss() {
    Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
}

public void SetSpawnRate(float rate) {
    spawnRate = rate;
}

public float monsterLifetime = 10f;
public void DespawnAfterTime(GameObject monster) {
    Destroy(monster, monsterLifetime);
}

public UnityEngine.UI.Text spawnTimerText;
public void UpdateSpawnTimerUI(float timeLeft) {
    if (spawnTimerText != null) spawnTimerText.text = timeLeft.ToString("F1");
}

public bool blockDuringCutscene = false;
public void SetBlockDuringCutscene(bool block) {
    blockDuringCutscene = block;
}

private int totalSpawned = 0;
public void IncrementSpawnCounter() {
    totalSpawned++;
}
public int GetTotalSpawned() {
    return totalSpawned;
}

public void ResizeSpawnArea(Vector2 min, Vector2 max) {
    spawnAreaMin = min;
    spawnAreaMax = max;
}

private bool spawningPaused = false;
public void PauseSpawning() {
    spawningPaused = true;
}
public void ResumeSpawning() {
    spawningPaused = false;
}

public int scoreThreshold = 100;
public void SpawnOnScore(int score) {
    if (score >= scoreThreshold) SpawnMonster();
}

public void SpawnWithRandomRotation() {
    Quaternion rot = Quaternion.Euler(0, 0, Random.Range(0, 360));
    Instantiate(monsterPrefab, spawnPosition, rot);
}

public void ChainSpawnOnDeath(GameObject monster) {
    monster.GetComponent<Monster>().OnDeath += SpawnMonster;
}

public Color spawnEffectColor = Color.white;
public void SetSpawnEffectColor(Color color) {
    spawnEffectColor = color;
}

public Collider2D triggerZone;
public void SpawnOnTriggerZone() {
    if (triggerZone != null && triggerZone.OverlapPoint(spawnPosition)) SpawnMonster();
}

public void ShowSpawnPreview(Vector2 pos) {
    // Editor-only: draw preview at pos
}

public void SpawnWithRandomScale() {
    GameObject m = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
    float s = Random.Range(0.5f, 1.5f);
    m.transform.localScale = Vector3.one * s;
}

public float respawnDelay = 2f;
public void DelaySpawnAfterPlayerRespawn() {
    Invoke(nameof(SpawnMonster), respawnDelay);
}

public Vector2[] multiSpawnPoints;
public void SpawnAtMultiplePoints() {
    foreach (var pos in multiSpawnPoints) Instantiate(monsterPrefab, pos, Quaternion.identity);
}

public void LogMonsterSpawn(GameObject monster) {
    Debug.Log($"Monster spawned: {monster.name} at {monster.transform.position}");
}

public void SpawnWithAIState(string state) {
    GameObject m = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
    m.GetComponent<Monster>().SetAIState(state);
}

