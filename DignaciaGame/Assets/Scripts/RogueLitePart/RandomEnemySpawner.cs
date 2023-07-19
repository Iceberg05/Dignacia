using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;      // Spawnlanacak d��man�n prefab�
    public int spawnCount;              // Edit�re girilen spawn say�s�
    public GameObject[] spawnPoints;    // Spawn noktalar�
    public bool spawnEnabled;           // Spawn i�leminin a��k/kapal� durumu

    private void Update()
    {
        if (spawnEnabled)
        {
            SpawnEnemies();
            spawnEnabled = false;  
        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < spawnCount; i++)
        {
           
            GameObject randomSpawnPoint = GetRandomSpawnPoint();

            if (randomSpawnPoint != null)
            {
                
                Instantiate(enemyPrefab, randomSpawnPoint.transform.position, Quaternion.identity);
            }
        }
    }

    private GameObject GetRandomSpawnPoint()
    {
        if (spawnPoints.Length == 0)
        {
            Debug.LogError("Spawn noktalar� ata la");
            return null;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        GameObject randomSpawnPoint = spawnPoints[randomIndex];
        return randomSpawnPoint;
    }
}



