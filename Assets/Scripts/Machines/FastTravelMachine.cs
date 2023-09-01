//Kod Emir Ba� taraf�ndan yaz�ld�.

using UnityEngine;

public class FastTravelMachine : MonoBehaviour
{
    FastTravelMachineManager fastTravelManager;

    bool isMachineActive;
    bool canInteract;
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
        if(canInteract)
        {
            //Interaksiyona ge�mesi i�in UI texti aktif edilir.
            if(Input.GetButtonDown("Interact"))
            {
                fastTravelManager.isPlayerTraveling = !fastTravelManager.isPlayerTraveling;
            }
        }
        if (fastTravelManager.isPlayerTraveling)
        {
            fastTravelManager.currentMachineNumber = fastTravelManager.fastTravelMachines.IndexOf(gameObject.transform);
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            canInteract = true;
        }
    }
    /*void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Staying");
            //Interaksiyona ge�mesi i�in UI texti aktif edilir.
            if (Input.GetButtonDown("Interact"))
            {
                //Interaksiyon tamamland�ktan sonra UI texti kapat�l�r.
                Debug.Log("Staying Interact");
                fastTravelManager.isPlayerTraveling = !fastTravelManager.isPlayerTraveling;
            }
        }
    }*/
    void OnTriggerExit2D(Collider2D col)
    {
       if(col.gameObject.tag == "Player")
        {
            canInteract = false;
            fastTravelManager.isPlayerTraveling = false;
        }
    }
}
