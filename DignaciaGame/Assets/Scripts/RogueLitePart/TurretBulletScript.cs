using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletScript : MonoBehaviour
{
    Rigidbody2D rb;
    public static int turretBulletHealthDamage = 40;
    public static int turretBulletArmorDamage = 40;
    public static bool playerHit = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector2.left * 3);
        Destroy(gameObject , 3f);
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player")
        {
            TurretScript.TurretBulletNormal();
        }
        
        
    }

    
    
}
