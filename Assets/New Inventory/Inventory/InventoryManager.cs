using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int MaxStackedItems = 4;
    public InventorySlott[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public Item itemss;
    int selectedSlot = -1;


    private void Update()
    {
        if (Input.inputString != null)
        {
            bool isNumber = int.TryParse(Input.inputString, out int number);
            if (isNumber && number > 0 && number < 4)
            {
                ChangeSelectedSlot(number - 1);
            }
        }
    }

    void ChangeSelectedSlot(int newValue)     //itemlerin se�ili oldu�u slotun de�i�mesini sa�layan kod 1-8 aras� tu�lar �uan
    {
        if (selectedSlot >= 0)
        {
         inventorySlots[selectedSlot].DeSelect();
        }  
        inventorySlots[newValue].Select();
        selectedSlot = newValue;

    }
    public bool AddItem (Item item )
    {
        //itemlerin stacklenmesini sa�layan kod
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlott slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();


            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < MaxStackedItems && itemInSlot.item.stackable == true) // e�er bo� ise item ekliyoruz
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
            
          
        }

        //itemlerin eklenmesini sa�layan kod
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlott slot = inventorySlots[i];
            //  Burda item eklenecek slotun bo� olup olmad���n� kontrol ediyoruz 
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null) // e�er bo� ise item ekliyoruz
            {
                SpawnNewItem(item, slot);
                return true;
            }
        }

        return false;
    }

    void SpawnNewItem(Item item, InventorySlott slot)
    {

        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    public Item GetSelectedItem(bool use) //BOOL TRUE OLURSA O �TEM� YOK ETMES�N� S�YL�YORUZ<
    {
        InventorySlott slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();  //burda kullanmak i�in atamalar yap�l�yor
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if (use == true)
            {
                itemInSlot.count--;    // item kullan�l�rsa (bool true olan bi fonksiyon �a��r�l�rsa 1 azal�r item
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject); //item 0 dan k���kse yok olur
                }
                else
                {
                    itemInSlot.RefreshCount();
                }
            }
            return item;
        }
        return null;

    }

}
