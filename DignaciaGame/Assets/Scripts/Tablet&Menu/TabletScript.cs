using UnityEngine;
using UnityEngine.UI;

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
        if (Input.GetKeyDown(KeyCode.T) && !tabletMenu.activeSelf)
        {
            tabletMenu.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            tabletMenu.SetActive(false);
        }
    }
}
