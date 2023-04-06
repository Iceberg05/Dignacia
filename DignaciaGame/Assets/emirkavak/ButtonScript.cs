using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public ItemObject item;
    public Character character;
    public InventoryObject inventory;
    public ItemDatabaseObject Itemdatabase;

    public void BuyRock()
    {
          
          Item _item = new Item(item);
          Debug.Log(_item.Id);
          inventory.AddItem(_item, 1);
          character.money = character.money - 20;



    }

    public void SellRock()
    {

        Item _item = new Item(item);
        Debug.Log(_item.Id);
        inventory.RemoveItem(_item, 1);
        character.money = character.money + 10;


    }



    //
    //        


}
