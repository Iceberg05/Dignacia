using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Health : MonoBehaviour
{
    [SerializeField] public static int health;
    [SerializeField] public static int armor;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI ArmorText;

    void Start()
    {
        health = 100;
        armor = 100;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health : " + health;
        ArmorText.text = "Armor: " + armor;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.tag == "HealthPackage")
        {
            HealthPackage();

        }
        if(other.gameObject.tag == "ArmorPackage")
        {
            ArmorPackage();

        }
    }

    
    
    void HealthPackage()
    {
        if(health + 50 > 100)
        {
                health = 100;
        }
        else
        {
                health = health +50;
        }

    }
    void ArmorPackage()
    {
        if(armor + 50 > 100)
            {
                armor = 100;
            }
            else
            {
                armor = armor + 50;
            }
        

    }
}


    

