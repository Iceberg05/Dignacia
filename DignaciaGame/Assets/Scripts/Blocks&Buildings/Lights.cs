using UnityEngine;
using UnityEngine.UI;

public class Lights : MonoBehaviour
{
    public Image imageToSample;
    public SpriteRenderer spriteToChange;
   // public GameObject ColorSystem;
   // public GameObject ColorButton;
    private GameObject[] childObjects;
    private void Start()
    {
        childObjects = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            childObjects[i] = transform.GetChild(i).gameObject;
        }
        childObjects[1].SetActive(false);

    }
    private void Update()
    {
        // Image'in rengini alarak Sprite'ýn rengine atar
        spriteToChange.color = imageToSample.color;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            childObjects[1].SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            childObjects[1].SetActive(false);
            childObjects[0].SetActive(false);
}
    }


}