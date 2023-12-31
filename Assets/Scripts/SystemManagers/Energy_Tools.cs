using UnityEngine;

public class Energy_Tools : MonoBehaviour
{
    [SerializeField] Energy_Controller energyController;

    [Tooltip("Harcadığı enerji miktarıdır.")]
    [SerializeField] int spentEnergy;

    void Start()
    {
        energyController.maxEnergy = energyController.maxEnergy + spentEnergy;
        energyController.currentEnergy = energyController.currentEnergy + spentEnergy;
    }
}
