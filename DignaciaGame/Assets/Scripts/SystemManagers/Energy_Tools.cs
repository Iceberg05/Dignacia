using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy_Tools : MonoBehaviour
{
    

   public Energy_Controller energy_controller;
    public int spentenergy;

    

    void Start()
    {



        energy_controller.maxEnergy = energy_controller.maxEnergy + spentenergy;
        energy_controller.currentEnergy = energy_controller.currentEnergy + spentenergy;
    }






   



    void Update()
    {
        


    }
}
