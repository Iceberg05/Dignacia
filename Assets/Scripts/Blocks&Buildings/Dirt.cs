using System.Collections;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    [Tooltip("Topraðýn çapalanýp çapalanmadýðýnýn deðeridir.")] public bool isHoed;
    [Tooltip("Topraða bitkinin ekilip ekilmediðinin deðeridir.")] public bool isPlanted;
    [Tooltip("Topraðýn güçlendirilip güçlendirilmediði kontrol edilir.")] public bool isReinforced;
    [Tooltip("Bitkinin büyümesi için su þartlarýnýn saðlanýp saðlanmadýðýný kontrol eder.")] public bool isWaterSituationGood;
    [Tooltip("Topraða otomatik sulama sistemi yerleþtirilip yerleþtirilmediðini kontrol eder.")] public bool isWaterAuto;
    [Tooltip("Bitkinin büyümesi için optimum hava þartlarýnýn saðlanýp saðlanmadýðýný kontrol eder.")] public bool isWeatherSituationGood = false;
    [Tooltip("Bitkinin biçilip biçilemeyeceðini kontrol eder.")] public bool isCuttable;

    [Tooltip("Topraðýn sahip olduðu su deðeridir. Optimum su deðerinden az olduðunda toprak susuz kalmýþ olur ve bitkinin yetiþmesini engeller.")] public float waterValue;
    [Tooltip("Topraðýn susuzluðunun artýþ deðeridir.")]
    [SerializeField] float thirstIncreaseMultiply = 0.1f;
    [Tooltip("Topraðýn susuz olmamasý için gereken su miktarý deðeridir.")]
    [SerializeField] float optimumWaterValue = 25f;
    [Tooltip("Bitkinin taþýyabileceði maksimum su miktarýdýr.")]
    [SerializeField] float maxWaterValue = 250f;

    AtmosphereManager atmosphereManager;
    Character character;
    public Plant plant;
    public bool coroutineAlreadyStarted = false;

    bool isAlreadyReinforced;

    [SerializeField] Sprite[] dirtSprites;
    void Start()
    {
        character = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
        character.currentModeNumber = 1; //Prostetik kolun sadece tarým modundayken ekim yapýlmasýný kontrol eder

        atmosphereManager = FindObjectOfType<AtmosphereManager>();
    }
    void Update()
    {
        if((plant.optimumAtmosphereValue <= atmosphereManager.atmosphereValue) && (plant.optimumTemperatureValue <= atmosphereManager.temperatureValue))
        {
            isWeatherSituationGood = true;
        }

        waterValue -= thirstIncreaseMultiply * Time.deltaTime;
        if(waterValue >= optimumWaterValue && waterValue <= maxWaterValue)
        {
            isWaterSituationGood = true;
            GetComponent<SpriteRenderer>().sprite = dirtSprites[2];
        } else
        {
            isWaterSituationGood = false;
            GetComponent<SpriteRenderer>().sprite = dirtSprites[1];
        }

        if(isHoed && isPlanted && isWaterSituationGood && isWeatherSituationGood && !coroutineAlreadyStarted)
        {
            StartCoroutine(Grow());
            coroutineAlreadyStarted = true;
        }

        if(isReinforced)
        {
            if(!isAlreadyReinforced)
            {
                plant.changePhaseTime -= plant.changePhaseTime * 0.25f;
                isAlreadyReinforced = true;
            }
            GetComponent<SpriteRenderer>().sprite = dirtSprites[3];
            isReinforced = true;
        }
        if(!isHoed) GetComponent<SpriteRenderer>().sprite = dirtSprites[0];

        if(isWaterAuto)
        {
            waterValue = optimumWaterValue;
            thirstIncreaseMultiply = 0f;
        }
    }
    void OnMouseEnter()
    {
        if (isPlanted)
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
    }
    void OnMouseOver()
    {
        /*if (isWeatherSituationGood && Input.GetMouseButton(0) && character.currentModeNumber == 1)
        {
            plant.gameObject.SetActive(true);
            isPlanted = true;
        }
        if (isPlanted == false && Input.GetMouseButton(1) && isHoed == true)
        {
            isHoed = false;
        }*/
    }
    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    public IEnumerator Grow()
    {
        yield return new WaitForSeconds(plant.changePhaseTime);
        plant.GetComponent<SpriteRenderer>().sprite = plant.phaseSprites[1];

        yield return new WaitForSeconds(plant.changePhaseTime);
        plant.GetComponent<SpriteRenderer>().sprite = plant.phaseSprites[2];

        yield return new WaitForSeconds(plant.changePhaseTime);
        plant.GetComponent<SpriteRenderer>().sprite = plant.phaseSprites[3];

        yield return new WaitForSeconds(plant.changePhaseTime);
        plant.GetComponent<SpriteRenderer>().sprite = plant.phaseSprites[4];

        isCuttable = true;
    }
}
