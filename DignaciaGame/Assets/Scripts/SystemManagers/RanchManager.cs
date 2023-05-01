using System.Collections;
using UnityEngine;

public class RanchManager : MonoBehaviour
{
    [Header("Building Variables")]

    [Tooltip("Yapý içerisinde kaç hayvan bulundurulabileceðini belirten deðerdir.")]
    public int buildingCapacity;
    [Tooltip("Çiftlikte bulunan hayvan miktarýdýr.")]
    [SerializeField] int animalCount;
    enum BuildingType { CowBarn, SheepBarn, HorseBarn, Coop };
    [Tooltip("Yapý ahýr olup inek ahýrýysa bu deðeri 'CowBarn', koyun/keçi/koç ahýrýysa 'SheepBarn', at ahýrýysa 'HorseBarn', yapý direkt kümes ise 'Coop' olarak iþaretleyin.")]
    [SerializeField] BuildingType buildingType;


    [Header("Ranch Stats")]

    [Tooltip("Susuzluk miktarýdýr. Ne kadar az olursa, üretim o kadar çok olur. Bu deðer arttýkça üretim bitmeye baþlar.")]
    [SerializeField] float thirstLevel = 0f;
    [Tooltip("Açlýk miktarýdýr. Ne kadar az olursa, üretim o kadar çok olur. Bu deðer arttýkça üretim bitmeye baþlar.")]
    [SerializeField] float hungerLevel = 0f;
    [Tooltip("Su ve yem miktarýdýr. Ne kadar çok olursa, hayvanlarýn susuzluðu ve açlýðý o kadar az olur.")]
    [SerializeField] float waterLevel = 100f, foodLevel = 100f;
    [Tooltip("Açlýðýn/susuzluðun saniye baþýna artma miktarýdýr.")]
    [SerializeField] float hungerIncreaseRate = 0.1f, thirstIncreaseRate = 0.2f;
    [Tooltip("Yemeðin/suyun saniye baþýna azalma miktarýdýr.")]
    [SerializeField] float waterDecreaseRate = 0.1f, foodDecreaseRate = 0.1f; 

    [Header("Products")]

    public int productCount;
    [Tooltip("Tek bir üretimde çýkan ürün miktarýdýr.")]
    public int productMultiplier = 1;
    [Tooltip("Üretim süresidir.")]
    [SerializeField] float productionTime = 10f;

    bool productionStarted = false;

    [Header("Buy Animal")]

    public float priceOfAnimal;
    [Tooltip("Spawn edilecek hayvanlarýn prefableridir.")]
    [SerializeField] GameObject cowPrefab, sheepPrefab, horsePrefab, chickenPrefab;
    [Tooltip("Hayvan satýn alýndýðýnda spawn olacaðý noktadýr.")]
    [SerializeField] Transform animalSpawnPoint;

    [Header("User Interface Part")]

    [Tooltip("Yapý ile etkileþime girildiðinde çýkan seçenek panelidir.")]
    public GameObject optionsPanel;

    void Start()
    {
        switch(buildingType)
        {
            case BuildingType.CowBarn: priceOfAnimal = 5000f; break;
            case BuildingType.SheepBarn: priceOfAnimal = 3000f; break;
            case BuildingType.HorseBarn: priceOfAnimal = 20000f; break;
            case BuildingType.Coop: priceOfAnimal = 2000f; break;
        }
    }
    void Update()
    {
        hungerLevel = Mathf.Clamp(hungerLevel, 0, 100f);
        thirstLevel = Mathf.Clamp(thirstLevel, 0, 100f);
        foodLevel = Mathf.Clamp(foodLevel, 0, 100f);
        waterLevel = Mathf.Clamp(waterLevel, 0, 100f);

        foodLevel -= foodDecreaseRate * Time.deltaTime;
        waterLevel -= waterDecreaseRate * Time.deltaTime;

        if(waterLevel <= 10)
        {
            thirstLevel += thirstIncreaseRate * Time.deltaTime;
        } else
        {
            thirstLevel -= 1f * Time.deltaTime;
        }

        if(foodLevel <= 10)
        {
            hungerLevel += hungerIncreaseRate * Time.deltaTime;
        } else
        {
            hungerLevel -= 1f * Time.deltaTime;
        }

        if(hungerLevel >= 90 || thirstLevel >= 90)
        {
            StopCoroutine(ProductionPeriod());
            productionStarted = false;
        } else
        {
            if(!productionStarted)
            {
                StartCoroutine(ProductionPeriod());
                productionStarted = true;
            }
        }
    }
    //Ürünleri toplama butonu
    public void CollectProducts()
    {
        switch(buildingType)
        {
            case BuildingType.CowBarn: //Envanter kodu ile birleþtirilecek
                break;
            case BuildingType.SheepBarn: //Envanter kodu ile birleþtirilecek
                break;
            case BuildingType.Coop: //Envanter kodu ile birleþtirilecek
                break;
        }
    }
    //Su verme butonu
    public void FillWater()
    {
        //Envanterde su taþýyýp taþýmadýðý kontrol edildikten sonra
        waterLevel += 20f;
        //Envanterden bir suyu yok et
    }
    //Yem verme butonu
    public void Feed()
    {
        //Envanterde yem taþýyýp taþýmadýðý kontrol edildikten sonra
        foodLevel += 20f;
        //Envanterden bir yemi yok et
    }
    //Yapý içerisine yeni hayvan ekleme butonu
    public void AddAnimal()
    {
        if(animalCount < buildingCapacity /*Karakterin parasý yetiyorsa*/)
        {
            animalCount++;
            //Oyuncunun toplam parasýndan "priceOfAnimal" deðeri kadar para eksiltir.
            switch (buildingType)
            {
                case BuildingType.CowBarn: Instantiate(cowPrefab, animalSpawnPoint);
                    break;
                case BuildingType.SheepBarn: Instantiate(sheepPrefab, animalSpawnPoint);
                    break;
                case BuildingType.HorseBarn: Instantiate(horsePrefab, animalSpawnPoint);
                    break;
                case BuildingType.Coop: Instantiate(chickenPrefab, animalSpawnPoint);
                    break;
            }
        }
    }
    IEnumerator ProductionPeriod()
    {
        productCount += productMultiplier;
        yield return new WaitForSeconds(productionTime);
        StartCoroutine(ProductionPeriod());
    }
    //BURASI PLAYER KODUNA ATILACAK VE BU KOD ÝÇERÝSÝNDEN SÝLÝNECEK
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "RanchBuilding")
        {
            //Arayüzde Interact yazýsý gösterme satýrý
            //Arayüzde istatistikleri gösterme satýrý

            if(Input.GetButtonDown("Interact"))
            {
                col.GetComponent<RanchManager>().optionsPanel.SetActive(true);
            }
        }
    }
    //BURASI PLAYER KODUNA ATILACAK VE BU KOD ÝÇERÝSÝNDEN SÝLÝNECEK
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "RanchBuilding")
        {
            //Arayüzde Interact yazýsý gösterme satýrý
            //Arayüzde istatistikleri gösterme satýrý

            if (Input.GetButtonDown("Interact"))
            {
                col.GetComponent<RanchManager>().optionsPanel.SetActive(true);
            }
        }
    }
    //BURASI PLAYER KODUNA ATILACAK VE BU KOD ÝÇERÝSÝNDEN SÝLÝNECEK
    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "RanchBuilding")
        {
            col.GetComponent<RanchManager>().optionsPanel.SetActive(false);
        }
    }
}
