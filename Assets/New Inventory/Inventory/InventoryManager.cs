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

    void ChangeSelectedSlot(int newValue)     //itemlerin seçili olduðu slotun deðiþmesini saðlayan kod 1-8 arasý tuþlar þuan
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
        //itemlerin stacklenmesini saðlayan kod
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlott slot = inventorySlots[i];
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();


            if (itemInSlot != null && itemInSlot.item == item && itemInSlot.count < MaxStackedItems && itemInSlot.item.stackable == true) // eðer boþ ise item ekliyoruz
            {
                itemInSlot.count++;
                itemInSlot.RefreshCount();
                return true;
            }
            
          
        }

        //itemlerin eklenmesini saðlayan kod
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            InventorySlott slot = inventorySlots[i];
            //  Burda item eklenecek slotun boþ olup olmadýðýný kontrol ediyoruz 
            InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
            if (itemInSlot == null) // eðer boþ ise item ekliyoruz
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

    public Item GetSelectedItem(bool use) //BOOL TRUE OLURSA O ÝTEMÝ YOK ETMESÝNÝ SÖYLÜYORUZ<
    {
        InventorySlott slot = inventorySlots[selectedSlot];
        InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();  //burda kullanmak için atamalar yapýlýyor
        if (itemInSlot != null)
        {
            Item item = itemInSlot.item;
            if (use == true)
            {
                itemInSlot.count--;    // item kullanýlýrsa (bool true olan bi fonksiyon çaðýrýlýrsa 1 azalýr item
                if (itemInSlot.count <= 0)
                {
                    Destroy(itemInSlot.gameObject); //item 0 dan küçükse yok olur
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
