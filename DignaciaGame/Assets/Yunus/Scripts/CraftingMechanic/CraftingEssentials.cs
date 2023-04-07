using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingEssentials : MonoBehaviour
{
    SpriteRenderer sr;


   public Sprite breadSprite;
   public Sprite breadSprite2;
   public Sprite breadSprite3;
   
   [SerializeField] GameObject material1;
   [SerializeField] GameObject material2;
   [SerializeField] GameObject material3;

   [SerializeField] Text materialText1;
   [SerializeField] Text materialText2;
   [SerializeField] Text materialText3;
   [SerializeField] Text description;
   
   

   
    
    private void Start() {
        sr = GetComponent<SpriteRenderer>();
    }
    void OnMouseDown()
    {
        if(this.gameObject.name == "Object1")
        {
            Test1();

        }
        else if(this.gameObject.name == "Object2")
        {
            Test2();

        }
        else if(this.gameObject.name == "Object3")
        {
            Test3();

        }
        
    }

    void Test1()
    {
        material1.GetComponent<SpriteRenderer>().sprite = breadSprite;
        material2.GetComponent<SpriteRenderer>().sprite = breadSprite;
        material3.GetComponent<SpriteRenderer>().sprite = breadSprite;
        materialText1.text = "1";
        materialText2.text = "2";
        materialText3.text = "3";
        description.text = "makimam da makimam";

    }
    void Test2()
    {
        material1.GetComponent<SpriteRenderer>().sprite = breadSprite2;
        material2.GetComponent<SpriteRenderer>().sprite = breadSprite2;
        material3.GetComponent<SpriteRenderer>().sprite = breadSprite2;
        materialText1.text = "4";
        materialText2.text = "5";
        materialText3.text = "9";
        description.text = "Antalyam da antalyam";

    }
    void Test3()
    {
        material1.GetComponent<SpriteRenderer>().sprite = breadSprite3;
        material2.GetComponent<SpriteRenderer>().sprite = breadSprite3;
        material3.GetComponent<SpriteRenderer>().sprite = breadSprite3;
        materialText1.text = "2";
        materialText2.text = "3";
        materialText3.text = "4";
        description.text = "Silksong Lütfen Çıksın Artık";

    }
    
  
}
