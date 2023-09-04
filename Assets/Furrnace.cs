using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furrnace: MonoBehaviour
{
    public List<Item> requiredItems = new List<Item>(); // Gerekli öðelerin listesi
    public List<Item> craftedItems = new List<Item>(); // Birleþtirilen öðelerin listesi

    public InventoryManager inventoryManager;

    public void Craft()
    {
        
        // Envantördeki belirli öðelerin sayýsýný kontrol edioz
        int requiredItemCount1 = inventoryManager.GetItemCount(requiredItems[0]);
        int requiredItemCount2 = inventoryManager.GetItemCount(requiredItems[1]);
        // Ýki gerekli öðe de envanterde yeterli sayýda varsa birleþtirme iþlemi yapýoz
        if (requiredItemCount1 >= 1 && requiredItemCount2 >= 1)
        {
            // Gerekli öðeleri envanterden kaldýrýoz
            inventoryManager.RemoveItem(requiredItems[0]);
            inventoryManager.RemoveItem(requiredItems[1]);

            // Yeni birleþtirilmiþ öðeyi envantere ekleyiyoz
            inventoryManager.AddItem(craftedItems[1]);

            Debug.Log("Birleþtirme baþarýlý");
        }
        else
        {
            // Yeterli öðe yoksa hata mesajý verioz
            Debug.Log("Birleþtirme baþarýsýz");
        }
    }
}