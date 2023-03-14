using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunScript : MonoBehaviour
{
    [SerializeField] GameObject LaserGunAmmo;
    [SerializeField] GameObject Player;

    

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.transform.position;
        if(Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(LaserGunAmmo , transform.position , Quaternion.Euler(0,0,0));
            
            
        }
    }
}
