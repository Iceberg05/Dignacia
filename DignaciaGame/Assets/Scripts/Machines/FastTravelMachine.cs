//Kod Emir Ba� taraf�ndan yaz�ld�.

using UnityEngine;

public class FastTravelMachine : MonoBehaviour
{
    FastTravelMachineManager fastTravelManager;

    bool isMachineActive;
    void Start()
    {
        fastTravelManager = FindObjectOfType<FastTravelMachineManager>();
        isMachineActive = true;
    }
    void Update()
    {
        if(isMachineActive)
        {
            fastTravelManager.fastTravelMachines.Add(gameObject.transform);
            isMachineActive = false;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            //Interaksiyona ge�mesi i�in UI texti aktif edilir.
            if (Input.GetButtonDown("Interact"))
            {
                //Interaksiyon tamamland�ktan sonra UI texti kapat�l�r.
                if(fastTravelManager.isPlayerTraveling)
                {
                    fastTravelManager.isPlayerTraveling = false;
                }
                else
                {
                    fastTravelManager.currentMachineNumber = fastTravelManager.fastTravelMachines.IndexOf(gameObject.transform);
                    fastTravelManager.isPlayerTraveling = true;
                }
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //Interaksiyona ge�mesi i�in UI texti aktif edilir.
            if (Input.GetButtonDown("Interact"))
            {
                //Interaksiyon tamamland�ktan sonra UI texti kapat�l�r.
                if (fastTravelManager.isPlayerTraveling)
                {
                    fastTravelManager.isPlayerTraveling = false;
                }
                else
                {
                    fastTravelManager.currentMachineNumber = fastTravelManager.fastTravelMachines.IndexOf(gameObject.transform);
                    fastTravelManager.isPlayerTraveling = true;
                }
            }
        }
    }
}
