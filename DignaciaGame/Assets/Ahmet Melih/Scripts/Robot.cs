using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    bool isnewBought;
    bool ischipped;

    private void Update()
    {
        if (isnewBought && !ischipped)
        {
            //�ip ver buna
            ischipped=true;
            isnewBought = false;
        }
    }
}
