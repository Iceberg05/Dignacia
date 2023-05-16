using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TabletScript : MonoBehaviour
{
    public Atmosphere_Script atmos;
    public int Atmosphere;
    public int Temperature;
    public GameObject tabletMenu;
    public GameObject buyMenu;
    public GameObject sellMenu;
    public GameObject atmosMenu;
    public Text Atmostext;
    public Text Temptext;
    void Start()
    {
        Atmosphere = atmos.AtmosphereValue;
        Temperature = atmos.TemperatureValue;
        Atmostext.text = Atmosphere.ToString();
        Temptext.text = Temperature.ToString();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && !tabletMenu.activeSelf)
        {
            tabletMenu.SetActive(true);

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            tabletMenu.SetActive(false);



        }

    }




    public void buyButton()
    {
        tabletMenu.SetActive(false);
        buyMenu.SetActive(true);
    }

    public void sellButton()
    {
        tabletMenu.SetActive(false);
        sellMenu.SetActive(true);
    }

    public void atmosButton()
    {
        tabletMenu.SetActive(false);
        atmosMenu.SetActive(true);
    }

    public void atmosBackButton()
    {
        tabletMenu.SetActive(true);
        atmosMenu.SetActive(false);
    }

    public void buyBackButton()
    {
        tabletMenu.SetActive(true);
        buyMenu.SetActive(false);
    }

}
