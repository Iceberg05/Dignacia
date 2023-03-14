using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKSEffect : MonoBehaviour
{
    [SerializeField] private int reductionParameter = 2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == "MKSBullet")
        {
            transform.localScale = transform.localScale/reductionParameter;
        }
    }
}
