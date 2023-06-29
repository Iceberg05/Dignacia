using System.Collections;
using UnityEngine;

public class RanchAnimal : MonoBehaviour
{
    [Header("Main")]
    [Space]

    [Tooltip("Bir hayvan�n fiyat�d�r.")]
    public float priceOfAnimal;
    [Tooltip("Yap�n�n seviyesidir.")]
    [SerializeField] int buildingLevel;
    enum AnimalType { Cow, Sheep, Goat, Horse, Chicken, Goose };
    [Tooltip("Hayvan�n hangi t�r oldu�unu belirtir. Bu de�i�kene g�re, di�er istatistikler de de�i�ir.")]
    [SerializeField] AnimalType animalType;

    public RanchManager ranch;

    [Header("Products")]
    [Space]

    [Tooltip("Tek bir �retimde ��kan �r�n miktar�d�r.")]
    public int productMultiplier = 1;

    [Tooltip("Mevcut �retim s�residir.")]
    [SerializeField] float currentProductionTime = 10f;
    [Tooltip("Uygun �artlardaki �retim s�residir.")]
    [SerializeField] float defaultProductionTime = 10f;
    [Tooltip("Zor �artlardaki �retim s�residir.")]
    [SerializeField] float extremeSituationProductionTime = 60f;

    bool productionStarted = false;

    [Header("Animal Stats")]
    [Space]

    [Tooltip("A�l���n/susuzlu�un saniye ba��na artma miktar�d�r.")]
    public float hungerIncreaseRate;
    public float thirstIncreaseRate;
    [Tooltip("Susuzluk miktar�d�r. Ne kadar az olursa, �retim o kadar �ok olur. Bu de�er artt�k�a �retim bitmeye ba�lar.")]
    [SerializeField] float thirstLevel = 0f;
    [Tooltip("A�l�k miktar�d�r. Ne kadar az olursa, �retim o kadar �ok olur. Bu de�er artt�k�a �retim bitmeye ba�lar.")]
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
