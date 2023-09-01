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
                    // E�er envanter yuvas�nda bir ��e varsa, onu yere d���r�n
                    GameObject droppedItem = Instantiate(inventoryItemPrefab, Player.transform.position, Quaternion.identity);

                    // D��en ��enin i�eri�ini envanter yuvas�ndaki ��eden kopyalayarak ayarlay�n
                    droppedItem.GetComponent<CollectItem>().Initialize(itemInSlot.item, itemInSlot.count); // Stacklenmi� ��elerin say�s�n� da aktar

                    // Envantersiz yuvay� bo�alt�n
                    Destroy(itemInSlot.gameObject);
                }
            }
        }
    }
}