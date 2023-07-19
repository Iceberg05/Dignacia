using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs; // Spawn edilecek itemlarýn prefablarý
    public Transform[] spawnPoints; // Itemlarýn spawn edileceði noktalar
    public float spawnrate;
    private void Start()
    {
        SpawnItems();
    }

    private void SpawnItems()
    {
        spawnrate = Random.Range(1, 2); // 1 ve 10 arasýnda 1 ve 10 dahil int sayý oluþturur.
        Debug.Log(spawnrate);
        if (spawnrate == 1)
        {
  foreach (Transform spawnPoint in spawnPoints)
        {
            // Rastgele bir item prefabý seçme
            GameObject randomItemPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

            // Itemý spawn etme
            Instantiate(randomItemPrefab, spawnPoint.position, Quaternion.identity);
        }
        }
        else
        {

        }
    }
}
