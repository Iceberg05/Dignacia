using UnityEngine;
public class InventoryButton : MonoBehaviour
{
    public ItemObject item;
    public Character character;
    public InventoryObject inventory;
    public ItemDatabaseObject Itemdatabase;
    public void BuyRock()
    {
        Item _item = new Item(item);
        inventory.AddItem(_item, 1);
        character.moneyValue = character.moneyValue - 20;
    }
    public void SellRock()
    {
        Item _item = new Item(item);
        inventory.RemoveItem(_item, 1);
        character.moneyValue = character.moneyValue + 10;
    }
}
