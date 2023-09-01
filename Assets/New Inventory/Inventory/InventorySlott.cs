using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlott : MonoBehaviour, IDropHandler
{
    public Image image;
    public Color selectedColor, notSelectedColor;

    private void Awake()
    {
        DeSelect();
    }
    public void Select() // se�ilen slotun rengini ayarlama kodlar�
    {
        image.color = selectedColor; 
    }


    public void DeSelect()
    {
        image.color = notSelectedColor;
    }
    public void OnDrop(PointerEventData eventData) {
        if (transform.childCount == 0)
        {
            InventoryItem inventoryItem = eventData.pointerDrag.GetComponent<InventoryItem>();
            inventoryItem.parentAfterDrag = transform;
        }
    
    
    
    
    }


}
