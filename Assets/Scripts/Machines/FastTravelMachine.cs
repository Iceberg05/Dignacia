//Kod Emir Baþ tarafýndan yazýldý.

using UnityEngine;

public class FastTravelMachine : MonoBehaviour
{
    FastTravelMachineManager fastTravelManager;
    Animator animator;

    bool isMachineActive;
    bool canInteract;

    public string pointName;
    public int pointNumber;

    bool isTriggering;
    public bool isAlreadyOpened;
    void Start()
    {
        animator = GetComponent<Animator>();
        fastTravelManager = FindObjectOfType<FastTravelMachineManager>();
        isMachineActive = true;
    }
    void Update()
    {
        pointNumber = fastTravelManager.fastTravelMachines.IndexOf(transform);
        if(isMachineActive)
        {
            fastTravelManager.fastTravelMachines.Add(gameObject.transform);
            isMachineActive = false;
        }
        if(canInteract && !fastTravelManager.nameInputField.isFocused)
        {
            //Interaksiyona geçmesi için UI texti aktif edilir.
            if (Input.GetButtonDown("Interact") && !isAlreadyOpened)
            {
                fastTravelManager.OpenUI(true);
                isAlreadyOpened = true;
                fastTravelManager.currentMachineNumber = fastTravelManager.fastTravelMachines.IndexOf(gameObject.transform);
                foreach (FastTravelMachine fastTravelMachine in FindObjectsOfType<FastTravelMachine>())
                {
                    fastTravelMachine.isAlreadyOpened = true;
                }
            }
            else if (Input.GetButtonDown("Interact") && isAlreadyOpened)
            {
                fastTravelManager.OpenUI(false);
                isAlreadyOpened = false;
                foreach (FastTravelMachine fastTravelMachine in FindObjectsOfType<FastTravelMachine>())
                {
                    fastTravelMachine.isAlreadyOpened = false;
                }
            }
        }
        if(pointName == null || pointName == "") pointName = "Point " + pointNumber.ToString();
        else pointName = PlayerPrefs.GetString(pointNumber.ToString(), "Point " + pointNumber.ToString());
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            canInteract = true;
            animator.Play("Anim_MachineOpen");
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
       if(col.gameObject.tag == "Player")
        {
            canInteract = false;
            animator.Play("Anim_MachineClose");
        }
    }
}
