using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class tablet_BuyItem : MonoBehaviour
{
    [Tooltip("sat�n alma yerindeki itemleri burdan ekliyoruz")]
    public ItemObject[] items;
    [Tooltip("sat�n alma yerindeki itemlerin desc k�sm�ndaki g�rsellerini burdan ekliyoruz")]
    public Sprite[] slotimages; 
    [Tooltip("buttonu buraya ekliyoruz")]
    public GameObject[] buybuttons;
    
    public Character character;
    public InventoryObject inventory;
    public ItemDatabaseObject Itemdatabase;
    [Tooltip("item eklerken desc de de�i�en yaz�")]
    public Text desc;
    [Tooltip("item eklerken desc de de�i�en fiyat")]
    public Text price;
    public Image descim;
  

   
    public void Item1()
    {
        desc.text = "bu bir adam";
        price.text = "100";
        descim.sprite = slotimages[0];
        buybuttons[0].SetActive(true);
    }
    public void Item1buy()
    {
        character.moneyValue = character.moneyValue - 100;
        Item _item = new Item(items[0]);
        inventory.AddItem(_item, 1);
    }


}
