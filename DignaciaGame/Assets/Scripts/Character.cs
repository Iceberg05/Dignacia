using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

    public  int money;
    public Text Moneytext;

    void Start()
    {
        money = 1122;
    }

    // Update is called once per frame
    void Update()
    {
        Moneytext.text = money.ToString();
    }




    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("shop"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

            }
        }
    }


}
