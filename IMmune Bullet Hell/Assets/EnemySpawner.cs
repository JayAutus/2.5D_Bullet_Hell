using System.Collections;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    [Header("Spawn Settings")]
    public GameObject enemyPrefab;
    public int enemyCount;
    public float spawnInterval;

    [Header("Path Settings")]
    public Transform waypointParent;  // drag your PathX parent here
}

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Define each wave: prefab, count, spawn interval, and path parent")]
    public EnemyWave[] waves;

    [Tooltip("Where enemies will spawn")]
    public Transform[] spawnPoints;

    private int currentWaveIndex = 0;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (currentWaveIndex < waves.Length)
        {
            EnemyWave wave = waves[currentWaveIndex];

            // 1) Spawn this wave’s enemies
            for (int i = 0; i < wave.enemyCount; i++)
            {
                Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject enemy = Instantiate(wave.enemyPrefab, sp.position, sp.rotation);

                // Assign path from the parent’s children
                if (wave.waypointParent != null)
                {
                    int count = wave.waypointParent.childCount;
                    Transform[] path = new Transform[count];
                    for (int j = 0; j < count; j++)
                        path[j] = wave.waypointParent.GetChild(j);

                    var movement = enemy.GetComponent<EnemyMovement>();
                    if (movement != null)
                        movement.SetWaypoints(path);
                }

                yield return new WaitForSeconds(wave.spawnInterval);
            }

            // 2) WAIT until **all** enemies tagged "Enemy" are gone
            while (GameObject.FindGameObjectsWithTag("Enemy").Length > 0)
            {
                yield return null; // check again next frame
            }

            // (Optional) small pause before next wave
            yield return new WaitForSeconds(1f);

            currentWaveIndex++;
        }

        Debug.Log("All waves completed!");
    }
}
