using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AtmosphereManager : MonoBehaviour
{
    [Tooltip("Atmosfer de�erinin butonudur.")]
    [SerializeField] Button atmosphereBtn;
    [Tooltip("Temperature de�erinin butonudur.")]
    [SerializeField] Button temperatureBtn;

    [Tooltip("Atmosfer men�s�n�n UI objesidir.")]
    [SerializeField] GameObject atmospherePanel;

    [Tooltip("S�cakl���n de�eridir.")]
    public int temperatureValue;
    [Tooltip("Atmosferin de�eridir.")]
    public int atmosphereValue;

    [SerializeField] Text atmosphereText;
    [SerializeField] Text temperatureText;
    void Start()
    {
        atmosphereText.text = atmosphereValue.ToString();
        temperatureText.text = temperatureValue.ToString();
        atmosphereValue = 100;
        temperatureValue = 100;

        atmosphereBtn = GetComponent<Button>();
        temperatureBtn = GetComponent<Button>();
    }
    void Update()
    {
        atmosphereText.text = atmosphereValue.ToString();
        temperatureText.text = temperatureValue.ToString();
    }
    IEnumerator AtmosphereNumerator()
    {
        if (atmosphereValue != 10)
        {
            atmosphereValue = atmosphereValue - 10;
            yield return new WaitForSeconds(2);
        }
        StartCoroutine(AtmosphereNumerator());
    }
    public void Atmosphere()
    {
        StartCoroutine(AtmosphereNumerator());
        Destroy(atmosphereBtn);
    }
    IEnumerator TempratureNumerator()
    {
        if (temperatureValue != 30 && temperatureValue < 30)
        {
            temperatureValue = temperatureValue + 10;
            yield return new WaitForSeconds(2);
        }
        if (temperatureValue != 30 && temperatureValue > 30)
        {
            temperatureValue = temperatureValue - 10;
            yield return new WaitForSeconds(2);
        }
        StartCoroutine(TempratureNumerator());
    }
    public void Temperature()
    {
        StartCoroutine(TempratureNumerator());
        Destroy(temperatureBtn);
    }

}