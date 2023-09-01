using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManager inventorymanager;
    public Item[] itemsToPickup;

    public void PickupItem(int id)
    {
       bool result = inventorymanager.AddItem(itemsToPickup[id]); // id si olan itemleri ekler item kýsmýný eklediðiniz itemler 0 dan baþlayarak sýrayla artmakta
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

        Item receivedItem = inventorymanager.GetSelectedItem(false); // bu void item hakkýnda bilgi veriyor .GetSelectedItem(false) olduðu için itemi yok etmez 
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

        Item receivedItem = inventorymanager.GetSelectedItem(true); // true olduðu için bu void kullanýlýrsa item 1 azalýr
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
