using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Atmosphere_Script : MonoBehaviour
{
    public GameObject atmosButton;
    public GameObject tempButton;

    public GameObject AtmospherePanel;
    public int TemperatureValue;
    public int AtmosphereValue;
    public Text Atmostext;
    public Text Temptext;
    void Start()
    {
        Atmostext.text = AtmosphereValue.ToString();
        Temptext.text = TemperatureValue.ToString();
        AtmosphereValue = 100;
        TemperatureValue = 100;

        atmosButton.GetComponent<Button>();
        tempButton.GetComponent<Button>();
    }
    void Update()
    {
        Atmostext.text = AtmosphereValue.ToString();
        Temptext.text = TemperatureValue.ToString();
    }
   
    IEnumerator atmosphereNumerator()
    {
        if (AtmosphereValue != 10)
        {
            AtmosphereValue = AtmosphereValue - 10;
            yield return new WaitForSeconds(2);
        }
        StartCoroutine(atmosphereNumerator());

    }
    public void Atmosphere()
    {

        StartCoroutine(atmosphereNumerator());
        Destroy(atmosButton.GetComponent<Button>());
    }
    IEnumerator tempratureNumerator()
    {
        if (TemperatureValue != 30 && TemperatureValue < 30)
        {
            TemperatureValue = TemperatureValue + 10;
            yield return new WaitForSeconds(2);
        }
        if (TemperatureValue != 30 && TemperatureValue > 30)
        {
            TemperatureValue = TemperatureValue - 10;
            yield return new WaitForSeconds(2);
        }
        StartCoroutine(tempratureNumerator());

    }
    public void temperature()
    {
        StartCoroutine(tempratureNumerator());
        Destroy(tempButton.GetComponent<Button>());
    }

}