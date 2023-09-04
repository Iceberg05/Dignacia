using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furrnace: MonoBehaviour
{
    public List<Item> requiredItems = new List<Item>(); // Gerekli ��elerin listesi
    public List<Item> craftedItems = new List<Item>(); // Birle�tirilen ��elerin listesi

    public InventoryManager inventoryManager;

    public void Craft()
    {
        
        // Envant�rdeki belirli ��elerin say�s�n� kontrol edioz
        int requiredItemCount1 = inventoryManager.GetItemCount(requiredItems[0]);
        int requiredItemCount2 = inventoryManager.GetItemCount(requiredItems[1]);
        // �ki gerekli ��e de envanterde yeterli say�da varsa birle�tirme i�lemi yap�oz
        if (requiredItemCount1 >= 1 && requiredItemCount2 >= 1)
        {
            // Gerekli ��eleri envanterden kald�r�oz
            inventoryManager.RemoveItem(requiredItems[0]);
            inventoryManager.RemoveItem(requiredItems[1]);

            // Yeni birle�tirilmi� ��eyi envantere ekleyiyoz
            inventoryManager.AddItem(craftedItems[1]);

            Debug.Log("Birle�tirme ba�ar�l�");
        }
        else
        {
            // Yeterli ��e yoksa hata mesaj� verioz
            Debug.Log("Birle�tirme ba�ar�s�z");
        }
    }
}