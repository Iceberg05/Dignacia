using UnityEngine;

public class Robot : MonoBehaviour
{
    bool isNewBought;
    bool ischipped;

    void Update()
    {
        if (isNewBought && !ischipped)
        {
            //Çip verilme sistemi
            ischipped = true;
            isNewBought = false;
        }
    }
}
