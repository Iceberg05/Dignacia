using System.Collections;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    [SerializeField] GameObject turretBullet;
    [SerializeField] GameObject mksTurretBullet;
    [SerializeField] GameObject finalBullet;
    public static bool MKSEffect = false;

    Character player;
    void Start()
    {
        InvokeRepeating("Shoot" , 1f , 1f);
        player = GameObject.FindWithTag("Player").GetComponent<Character>();
    }
    void Update()
    {
        if (MKSEffect == true)
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
        if(HealthSystem.armor != 0)
        {
            HealthSystem.health = HealthSystem.health - 5;
            HealthSystem.armor = HealthSystem.armor - 5;
        }
       if(HealthSystem.armor == 0)
       {
            HealthSystem.health = HealthSystem.health - 10;

       }
    }
    public static void TurretBulletNormal()
    {
       if(HealthSystem.armor != 0){
            HealthSystem.health = HealthSystem.health - 15;
            HealthSystem.armor = HealthSystem.armor - 15;
       }
       if(HealthSystem.armor == 0)
       {
            HealthSystem.health = HealthSystem.health - 20;

       }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        MKSEffect = false;
    }
}
