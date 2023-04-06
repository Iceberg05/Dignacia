using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunChange : MonoBehaviour
{
    public GameObject[] guns;
    int i;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            guns[0].SetActive(true);
            guns[1].SetActive(false);
            guns[2].SetActive(false);
            
        }

        if(Input.GetKeyDown(KeyCode.Alpha2)){
            guns[0].SetActive(false);
            guns[1].SetActive(true);
            guns[2].SetActive(false);
            
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            guns[0].SetActive(false);
            guns[1].SetActive(false);
            guns[2].SetActive(true);
            
        }
    }
}
