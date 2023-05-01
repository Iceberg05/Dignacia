using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmingScriptNormal : MonoBehaviour
{
    RaycastHit2D hit;

    [SerializeField] List<Plant> plants = new List<Plant>();
    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastSystem();
        }
    }
    public void RaycastSystem()
    {
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Dirt" && GetComponent<Character>().modeName == "Farming")
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
                    StartCoroutine(Grow());
                }
                if (hit.collider.gameObject.GetComponent<Dirt>().isCuttable)
                {
                    //objeyi kes tohum ve ürün ver objeyi de destroyla...
                    Debug.Log("Kesildi");
                }
            }
        }
    }
    IEnumerator Grow()
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
