using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trader_Npc : MonoBehaviour
{

    public GameObject store;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            store.SetActive(true);
        }
        else
        {
            store.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            store.SetActive(false);
        }

    }

}
