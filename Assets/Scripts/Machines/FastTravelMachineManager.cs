//Kod Emir Baþ tarafýndan yazýldý.

using System.Collections.Generic;
using UnityEngine;

public class FastTravelMachineManager : MonoBehaviour
{
    Character player;

    //Bununla alakalý bir þey yapýlmasýna gerek yok, otomatik olarak aktif fast travel makinelerini bulur.
    public List<Transform> fastTravelMachines = new List<Transform>();

    [Tooltip("En son ýþýnlanmada kullanýlan makinenin fastTravelMachines listesindeki numarasýdýr.")]
    public int currentMachineNumber;

    [Tooltip("Oyuncunun ýþýnlanmakta olduðunu belirten kýsýmdýr.")]
    public bool isPlayerTraveling;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Character>();
    }
    void Update()
    {
        if(isPlayerTraveling && fastTravelMachines.Count > 1)
        {
            if(Input.GetButtonDown("Next"))
            {
                if (currentMachineNumber == fastTravelMachines.Count - 1)
                {
                    currentMachineNumber = 0;
                } else
                {
                    currentMachineNumber++;
                }
                player.transform.position = fastTravelMachines[currentMachineNumber].transform.position;
            }
            else if(Input.GetButtonDown("Previous"))
            {
                if (currentMachineNumber == 0)
                {
                    currentMachineNumber = fastTravelMachines.Count - 1;
                }
                else
                {
                    currentMachineNumber--;
                }
                player.transform.position = fastTravelMachines[currentMachineNumber].transform.position;
            }
        }
    }
}
