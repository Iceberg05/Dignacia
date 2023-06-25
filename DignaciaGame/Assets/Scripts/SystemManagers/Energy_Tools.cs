using UnityEngine;

public class Energy_Tools : MonoBehaviour
{
    [SerializeField] Energy_Controller energyController;

    [Tooltip("Harcad��� enerji miktar�d�r.")]
    [SerializeField] int spentEnergy;

    void Start()
    {
        energyController.maxEnergy = energyController.maxEnergy + spentEnergy;
        energyController.currentEnergy = energyController.currentEnergy + spentEnergy;
    }
}
