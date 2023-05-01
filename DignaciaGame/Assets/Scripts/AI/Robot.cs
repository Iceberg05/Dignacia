using UnityEngine;

public class Robot : MonoBehaviour
{
    bool isnewBought;
    bool ischipped;

    private void Update()
    {
        if (isnewBought && !ischipped)
        {
            //Çip verilme sistemi
            ischipped = true;
            isnewBought = false;
        }
    }
}
