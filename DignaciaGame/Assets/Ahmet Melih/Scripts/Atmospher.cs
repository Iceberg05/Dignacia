using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Atmospher : MonoBehaviour
{
    public GameObject atmosButton;
    public GameObject tempButton;

    public GameObject Panel;
    public int Temperature;
    public int Atmosphere;
    public Text Atmostext;
    public Text Temptext;
    void Start()
    {
        Atmostext.text = Atmosphere.ToString();
        Temptext.text = Temperature.ToString();
        Atmosphere = 100;
        Temperature = 100;

        atmosButton.GetComponent<Button>();
        tempButton.GetComponent<Button>();
    }
    
    void Update()
    {
        Atmostext.text = Atmosphere.ToString();
        Temptext.text = Temperature.ToString();
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
        if (Atmosphere != 10)
        {
            Atmosphere = Atmosphere - 10;
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
        if (Temperature != 30 && Temperature < 30)
        {
            Temperature = Temperature + 10;
            yield return new WaitForSeconds(2);
        }
        if (Temperature != 30 && Temperature > 30)
        {
            Temperature = Temperature - 10;
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