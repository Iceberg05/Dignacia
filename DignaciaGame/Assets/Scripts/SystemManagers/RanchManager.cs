using System.Collections;
using UnityEngine;

public class RanchManager : MonoBehaviour
{
    [Header("Building Variables")]

    [Tooltip("Yap� i�erisinde ka� hayvan bulundurulabilece�ini belirten de�erdir.")]
    public int buildingCapacity;
    [Tooltip("�iftlikte bulunan hayvan miktar�d�r.")]
    [SerializeField] int animalCount;
    enum BuildingType { CowBarn, SheepBarn, HorseBarn, Coop };
    [Tooltip("Yap� ah�r olup inek ah�r�ysa bu de�eri 'CowBarn', koyun/ke�i/ko� ah�r�ysa 'SheepBarn', at ah�r�ysa 'HorseBarn', yap� direkt k�mes ise 'Coop' olarak i�aretleyin.")]
    [SerializeField] BuildingType buildingType;


    [Header("Ranch Stats")]

    [Tooltip("Susuzluk miktar�d�r. Ne kadar az olursa, �retim o kadar �ok olur. Bu de�er artt�k�a �retim bitmeye ba�lar.")]
    [SerializeField] float thirstLevel = 0f;
    [Tooltip("A�l�k miktar�d�r. Ne kadar az olursa, �retim o kadar �ok olur. Bu de�er artt�k�a �retim bitmeye ba�lar.")]
    [SerializeField] float hungerLevel = 0f;
    [Tooltip("Su ve yem miktar�d�r. Ne kadar �ok olursa, hayvanlar�n susuzlu�u ve a�l��� o kadar az olur.")]
    [SerializeField] float waterLevel = 100f, foodLevel = 100f;
    [Tooltip("A�l���n/susuzlu�un saniye ba��na artma miktar�d�r.")]
    [SerializeField] float hungerIncreaseRate = 0.1f, thirstIncreaseRate = 0.2f;
    [Tooltip("Yeme�in/suyun saniye ba��na azalma miktar�d�r.")]
    [SerializeField] float waterDecreaseRate = 0.1f, foodDecreaseRate = 0.1f; 

    [Header("Products")]

    public int productCount;
    [Tooltip("Tek bir �retimde ��kan �r�n miktar�d�r.")]
    public int productMultiplier = 1;
    [Tooltip("�retim s�residir.")]
    [SerializeField] float productionTime = 10f;

    bool productionStarted = false;

    [Header("Buy Animal")]

    public float priceOfAnimal;
    [Tooltip("Spawn edilecek hayvanlar�n prefableridir.")]
    [SerializeField] GameObject cowPrefab, sheepPrefab, horsePrefab, chickenPrefab;
    [Tooltip("Hayvan sat�n al�nd���nda spawn olaca�� noktad�r.")]
    [SerializeField] Transform animalSpawnPoint;

    [Header("User Interface Part")]

    [Tooltip("Yap� ile etkile�ime girildi�inde ��kan se�enek panelidir.")]
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
    //�r�nleri toplama butonu
    public void CollectProducts()
    {
        switch(buildingType)
        {
            case BuildingType.CowBarn: //Envanter kodu ile birle�tirilecek
                break;
            case BuildingType.SheepBarn: //Envanter kodu ile birle�tirilecek
                break;
            case BuildingType.Coop: //Envanter kodu ile birle�tirilecek
                break;
        }
    }
    //Su verme butonu
    public void FillWater()
    {
        //Envanterde su ta��y�p ta��mad��� kontrol edildikten sonra
        waterLevel += 20f;
        //Envanterden bir suyu yok et
    }
    //Yem verme butonu
    public void Feed()
    {
        //Envanterde yem ta��y�p ta��mad��� kontrol edildikten sonra
        foodLevel += 20f;
        //Envanterden bir yemi yok et
    }
    //Yap� i�erisine yeni hayvan ekleme butonu
    public void AddAnimal()
    {
        if(animalCount < buildingCapacity /*Karakterin paras� yetiyorsa*/)
        {
            animalCount++;
            //Oyuncunun toplam paras�ndan "priceOfAnimal" de�eri kadar para eksiltir.
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
    //BURASI PLAYER KODUNA ATILACAK VE BU KOD ��ER�S�NDEN S�L�NECEK
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "RanchBuilding")
        {
            //Aray�zde Interact yaz�s� g�sterme sat�r�
            //Aray�zde istatistikleri g�sterme sat�r�

            if(Input.GetButtonDown("Interact"))
            {
                col.GetComponent<RanchManager>().optionsPanel.SetActive(true);
            }
        }
    }
    //BURASI PLAYER KODUNA ATILACAK VE BU KOD ��ER�S�NDEN S�L�NECEK
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "RanchBuilding")
        {
            //Aray�zde Interact yaz�s� g�sterme sat�r�
            //Aray�zde istatistikleri g�sterme sat�r�

            if (Input.GetButtonDown("Interact"))
            {
                col.GetComponent<RanchManager>().optionsPanel.SetActive(true);
            }
        }
    }
    //BURASI PLAYER KODUNA ATILACAK VE BU KOD ��ER�S�NDEN S�L�NECEK
    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.tag == "RanchBuilding")
        {
            col.GetComponent<RanchManager>().optionsPanel.SetActive(false);
        }
    }
}
