using System.Collections.Generic;
using UnityEngine;

public class RanchManager : MonoBehaviour
{
    [Header("Building Variables")]
    [Space]

    [Tooltip("Yapý içerisinde kaç hayvan bulundurulabileceðini belirten deðerdir.")]
    public int buildingCapacity;
    [Tooltip("Çiftlikte bulunan hayvan miktarýdýr.")]
    [SerializeField] int currentAnimalCount;
    enum BuildingType { Barn, Coop };
    [Tooltip("Yapýnýn türünü belirler. Yapý bir ahýrsa 'Barn', kümes ise 'coop' seçeneði iþaretlenmelidir.")]
    [SerializeField] BuildingType buildingType;

    public List<RanchAnimal> animalsInRanch = new List<RanchAnimal>();

    [Header("Ranch Stats")]
    [Space]

    [Tooltip("Su ve yem miktarýdýr. Ne kadar çok olursa, hayvanlarýn susuzluðu ve açlýðý o kadar az olur.")]
    public float waterLevel = 100f;
    public float foodLevel = 100f;
    [Tooltip("Yemeðin/suyun saniye baþýna azalma miktarýdýr.")]
    public float waterDecreaseRate = 0f;
    public float foodDecreaseRate = 0f; 

    [Tooltip("Mevcut üretilmiþ ürün miktarýdýr.")]
    public int productCount;

    [Header("Buy Animal")]
    [Space]

    [Tooltip("Spawn edilecek hayvanlarýn prefableridir.")]
    [SerializeField] GameObject cowPrefab;
    [SerializeField] GameObject goatPrefab;
    [SerializeField] GameObject sheepPrefab;
    [SerializeField] GameObject horsePrefab;
    [SerializeField] GameObject chickenPrefab;
    [SerializeField] GameObject goosePrefab;
    [Tooltip("Hayvan satýn alýndýðýnda spawn olacaðý noktadýr.")]
    [SerializeField] Transform animalSpawnPoint;

    [Header("User Interface Part")]
    [Space]

    [Tooltip("Yapý ile etkileþime girildiðinde çýkan seçenek panelidir.")]
    public GameObject optionsPanel;
    [SerializeField] GameObject buyAnimalPanel;

    void Start()
    {
        /*switch(buildingType)
        {
            case BuildingType.Barn: priceOfAnimal = 5000f; break;
            case BuildingType.Coop: priceOfAnimal = 2000f; break;
        }*/
    }
    void Update()
    {
        currentAnimalCount = animalsInRanch.Count;
        foodLevel = Mathf.Clamp(foodLevel, 0, 100f);
        waterLevel = Mathf.Clamp(waterLevel, 0, 100f);

        foodLevel -= foodDecreaseRate * Time.deltaTime;
        waterLevel -= waterDecreaseRate * Time.deltaTime;
    }
    //Ürünleri toplama butonu
    public void CollectProducts()
    {
        productCount = 0;
        switch(buildingType)
        {
            case BuildingType.Barn: //Envanter kodu ile birleþtirilecek
                break;
            case BuildingType.Coop: //Envanter kodu ile birleþtirilecek
                break;
        }
    }
    public void FillWater()
    {
        //Envanterde su taþýyýp taþýmadýðý kontrol edildikten sonra
        waterLevel += 20f;
        //Envanterden bir suyu yok et
    }
    public void Feed()
    {
        //Envanterde yem taþýyýp taþýmadýðý kontrol edildikten sonra
        foodLevel += 20f;
        //Envanterden bir yemi yok et
    }
    public void AddAnimal()
    {
        buyAnimalPanel.SetActive(true);
    }
    public void BuyAnimalButton(string animalName)
    {
        //PARA KONTROLÜ YAPILACAK

        if(currentAnimalCount < buildingCapacity)
        {
            switch (animalName)
            {
                case "Cow":
                    GameObject cow = Instantiate(cowPrefab, animalSpawnPoint.transform);
                    animalsInRanch.Add(cow.GetComponent<RanchAnimal>());
                    cow.GetComponent<RanchAnimal>().ranch = this;
                    break;
                case "Goat":
                    GameObject goat = Instantiate(goatPrefab, animalSpawnPoint.transform);
                    animalsInRanch.Add(goat.GetComponent<RanchAnimal>());
                    goat.GetComponent<RanchAnimal>().ranch = this;
                    break;
                case "Sheep":
                    GameObject sheep = Instantiate(sheepPrefab, animalSpawnPoint.transform);
                    animalsInRanch.Add(sheep.GetComponent<RanchAnimal>());
                    sheep.GetComponent<RanchAnimal>().ranch = this;
                    break;
                case "Horse":
                    GameObject horse = Instantiate(horsePrefab, animalSpawnPoint.transform);
                    animalsInRanch.Add(horse.GetComponent<RanchAnimal>());
                    horse.GetComponent<RanchAnimal>().ranch = this;
                    break;
                case "Chicken":
                    GameObject chicken = Instantiate(chickenPrefab, animalSpawnPoint.transform);
                    animalsInRanch.Add(chicken.GetComponent<RanchAnimal>());
                    chicken.GetComponent<RanchAnimal>().ranch = this;
                    break;
                case "Goose":
                    GameObject goose = Instantiate(goosePrefab, animalSpawnPoint.transform);
                    animalsInRanch.Add(goose.GetComponent<RanchAnimal>());
                    goose.GetComponent<RanchAnimal>().ranch = this;
                    break;
            }
            buyAnimalPanel.SetActive(false);
            optionsPanel.SetActive(false);
            currentAnimalCount++;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            //Etkileþime geçme yazýsý
            if(Input.GetButtonDown("Interact") && !optionsPanel.activeSelf)
            {
                optionsPanel.SetActive(true);
            }
            else if (Input.GetButtonDown("Interact") && optionsPanel.activeSelf)
            {
                optionsPanel.SetActive(false);
                buyAnimalPanel.SetActive(false);
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Etkileþime geçme yazýsý
            if (Input.GetButtonDown("Interact") && !optionsPanel.activeSelf)
            {
                optionsPanel.SetActive(true);
            }
            else if (Input.GetButtonDown("Interact") && optionsPanel.activeSelf)
            {
                optionsPanel.SetActive(false);
                buyAnimalPanel.SetActive(false);
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            optionsPanel.SetActive(false);
            buyAnimalPanel.SetActive(false);
        }
    }
}
