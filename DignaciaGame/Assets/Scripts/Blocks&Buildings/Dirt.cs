using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    public bool isHoed;
    public bool isPlanted;
    public bool isHydraded;
    public bool isCuttable;
    public bool isWeatherStuationgood;

    [SerializeField] GameObject plant;

    void OnMouseEnter()
    {
        if(isPlanted)
        {
            GetComponent<Renderer>().material.color = Color.red;
        } else
        {
            GetComponent<Renderer>().material.color = Color.green;
        }
    }
    private void OnMouseOver()
    {
        if(Input.GetMouseButton(0) && !isPlanted)
        {
            plant.SetActive(true);
            isPlanted = true;
        }
    }
    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}
