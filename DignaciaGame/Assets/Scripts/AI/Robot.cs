using UnityEngine;

public class Robot : MonoBehaviour
{
    bool isNewBought;
    bool ischipped;

    void Update()
    {
        if (isNewBought && !ischipped)
        {
            //�ip verilme sistemi
            ischipped = true;
            isNewBought = false;
        }
    }
}
