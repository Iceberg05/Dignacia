using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventorymanager;
    public Item[] itemsToPickup;

    public void PickupItem(int id)
    {
       bool result = inventorymanager.AddItem(itemsToPickup[id]); // id si olan itemleri ekler item k�sm�n� ekledi�iniz itemler 0 dan ba�layarak s�rayla artmakta
        if (result == true)
        {
            Debug.Log("Item added");
        }
        else
        {
            Debug.Log("Item eklenmedi");
        }
    }

    public void GetSelectedItem()
    {

        Item receivedItem = inventorymanager.GetSelectedItem(false); // bu void item hakk�nda bilgi veriyor .GetSelectedItem(false) oldu�u i�in itemi yok etmez 
        if (receivedItem != null)
        {
            Debug.Log("received item " + receivedItem);
        }
        else
        {
            Debug.Log("no item received!");
        }

    }

    public void UseSelectedItem()
    {

        Item receivedItem = inventorymanager.GetSelectedItem(true); // true oldu�u i�in bu void kullan�l�rsa item 1 azal�r
        if (receivedItem != null)
        {
            Debug.Log("received item " + receivedItem);
        }
        else
        {
            Debug.Log("no item received!");
        }

    }
}
