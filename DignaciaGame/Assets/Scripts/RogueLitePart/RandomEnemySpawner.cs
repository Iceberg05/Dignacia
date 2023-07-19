using UnityEngine;

public class RandomEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;      // Spawnlanacak düþmanýn prefabý
    public int spawnCount;              // Editöre girilen spawn sayýsý
    public GameObject[] spawnPoints;    // Spawn noktalarý
    public bool spawnEnabled;           // Spawn iþleminin açýk/kapalý durumu

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
            Debug.LogError("Spawn noktalarý ata la");
            return null;
        }

        int randomIndex = Random.Range(0, spawnPoints.Length);
        GameObject randomSpawnPoint = spawnPoints[randomIndex];
        return randomSpawnPoint;
    }
}



