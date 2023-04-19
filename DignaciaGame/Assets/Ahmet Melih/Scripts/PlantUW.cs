using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantUW : MonoBehaviour
{
    public List<GameObject> Plants;

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlantUW1")
        {
            collision.gameObject.transform.parent.GetChild(0).gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "PlantUW2")
        {
            collision.gameObject.transform.parent.GetChild(0).gameObject.SetActive(false);
            collision.gameObject.transform.parent.GetChild(1).gameObject.SetActive(false);
        }
        else if (collision.gameObject.tag == "PlantUW3")
        {
            collision.gameObject.transform.parent.GetChild(0).gameObject.SetActive(false);
            collision.gameObject.transform.parent.GetChild(1).gameObject.SetActive(false);
            collision.gameObject.transform.parent.GetChild(2).gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        StartCoroutine(Grow2());
    }
    private IEnumerator Grow2()
    {
        for (int i = 0; i < Plants.Count; i++)
        {
            if (!Plants[i].transform.GetChild(0).gameObject.activeSelf &&
                !Plants[i].transform.GetChild(1).gameObject.activeSelf &&
                 Plants[i].transform.GetChild(2).gameObject.activeSelf)
            {

                yield return new WaitForSeconds(3);
                if (Plants[i].transform.GetChild(2).gameObject.activeSelf)
                {
                    Plants[i].transform.GetChild(1).gameObject.SetActive(true);
                }

            }

        }
        for (int i = 0; i < Plants.Count; i++)
        {
            if (!Plants[i].transform.GetChild(0).gameObject.activeSelf &&
                       Plants[i].transform.GetChild(1).gameObject.activeSelf &&
                       Plants[i].transform.GetChild(2).gameObject.activeSelf)
            {
                yield return new WaitForSeconds(3);
                if (Plants[i].transform.GetChild(1).gameObject.activeSelf &&
                    Plants[i].transform.GetChild(2).gameObject.activeSelf)
                {
                    Plants[i].transform.GetChild(0).gameObject.SetActive(true);
                }


            }
        }   
        
            
                    
        
    }
}


