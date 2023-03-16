using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField] GameObject turretBullet;
    void Start()
    {
        InvokeRepeating("Shoot" , 1f , 1f);
    }

   
    void Update()
    {
        
    }
    void Shoot()
    {
        Instantiate(turretBullet , transform.position , Quaternion.Euler(0,0,0));
        
    }
}
