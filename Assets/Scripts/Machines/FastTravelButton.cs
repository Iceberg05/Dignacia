using UnityEngine;

public class FastTravelButton : MonoBehaviour
{
    public int pointNumber;

    FastTravelMachineManager fastTravelManager;
    void Awake()
    {
        fastTravelManager = FindObjectOfType<FastTravelMachineManager>();
    }
    public void TeleportToSelectedPoint()
    {
        GameObject.FindWithTag("Player").transform.position = fastTravelManager.fastTravelMachines[pointNumber].transform.position;
    }
}
