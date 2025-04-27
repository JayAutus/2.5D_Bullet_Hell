using System.Collections;
using UnityEngine;

[System.Serializable]
public class EnemyWaveSettings
{
    public GameObject[] enemyPrefabs;
    public int minEnemyCount = 5;
    public int maxEnemyCount = 15;
    public float spawnInterval = 1.0f;

    public GameObject bossPrefab;
    public int bossWaveInterval = 5;

    [Header("Multiple Paths")]
    public Transform[] waypointGroups;  // Each is a parent with child waypoints
}

public class EnemySpawner : MonoBehaviour
{
    public EnemyWaveSettings waveSettings;
    public Transform[] spawnGroups;

    private int waveNumber = 1;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (true) // Endless loop
        {
            Debug.Log($"Spawning Wave {waveNumber}");

            bool isBossWave = (waveNumber % waveSettings.bossWaveInterval == 0);

            if (isBossWave && waveSettings.bossPrefab != null)
            {
                SpawnEnemy(waveSettings.bossPrefab);
            }
            else
            {
                int enemyCount = Random.Range(waveSettings.minEnemyCount, waveSettings.maxEnemyCount + 1);
                for (int i = 0; i < enemyCount; i++)
                {
                    GameObject prefab = waveSettings.enemyPrefabs[Random.Range(0, waveSettings.enemyPrefabs.Length)];
                    SpawnEnemy(prefab);
                    yield return new WaitForSeconds(waveSettings.spawnInterval);
                }
            }

            // Wait until all enemies are gone
            while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
            {
                yield return null;
            }

            yield return new WaitForSeconds(2f);
            waveNumber++;
        }
    }

    void SpawnEnemy(GameObject prefab)
{
    Transform group = spawnGroups[Random.Range(0, spawnGroups.Length)];
    if (group.childCount == 0) return;

    Transform spawnPoint = group.GetChild(Random.Range(0, group.childCount));
    GameObject enemy = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

    // Select a random waypoint group
    if (waveSettings.waypointGroups.Length > 0)
    {
        Transform pathGroup = waveSettings.waypointGroups[Random.Range(0, waveSettings.waypointGroups.Length)];
        int count = pathGroup.childCount;
        Transform[] path = new Transform[count];
        for (int j = 0; j < count; j++)
            path[j] = pathGroup.GetChild(j);

        var movement = enemy.GetComponent<EnemyMovement>();
        if (movement != null)
            movement.SetWaypoints(path);
    }
}

}
