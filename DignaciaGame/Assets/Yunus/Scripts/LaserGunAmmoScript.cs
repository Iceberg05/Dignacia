using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunAmmoScript : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(new Vector2(3,0) * Time.deltaTime);
        Destroy(gameObject , 3f);
    }
}
