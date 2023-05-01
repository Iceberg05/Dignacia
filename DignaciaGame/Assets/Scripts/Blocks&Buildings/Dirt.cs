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

    void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = Color.red;
    }
    void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }
}
