using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField] GameObject turretBullet;
    [SerializeField] GameObject mksTurretBullet;
    [SerializeField] GameObject finalBullet;
    public static bool MKSEffect = false;
    
    
    void Start()
    {
        InvokeRepeating("Shoot" , 1f , 1f);
    }

   void Update()
   {
    if(MKSEffect == true)
    {
        StartCoroutine(Wait());


    }
    
    
    
   }
    
    void Shoot()
    {
        if(MKSEffect == true)
        {
            finalBullet = mksTurretBullet;
        }
        else
        {
            finalBullet = turretBullet;

        }
        Instantiate(finalBullet , transform.position , Quaternion.Euler(0,0,0));
        
    }
   
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "MKSBullet")
        {
            MKSEffect = true;
            Debug.Log("Çalıştı");
        }
        
    }

    
    

    
    
    
    public static void TurretBulletMKS()
    {
        if(Health.armor != 0)
        {
         Health.health = Health.health - 5;
         Health.armor = Health.armor - 5;
        }
       if(Health.armor == 0)
       {
        Health.health = Health.health - 10;

       }
    }
    public static void TurretBulletNormal()
    {
       if(Health.armor != 0){
        Health.health = Health.health - 15;
        Health.armor = Health.armor - 15;
       }
       if(Health.armor == 0)
       {
        Health.health = Health.health - 20;

       }

    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        MKSEffect = false;

    }
   
    
}
