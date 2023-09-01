using UnityEngine;
using UnityEngine.UI;

public class TabletScript : MonoBehaviour
{
    AtmosphereManager atmosphereManager;

    int atmosphereValue;
    int temperatureValue;

    [Tooltip("Tablet men�s�n�n UI objesidir.")]
    [SerializeField] GameObject tabletMenu;

    [Tooltip("Atmosfer de�erini g�steren ve Text component�n� i�eren objedir.")]
    [SerializeField] Text atmosphereText;
    [Tooltip("Temperature de�erini g�steren ve Text component�n� i�eren objedir.")]
    [SerializeField] Text temperatureText;
    void Awake()
    {
        atmosphereManager = FindObjectOfType<AtmosphereManager>();
    }
    void Update()
    {
        //De�erleri atmosphereManager objesinden �eker
        atmosphereValue = atmosphereManager.atmosphereValue;
        temperatureValue = atmosphereManager.temperatureValue;

        //Textlere de�erleri yazd�r�r
        atmosphereText.text = atmosphereValue.ToString();
        temperatureText.text = temperatureValue.ToString();

        //Tableti kapat�p a�ar
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
