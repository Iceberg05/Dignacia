using System.Collections;
using UnityEngine;

public class RanchAnimal : MonoBehaviour
{
    [Header("Main")]
    [Space]

    [Tooltip("Bir hayvanýn fiyatýdýr.")]
    public float priceOfAnimal;
    [Tooltip("Yapýnýn seviyesidir.")]
    [SerializeField] int buildingLevel;
    enum AnimalType { Cow, Sheep, Goat, Horse, Chicken, Goose };
    [Tooltip("Hayvanýn hangi tür olduðunu belirtir. Bu deðiþkene göre, diðer istatistikler de deðiþir.")]
    [SerializeField] AnimalType animalType;

    public RanchManager ranch;

    [Header("Products")]
    [Space]

    [Tooltip("Tek bir üretimde çýkan ürün miktarýdýr.")]
    public int productMultiplier = 1;

    [Tooltip("Mevcut üretim süresidir.")]
    [SerializeField] float currentProductionTime = 10f;
    [Tooltip("Uygun þartlardaki üretim süresidir.")]
    [SerializeField] float defaultProductionTime = 10f;
    [Tooltip("Zor þartlardaki üretim süresidir.")]
    [SerializeField] float extremeSituationProductionTime = 60f;

    bool productionStarted = false;

    [Header("Animal Stats")]
    [Space]

    [Tooltip("Açlýðýn/susuzluðun saniye baþýna artma miktarýdýr.")]
    public float hungerIncreaseRate;
    public float thirstIncreaseRate;
    [Tooltip("Susuzluk miktarýdýr. Ne kadar az olursa, üretim o kadar çok olur. Bu deðer arttýkça üretim bitmeye baþlar.")]
    [SerializeField] float thirstLevel = 0f;
    [Tooltip("Açlýk miktarýdýr. Ne kadar az olursa, üretim o kadar çok olur. Bu deðer arttýkça üretim bitmeye baþlar.")]
    [SerializeField] float hungerLevel = 0f;

    bool isSick;
    [SerializeField] float deathTime;
    void Start()
    {
        ranch.waterDecreaseRate += thirstIncreaseRate;
        ranch.foodDecreaseRate += hungerIncreaseRate;
    }
    void Update()
    {
        hungerLevel = Mathf.Clamp(hungerLevel, 0, 100f);
        thirstLevel = Mathf.Clamp(thirstLevel, 0, 100f);

        if (ranch.waterLevel <= 10) thirstLevel += thirstIncreaseRate * Time.deltaTime;
        else thirstLevel -= 1f * Time.deltaTime;

        if (ranch.foodLevel <= 10) hungerLevel += hungerIncreaseRate * Time.deltaTime;
        else hungerLevel -= 1f * Time.deltaTime;

        if (hungerLevel >= 90 || thirstLevel >= 90)
        {
            StopCoroutine(ProductionPeriod());
            isSick = true;
            productionStarted = false;
        }
        else if((hungerLevel < 90 && hungerLevel >= 60) || (thirstLevel < 90 && thirstLevel >= 60))
        {
            if(!productionStarted)
            {
                currentProductionTime = extremeSituationProductionTime;
                StartCoroutine(ProductionPeriod());
                productionStarted = true;
            }
        }
        else if(hungerLevel < 60 && thirstLevel < 60)
        {
            if (!productionStarted)
            {
                currentProductionTime = defaultProductionTime;
                isSick = false;
                StartCoroutine(ProductionPeriod());
                productionStarted = true;
            }
        }
        if(isSick) StartCoroutine(Dying());
        else StopCoroutine(Dying());
    }
    IEnumerator ProductionPeriod()
    {
        ranch.productCount += productMultiplier;
        yield return new WaitForSeconds(currentProductionTime);
        StartCoroutine(ProductionPeriod());
    }
    IEnumerator Dying()
    {
        yield return new WaitForSeconds(deathTime);

        ranch.animalsInRanch.Remove(this);
        ranch.waterDecreaseRate -= thirstIncreaseRate;
        ranch.foodDecreaseRate -= hungerIncreaseRate;
        Destroy(gameObject);
    }
}
