using System.Collections;
using UnityEngine;

public class GreenHouse : MonoBehaviour
{
    public GameObject atm;
    RaycastHit2D hitty;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (atm.GetComponent<Atmosphere>().AtmosphereValue == 10 && atm.GetComponent<Atmosphere>().TemperatureValue == 30)
            {
               DoIt2();
            }
        }
    }
    public void DoIt2()
    {
        hitty = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hitty.collider != null)
        {
            //Debug.Log("Target Position: " + hit.collider.gameObject.transform.position
            //    + " AND TARGET NAME: " + hit.collider.gameObject.name);
            if (hitty.collider.gameObject.tag == "Dirt") //ve elinde çapa varsa...
            {
                if (!hitty.collider.gameObject.GetComponent<Dirt>().isHoed)
                {
                    hitty.collider.gameObject.GetComponent<Dirt>().isHoed = true;
                    //çapalandý 
                }
                else if (!hitty.collider.gameObject.GetComponent<Dirt>().isPlanted)
                {
                    hitty.collider.gameObject.GetComponent<Dirt>().isPlanted = true;
                    //ekildi ilk ekili hali olacak
                }
                else if (!hitty.collider.gameObject.GetComponent<Dirt>().isHydraded)
                {
                    hitty.collider.gameObject.GetComponent<Dirt>().isHydraded = true;
                    //sulandý 
                    StartCoroutine(Growto22());
                }
                if (hitty.collider.gameObject.GetComponent<Dirt>().isCuttable)
                {
                    //objeyi kes tohum ve ürün ver objeyi de destroyla...
                    Debug.Log("Kesildi");
                }
            }
        }
        //Debug.Log(isHydraded+" "+isPlanted+" "+isHoed);
        Debug.Log(hitty.collider.gameObject.GetComponent<Dirt>().isHoed + " " + hitty.collider.gameObject.GetComponent<Dirt>().isPlanted + " " +
            hitty.collider.gameObject.GetComponent<Dirt>().isHydraded);
    }
    IEnumerator Growto22()
    {
        yield return new WaitForSeconds(10);
        // tohumun 2.halini koy
        Debug.Log("2.hal");

        yield return new WaitForSeconds(10);
        // tohumun 3.halini koy
        Debug.Log("3.hal");

        yield return new WaitForSeconds(10);
        // tohumun 4.halini koy
        Debug.Log("4.hal");

        yield return new WaitForSeconds(10);
        // tohumun 5.halini koy en son hali..
        Debug.Log("5.hal");
        hitty.collider.gameObject.GetComponent<Dirt>().isCuttable = true;
    }
}
