using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AtmosphereManager : MonoBehaviour
{
    [Tooltip("Atmosfer men�s�n�n UI objesidir.")]
    [SerializeField] GameObject atmospherePanel;

    [Tooltip("S�cakl���n de�eridir.")]
    public int temperatureValue;
    [Tooltip("S�cakl���n maksimum de�eridir.")]
    public int temperatureMaxValue = 100;
    [Tooltip("S�cakl�k de�erinin ne kadar h�zl� artt���n�n de�eridir.")]
    public int temperatureIncreaseValue;

    [Tooltip("Atmosferin de�eridir.")]
    public int atmosphereValue;
    [Tooltip("Atmosferin maksimum de�eridir.")]
    public int atmosphereMaxValue = 100;
    [Tooltip("Atmosfer de�erinin ne kadar h�zl� artt���n�n de�eridir.")]
    public int atmosphereIncreaseValue;

    [SerializeField] Text atmosphereText;
    [SerializeField] Text temperatureText;

    bool isAtmosphereMachineOpen;
    void Start()
    {
        atmosphereValue = PlayerPrefs.GetInt("Atmosphere Value");
        temperatureValue = PlayerPrefs.GetInt("Temperature Value");
    }
    void Update()
    {
        atmosphereText.text = atmosphereValue.ToString();
        temperatureText.text = temperatureValue.ToString();
        temperatureValue = Mathf.Clamp(temperatureValue, 0, temperatureMaxValue);
        atmosphereValue = Mathf.Clamp(atmosphereValue, 0, atmosphereMaxValue);

        #region SAVE_VALUES

        PlayerPrefs.SetInt("Atmosphere Value", atmosphereValue);
        PlayerPrefs.SetInt("Temperature Value", temperatureValue);

        #endregion

        if (isAtmosphereMachineOpen)
        {
            atmospherePanel.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                isAtmosphereMachineOpen = false;
            }
        } else
        {
            atmospherePanel.SetActive(false);
        }
    }
    IEnumerator AtmosphereNumerator()
    {
        atmosphereValue += atmosphereIncreaseValue;
        yield return new WaitForSeconds(2);

        if (atmosphereValue < atmosphereMaxValue)
        {
            StartCoroutine(AtmosphereNumerator());
        }
        else
        {
            StopCoroutine(AtmosphereNumerator());
        }
    }
    public void Atmosphere()
    {
        StartCoroutine(AtmosphereNumerator());
    }
    IEnumerator TempratureNumerator()
    {
        temperatureValue += temperatureIncreaseValue;
        yield return new WaitForSeconds(2);

        if (temperatureValue < temperatureMaxValue)
        {
            StartCoroutine(TempratureNumerator());
        } else
        {
            StopCoroutine(TempratureNumerator());
        }
    }
    public void Temperature()
    {
        StartCoroutine(TempratureNumerator());
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player" && !isAtmosphereMachineOpen)
        {
            //�nteraksiyon yaz�s� ��kar�lacak
            if(Input.GetButtonDown("Interact"))
            {
                isAtmosphereMachineOpen = true;
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && !isAtmosphereMachineOpen)
        {
            //�nteraksiyon yaz�s� ��kar�lacak
            if (Input.GetButtonDown("Interact"))
            {
                isAtmosphereMachineOpen = true;
            }
        }
    }

}