using System.Collections;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    [Tooltip("Toprağın çapalanıp çapalanmadığının değeridir.")] public bool isHoed;
    [Tooltip("Toprağa bitkinin ekilip ekilmediğinin değeridir.")] public bool isPlanted;
    [Tooltip("Toprağın güçlendirilip güçlendirilmediği kontrol edilir.")] public bool isReinforced;
    [Tooltip("Bitkinin büyümesi için su şartlarının sağlanıp sağlanmadığını kontrol eder.")] public bool isWaterSituationGood;
    [Tooltip("Toprağa otomatik sulama sistemi yerleştirilip yerleştirilmediğini kontrol eder.")] public bool isWaterAuto;
    [Tooltip("Bitkinin büyümesi için optimum hava şartlarının sağlanıp sağlanmadığını kontrol eder.")] public bool isWeatherSituationGood = false;
    [Tooltip("Bitkinin biçilip biçilemeyeceğini kontrol eder.")] public bool isCuttable;

    [Tooltip("Toprağın sahip olduğu su değeridir. Optimum su değerinden az olduğunda toprak susuz kalmış olur ve bitkinin yetişmesini engeller.")] public float waterValue;
    [Tooltip("Toprağın susuzluğunun artış değeridir.")]
    [SerializeField] float thirstIncreaseMultiply = 0.1f;
    [Tooltip("Toprağın susuz olmaması için gereken su miktarı değeridir.")]
    [SerializeField] float optimumWaterValue = 25f;
    [Tooltip("Bitkinin taşıyabileceği maksimum su miktarıdır.")]
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
        character.currentModeNumber = 1; //Prostetik kolun sadece tarım modundayken ekim yapılmasını kontrol eder

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
