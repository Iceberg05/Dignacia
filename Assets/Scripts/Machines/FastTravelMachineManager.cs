//Kod Emir Ba� taraf�ndan yaz�ld�.

using System.Collections.Generic;
using UnityEngine;

public class FastTravelMachineManager : MonoBehaviour
{
    Character player;

    //Bununla alakal� bir �ey yap�lmas�na gerek yok, otomatik olarak aktif fast travel makinelerini bulur.
    public List<Transform> fastTravelMachines = new List<Transform>();

    [Tooltip("En son ���nlanmada kullan�lan makinenin fastTravelMachines listesindeki numaras�d�r.")]
    public int currentMachineNumber;

    [Tooltip("Oyuncunun ���nlanmakta oldu�unu belirten k�s�md�r.")]
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
