using System.Collections;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    [Tooltip("Topra��n �apalan�p �apalanmad���n�n de�eridir.")] public bool isHoed;
    [Tooltip("Topra�a bitkinin ekilip ekilmedi�inin de�eridir.")] public bool isPlanted;
    [Tooltip("Topra��n g��lendirilip g��lendirilmedi�i kontrol edilir.")] public bool isReinforced;
    [Tooltip("Bitkinin b�y�mesi i�in su �artlar�n�n sa�lan�p sa�lanmad���n� kontrol eder.")] public bool isWaterSituationGood;
    [Tooltip("Topra�a otomatik sulama sistemi yerle�tirilip yerle�tirilmedi�ini kontrol eder.")] public bool isWaterAuto;
    [Tooltip("Bitkinin b�y�mesi i�in optimum hava �artlar�n�n sa�lan�p sa�lanmad���n� kontrol eder.")] public bool isWeatherSituationGood = false;
    [Tooltip("Bitkinin bi�ilip bi�ilemeyece�ini kontrol eder.")] public bool isCuttable;

    [Tooltip("Topra��n sahip oldu�u su de�eridir. Optimum su de�erinden az oldu�unda toprak susuz kalm�� olur ve bitkinin yeti�mesini engeller.")] public float waterValue;
    [Tooltip("Topra��n susuzlu�unun art�� de�eridir.")]
    [SerializeField] float thirstIncreaseMultiply = 0.1f;
    [Tooltip("Topra��n susuz olmamas� i�in gereken su miktar� de�eridir.")]
    [SerializeField] float optimumWaterValue = 25f;
    [Tooltip("Bitkinin ta��yabilece�i maksimum su miktar�d�r.")]
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
        character.currentModeNumber = 1; //Prostetik kolun sadece tar�m modundayken ekim yap�lmas�n� kontrol eder

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
