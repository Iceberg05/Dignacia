using System.Collections;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    public bool isHoed;
    public bool isPlanted;
    public bool isWaterSituationGood;
    public bool isWeatherSituationGood = false;
    public bool isCuttable;

    [Tooltip("Topra��n sahip oldu�u su de�eridir. Optimum su de�erinden az oldu�unda toprak susuz kalm�� olur ve bitkinin yeti�mesini engeller.")]
    public float waterValue;
    [Tooltip("Topra��n susuzlu�unun art�� de�eridir.")]
    [SerializeField] float thirstIncreaseMultiply = 0.1f;
    [Tooltip("Topra��n susuz olmamas� i�in gereken su miktar� de�eridir.")]
    [SerializeField] float optimumWaterValue = 25f;
    [Tooltip("Bitkinin ta��yabilece�i maksimum su miktar�d�r.")]
    [SerializeField] float maxWaterValue = 250f;

    public Plant plant;
    public bool coroutineAlreadyStarted = false;

    AtmosphereManager atmosphereManager;

    Character character;

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
            GetComponent<SpriteRenderer>().sprite = dirtSprites[1];
        } else
        {
            isWaterSituationGood = false;
            GetComponent<SpriteRenderer>().sprite = dirtSprites[0];
        }

        if(isHoed && isPlanted && isWaterSituationGood && isWeatherSituationGood && !coroutineAlreadyStarted)
        {
            StartCoroutine(Grow());
            coroutineAlreadyStarted = true;
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
