using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingScriptNormal : MonoBehaviour
{
    
    
    RaycastHit2D hit;

    

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            DoIt();

        }
        
           
    }

    public void DoIt()
    {
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            //Debug.Log("Target Position: " + hit.collider.gameObject.transform.position
            //    + " AND TARGET NAME: " + hit.collider.gameObject.name);

            if (hit.collider.gameObject.tag == "Dirt") //ve elinde çapa varsa...
            {


                if (!hit.collider.gameObject.GetComponent<Dirt>().isHoed)
                {
                    hit.collider.gameObject.GetComponent<Dirt>().isHoed = true;
                    //çapalandý 
                }
                else if (!hit.collider.gameObject.GetComponent<Dirt>().isPlanted)
                {
                    hit.collider.gameObject.GetComponent<Dirt>().isPlanted = true;
                    //ekildi ilk ekili hali olacak
                }
                else if (!hit.collider.gameObject.GetComponent<Dirt>().isHydraded)
                {
                    hit.collider.gameObject.GetComponent<Dirt>().isHydraded = true;
                    //sulandý 
                    StartCoroutine(Growto2());
                }

                if (hit.collider.gameObject.GetComponent<Dirt>().isCuttable)
                {
                    //objeyi kes tohum ve ürün ver objeyi de destroyla...
                    Debug.Log("Kesildi");
                }



            }
        }

        //Debug.Log(isHydraded+" "+isPlanted+" "+isHoed);
        Debug.Log(hit.collider.gameObject.GetComponent<Dirt>().isHoed + " " + hit.collider.gameObject.GetComponent<Dirt>().isPlanted + " " +
            hit.collider.gameObject.GetComponent<Dirt>().isHydraded);

    }

    IEnumerator Growto2()
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
        hit.collider.gameObject.GetComponent<Dirt>().isCuttable = true;
    }
    

    
}
