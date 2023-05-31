//Kod Emir Baþ tarafýndan yazýldý.

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
            //Interaksiyona geçmesi için UI texti aktif edilir.
            if (Input.GetButtonDown("Interact"))
            {
                //Interaksiyon tamamlandýktan sonra UI texti kapatýlýr.
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
            //Interaksiyona geçmesi için UI texti aktif edilir.
            if (Input.GetButtonDown("Interact"))
            {
                //Interaksiyon tamamlandýktan sonra UI texti kapatýlýr.
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
