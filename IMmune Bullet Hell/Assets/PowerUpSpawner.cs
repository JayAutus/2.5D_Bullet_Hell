using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public float spawnInterval = 10f;
    public Vector3 spawnArea = new Vector3(10, 0, 10); // X and Z area

    void Start()
    {
        InvokeRepeating("SpawnPowerUp", 3f, spawnInterval);
    }

    void SpawnPowerUp()
    {
        Vector3 spawnPos = new Vector3(
            Random.Range(-spawnArea.x, spawnArea.x),
            0.5f, // Keep it above ground slightly
            Random.Range(-spawnArea.z, spawnArea.z)
        );

        Instantiate(powerUpPrefab, spawnPos, Quaternion.identity);
    }
}
