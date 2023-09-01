using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs; // Spawn edilecek itemlar�n prefablar�
    public Transform[] spawnPoints; // Itemlar�n spawn edilece�i noktalar
    public float spawnrate;
    private void Start()
    {
        SpawnItems();
    }

    private void SpawnItems()
    {
        spawnrate = Random.Range(1, 2); // 1 ve 10 aras�nda 1 ve 10 dahil int say� olu�turur.
        Debug.Log(spawnrate);
        if (spawnrate == 1)
        {
  foreach (Transform spawnPoint in spawnPoints)
        {
            // Rastgele bir item prefab� se�me
            GameObject randomItemPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

            // Item� spawn etme
            Instantiate(randomItemPrefab, spawnPoint.position, Quaternion.identity);
        }
        }
        else
        {

        }
    }
}
