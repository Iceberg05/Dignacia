using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Atmosphere_Script : MonoBehaviour
{
    public GameObject atmosButton;
    public GameObject tempButton;

    public GameObject Panel;
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
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Panel.SetActive(true);
        }
        else
        {
            Panel.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Panel.SetActive(false);
        }

    }
    IEnumerator atmosp()
    {
        if (AtmosphereValue != 10)
        {
            AtmosphereValue = AtmosphereValue - 10;
            yield return new WaitForSeconds(2);
        }
        StartCoroutine(atmosp());

    }
    public void Atmospheree()
    {

        StartCoroutine(atmosp());
        Destroy(atmosButton.GetComponent<Button>());
    }
    IEnumerator tempp()
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
        StartCoroutine(tempp());

    }
    public void temp()
    {
        StartCoroutine(tempp());
        Destroy(tempButton.GetComponent<Button>());
    }

}