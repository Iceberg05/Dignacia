using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour
{
    public SpriteRenderer sr;
    public Item item;
    public InventoryManager inventorymanager;
    public int count;

    // Start is called before the first frame update

    private void Start()
    {
        inventorymanager = GameObject.FindObjectOfType<InventoryManager>();
        sr.sprite = item.image;
        if (count == 0)
        {
            count = 1;
        }
    }
    public void Initialize(Item item, int count)
    {
        this.count = count;
        this.item = item;
        sr.sprite = item.image;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < count; i++)
            {
                bool result = inventorymanager.AddItem(item); // envanter dolu mu diye kontrol ediyoruz
                if (result)
                {
                    Debug.Log("Item added"); // eðer envanter boþsa (true), nesneyi ekleyip yok ediyoruz
                    Destroy(this.gameObject);
                }
                else
                {
                    Debug.Log("Item not added due to inventory full"); // eðer envanter doluysa (false), sadece log mesajý veriyoruz
                }
            }
        }
    }

}
