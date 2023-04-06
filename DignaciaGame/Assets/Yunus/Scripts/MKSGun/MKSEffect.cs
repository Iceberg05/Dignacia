using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKSEffect : MonoBehaviour
{
    /*
    IF A GAMEOBJECT CONTAINS THIS SCRIPT AND COLLIDES WITH A MKS BULLET. GAME OBEJCT'S SIZE REDUCES BY (GAMEOBEJCTS SIZE / reductionParamter) 
    AND AFTER (MKSEffectTime) SECONDS LATER GAMEOBJECT WILL TURN BACK ITS ORIGINAL SIZE. 
    */
    [SerializeField] private int reductionParameter = 2;
    [SerializeField] private int MKSEffecttTime = 3;
    public static bool GeneralMKSEffect = false;
    
    

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "MKSBullet")
        {
           
           StartCoroutine(GeneralMKSEffectFunction());
            

        }
       
    }
    
    IEnumerator GeneralMKSEffectFunction()
    {
        GeneralMKSEffect = true;
        transform.localScale = transform.localScale / reductionParameter;
        yield return new WaitForSeconds(MKSEffecttTime);
        GeneralMKSEffect = false;
        transform.localScale = transform.localScale * reductionParameter;
    }
}
    
