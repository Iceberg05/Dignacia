using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TabletScript : MonoBehaviour
{
    AtmosphereManager atmosphereManager;

    int atmosphereValue;
    int temperatureValue;

    [Tooltip("Tablet menüsünün UI objesidir.")]
    [SerializeField] GameObject tabletMenu;

    [Tooltip("Atmosfer deðerini gösteren ve Text componentýný içeren objedir.")]
    [SerializeField] Text atmosphereText;
    [Tooltip("Temperature deðerini gösteren ve Text componentýný içeren objedir.")]
    [SerializeField] Text temperatureText;

    [Header("Chips")]
    [Space]

    [SerializeField] Button atmosphereBtn;
    [SerializeField] Button restourantBtn;
    [SerializeField] Button mapSensorBtn;
    [SerializeField] Button upgradeManagerBtn;
    [SerializeField] Button populationAndTourismBtn;
    [SerializeField] Button dungeonGuideBtn;
    void Awake()
    {
        atmosphereManager = FindObjectOfType<AtmosphereManager>();
    }
    void Update()
    {
        //Deðerleri atmosphereManager objesinden çeker
        atmosphereValue = atmosphereManager.atmosphereValue;
        temperatureValue = atmosphereManager.temperatureValue;

        //Textlere deðerleri yazdýrýr
        atmosphereText.text = atmosphereValue.ToString();
        temperatureText.text = temperatureValue.ToString();

        //Tableti kapatýp açar
        if (Input.GetButtonDown("Device") && !tabletMenu.activeSelf) tabletMenu.SetActive(true);
        else if(Input.GetButtonDown("Device") && tabletMenu.activeSelf) tabletMenu.SetActive(false);
        else if (Input.GetKeyDown(KeyCode.Escape)) tabletMenu.SetActive(false);

        if (PlayerPrefs.GetInt("AtmosphereChip") == 1) atmosphereBtn.interactable = true;
        if (PlayerPrefs.GetInt("RestourantChip") == 1) restourantBtn.interactable = true;
        if (PlayerPrefs.GetInt("MapSensorChip") == 1) mapSensorBtn.interactable = true;
        if (PlayerPrefs.GetInt("UpgradeManagerChip") == 1) upgradeManagerBtn.interactable = true;
        if (PlayerPrefs.GetInt("PopulationAndTourismChip") == 1) populationAndTourismBtn.interactable = true;
        if (PlayerPrefs.GetInt("DungeonGuideChip") == 1) dungeonGuideBtn.interactable = true;
    }
}
