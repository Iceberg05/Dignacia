using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "LaserAmmo")
        {
            LaserShooting.laserAmmo = LaserShooting.laserAmmo + 10;
        }
        if (other.gameObject.tag == "MagnumAmmo")
        {
            MagnumShooting.magnumAmmo = MagnumShooting.magnumAmmo + 10;
        }
        if (other.gameObject.tag == "MKSAmmo")
        {
            MKSShooting.MKSAmmo = MKSShooting.MKSAmmo + 10;
        }

    }
}
