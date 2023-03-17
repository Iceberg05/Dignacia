using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnumShooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullettPrefab;
    public float bulletForce = 20f;

    public static int ammo = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if(ammo > 0)
            {
                Shoot();
            }
            Debug.Log(ammo);

        }
    }
    void Shoot(){
        ammo = ammo -1 ;
        GameObject bullet = Instantiate(bullettPrefab , firePoint.position , firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
    
}
