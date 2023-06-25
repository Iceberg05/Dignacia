using UnityEngine;
using UnityEngine.UI;

public class Energy_Controller : MonoBehaviour
{
    [SerializeField] Text maxenergyText;
    [SerializeField] Text currenterengyText;

    public int maxEnergy = 25;
    public int currentEnergy;
    void Start()
    {
        currentEnergy = maxEnergy;
    }
    void Update()
    {
        maxenergyText.text = maxEnergy.ToString();
        currenterengyText.text = currentEnergy.ToString();
    }
}
