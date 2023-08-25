using System.Collections.Generic;
using UnityEngine;

public class RanchManager : MonoBehaviour
{
    [Header("Building Variables")]
    [Space]

    [Tooltip("Yap� i�erisinde ka� hayvan bulundurulabilece�ini belirten de�erdir.")]
    public int buildingCapacity;
    [Tooltip("�iftlikte bulunan hayvan miktar�d�r.")]
    [SerializeField] int currentAnimalCount;
    enum BuildingType { Barn, Coop };
    [Tooltip("Yap�n�n t�r�n� belirler. Yap� bir ah�rsa 'Barn', k�mes ise 'coop' se�ene�i i�aretlenmelidir.")]
    [SerializeField] BuildingType buildingType;

    public List<RanchAnimal> animalsInRanch = new List<RanchAnimal>();

    [Header("Ranch Stats")]
    [Space]

    [Tooltip("Su ve yem miktar�d�r. Ne kadar �ok olursa, hayvanlar�n susuzlu�u ve a�l��� o kadar az olur.")]
    public float waterLevel = 100f;
    public float foodLevel = 100f;
    [Tooltip("Yeme�in/suyun saniye ba��na azalma miktar�d�r.")]
    public float waterDecreaseRate = 0f;
    public float foodDecreaseRate = 0f; 

    [Tooltip("Mevcut �retilmi� �r�n miktar�d�r.")]
    public int productCount;

    [Header("Buy Animal")]
    [Space]

    [Tooltip("Spawn edilecek hayvanlar�n prefableridir.")]
    [SerializeField] GameObject cowPrefab;
    [SerializeField] GameObject goatPrefab;
    [SerializeField] GameObject sheepPrefab;
    [SerializeField] GameObject horsePrefab;
    [SerializeField] GameObject chickenPrefab;
    [SerializeField] GameObject goosePrefab;
    [Tooltip("Hayvan sat�n al�nd���nda spawn olaca�� noktad�r.")]
    [SerializeField] Transform animalSpawnPoint;

    [Header("User Interface Part")]
    [Space]

    [Tooltip("Yap� ile etkile�ime girildi�inde ��kan se�enek panelidir.")]
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
    //�r�nleri toplama butonu
    public void CollectProducts()
    {
        productCount = 0;
        switch(buildingType)
        {
            case BuildingType.Barn: //Envanter kodu ile birle�tirilecek
                break;
            case BuildingType.Coop: //Envanter kodu ile birle�tirilecek
                break;
        }
    }
    public void FillWater()
    {
        //Envanterde su ta��y�p ta��mad��� kontrol edildikten sonra
        waterLevel += 20f;
        //Envanterden bir suyu yok et
    }
    public void Feed()
    {
        //Envanterde yem ta��y�p ta��mad��� kontrol edildikten sonra
        foodLevel += 20f;
        //Envanterden bir yemi yok et
    }
    public void AddAnimal()
    {
        buyAnimalPanel.SetActive(true);
    }
    public void BuyAnimalButton(string animalName)
    {
        //PARA KONTROL� YAPILACAK

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
            //Etkile�ime ge�me yaz�s�
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
            //Etkile�ime ge�me yaz�s�
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
