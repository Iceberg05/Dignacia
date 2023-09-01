//using UnityEngine;
//using UnityEngine.UI;
//public class TabletSellItem : MonoBehaviour
//{
//    //BU KOD SORULACAK

//    [Tooltip("satma yerindeki itemleri burdan ekliyoruz")]
//    public ItemObject[] items;
//    [Tooltip("satma yerindeki itemlerin desc kýsmýndaki görsellerini burdan ekliyoruz")]
//    public Sprite[] slotimages; 
//    [Tooltip("buttonu buraya ekliyoruz")]
//    public GameObject[] sellbuttons;
    
//    public Character character;
//    public InventoryObject inventory;
//    public ItemDatabaseObject database;
//    public Inventory Container;
//    [Tooltip("item eklerken desc de deðiþen yazý")]
//    public Text desc;
//    [Tooltip("item eklerken desc de deðiþen fiyat")]
//    public Text price;
//    public Image descim;
  
//    public void Item1()
//    {
//        desc.text = "bu bir adam";
//        price.text = "100";
//        descim.sprite = slotimages[0];
//        sellbuttons[0].SetActive(true);
//    }
//    public void Item1sell(Item _item)
//    {
//        for (int i = 0; i < Container.Items.Length; i++)
//        {
//            if (Container.Items[i].ID == _item.Id)
//            {
//                _item = new Item(items[0]);
//                inventory.RemoveItem(_item, 1);
//                character.moneyValue = character.moneyValue + 100;
//            }
//        }
//    }
//}
