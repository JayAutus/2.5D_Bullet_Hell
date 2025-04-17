using System.Collections;
using UnityEngine;

[System.Serializable]
public class EnemyWave
{
    public GameObject enemyPrefab;
    public int enemyCount;
    public float spawnInterval;
}

public class EnemySpawner : MonoBehaviour
{
    public EnemyWave[] waves;
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

            for (int i = 0; i < wave.enemyCount; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Instantiate(wave.enemyPrefab, spawnPoint.position, spawnPoint.rotation);
                yield return new WaitForSeconds(wave.spawnInterval);
            }

            currentWaveIndex++;

            // Delay before next wave
            yield return new WaitForSeconds(3f);
        }

        Debug.Log("All waves completed!");
    }
}
