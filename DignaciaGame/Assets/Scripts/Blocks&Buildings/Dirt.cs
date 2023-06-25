using UnityEngine;

public class Dirt : MonoBehaviour
{
    [Tooltip("Bitkinin ekilip ekilmediğini kontrol eder.")]
    public bool isPlanted;
    [Tooltip("Çapalanıp çabalanmadığını kontrol eder.")]
    public bool isHoed;
    [Tooltip("Bitkinin sulanıp sulanmadığını kontrol eder.")]
    public bool isHydraded;
    [Tooltip("Bitkinin biçilip biçilemeyeceğini kontrol eder.")]
    public bool isCuttable;
    [Tooltip("Hava durumunun bitki için ideal olup olmadığını kontrol eder.")]
    public bool isWeatherStuationgood;

    [Tooltip("Bitki objesidir.")]
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
    void OnMouseOver()
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
