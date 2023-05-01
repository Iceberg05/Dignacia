using UnityEngine;
using TMPro;

public class HealthSystem : MonoBehaviour
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
    void Update()
    {
        #region CLAMP_VARIABLES
        health = Mathf.Clamp(health, 0, 100);
        armor = Mathf.Clamp(armor, 0, 100);
        #endregion

        healthText.text = "Health : " + health;
        ArmorText.text = "Armor: " + armor;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "HealthPackage")
        {
            HealthPackage();
        }
        if (col.gameObject.tag == "ArmorPackage")
        {
            ArmorPackage();
        }
    }
    void HealthPackage()
    {
        if (health + 50 > 100)
        {
            health = 100;
        }
        else
        {
            health += 50;
        }
    }
    void ArmorPackage()
    {
        if (armor + 50 > 100)
        {
            armor = 100;
        }
        else
        {
            armor += 50;
        }
    }
}
