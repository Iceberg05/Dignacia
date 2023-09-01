using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItems : MonoBehaviour
{
    public int spawnerlevel = 0;
    public GameObject[] itemsToPickup;
    public InventoryManager inventorymanager;
    public void spawnitem(int id)
    {
        if (spawnerlevel == 0)
        {
            StartCoroutine(level0(id));
        }

        if (spawnerlevel == 1)
        {
            StartCoroutine(level1(id));
        }

        if (spawnerlevel == 2)
        {
            StartCoroutine(level2(id));
        }


    }



    IEnumerator level0(int id)
    {
        yield return new WaitForSeconds(10.0f);
        GameObject newItem = Instantiate(itemsToPickup[id], transform.position, Quaternion.identity);
       
        StopAllCoroutines();
    }

    IEnumerator level1(int id)
    {
        yield return new WaitForSeconds(5.0f);
        GameObject newItem = Instantiate(itemsToPickup[id], transform.position, Quaternion.identity);
        StopAllCoroutines();
    }

    IEnumerator level2(int id)
    {
        yield return new WaitForSeconds(3.0f);
        GameObject newItem = Instantiate(itemsToPickup[id], transform.position, Quaternion.identity);
        StopAllCoroutines();
    }

}
