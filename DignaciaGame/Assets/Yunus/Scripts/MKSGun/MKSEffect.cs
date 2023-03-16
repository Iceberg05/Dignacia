using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKSEffect : MonoBehaviour
{
    [SerializeField] private int reductionParameter = 2;
    public static bool turretMKSEffect;
    void Start()
    {
        turretMKSEffect = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "MKSBullet")
        {
           StartCoroutine(TurretMKSEffect());
            

        }
    }
    IEnumerator TurretMKSEffect()
    {
        turretMKSEffect = true;
        transform.localScale = transform.localScale / reductionParameter;
        yield return new WaitForSeconds(3);
        turretMKSEffect = false;
        transform.localScale = transform.localScale * reductionParameter;
    }
}
    
