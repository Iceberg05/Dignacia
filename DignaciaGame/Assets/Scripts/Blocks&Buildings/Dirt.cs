using UnityEngine;

public class Dirt : MonoBehaviour
{
    [Tooltip("Bitkinin ekilip ekilmedi�ini kontrol eder.")]
    public bool isPlanted;
    [Tooltip("�apalan�p �abalanmad���n� kontrol eder.")]
    public bool isHoed;
    [Tooltip("Bitkinin sulan�p sulanmad���n� kontrol eder.")]
    public bool isHydraded;
    [Tooltip("Bitkinin bi�ilip bi�ilemeyece�ini kontrol eder.")]
    public bool isCuttable;
    [Tooltip("Hava durumunun bitki i�in ideal olup olmad���n� kontrol eder.")]
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
