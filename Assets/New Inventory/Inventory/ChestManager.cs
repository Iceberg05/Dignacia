using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public int MaxStackedItems = 4;
    public InventorySlott[] inventorySlots;
    public GameObject inventoryItemPrefab;
    public bool chestDestroyed;
    public GameObject Player;

    private void Update()
    {
        if (chestDestroyed == true)
        {
            foreach (InventorySlott slot in inventorySlots)
            {
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();

                if (itemInSlot != null)
                {
                    // Eðer envanter yuvasýnda bir öðe varsa, onu yere düþürün
                    GameObject droppedItem = Instantiate(inventoryItemPrefab, Player.transform.position, Quaternion.identity);

                    // Düþen öðenin içeriðini envanter yuvasýndaki öðeden kopyalayarak ayarlayýn
                    droppedItem.GetComponent<CollectItem>().Initialize(itemInSlot.item, itemInSlot.count); // Stacklenmiþ öðelerin sayýsýný da aktar

                    // Envantersiz yuvayý boþaltýn
                    Destroy(itemInSlot.gameObject);
                }
            }
        }
    }
}