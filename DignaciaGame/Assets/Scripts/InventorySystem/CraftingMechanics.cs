
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingMechanics : MonoBehaviour
{
    public InventoryObject inventory;
    [Header("Materials")]
    public Item[] item;
    public Sprite[] neededItemPictures;
    public Image[] neededItems;
    [Header("Product")]
    public Item[] craftedItems;
    public Sprite itemPicture;
    
    
    private static string willcraft = "null";


    [SerializeField] GameObject ItemPictureGameObject;






    //Başında ..Set olan fonksiyonlar craft yapmadan önce karakterin neye ihtiyacının olduğunu göstermek için yapılmıştır.
    public void PatatesSet()
    {
        ItemPictureGameObject.GetComponent<Image>().sprite = itemPicture;
        neededItems[0].GetComponent<Image>().sprite = neededItemPictures[0];
        neededItems[1].GetComponent<Image>().sprite = neededItemPictures[1];
        willcraft = "Patates";
        Debug.Log(willcraft);
        

            
        

    }
    public void OdunSet()
    {
        ItemPictureGameObject.GetComponent<Image>().sprite = itemPicture;
        neededItems[0].GetComponent<Image>().sprite = neededItemPictures[0];
        neededItems[1].GetComponent<Image>().sprite = neededItemPictures[1];
        neededItems[2].GetComponent<Image>().sprite = neededItemPictures[2];
        willcraft = "Kütük";

    }
    //Başında craft olanlar ise butona basıldığında gerekli materyalin yapılması için oluşturulmuştur.
    public void CraftRemove(Item item, int _amount)
    {

        inventory.RemoveItem(item, _amount);

    }
    public void CraftAdd(Item craftedItems, int _amountt)
    {
        inventory.AddItem(craftedItems, _amountt);

    }
    public void PatatesCraft()
    {
        CraftAdd(craftedItems[0], 1);
        
        CraftRemove(item[1] , 1);
        CraftRemove(item[0] , 2);
        


    }
    public void KütükCraft()
    {
        CraftRemove(item[0], 2);
        CraftRemove(item[1], 1);
        CraftAdd(craftedItems[0], 1);
        

    }

    public void CraftButton()
    {
        Debug.Log(willcraft);
        
        if(willcraft == "Patates")
        {
            Debug.Log("Deneme");
            PatatesCraft();
           
 
        }
        if(willcraft == "Kütük")
        {
            KütükCraft();
            
        }
    }




}
