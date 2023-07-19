using UnityEngine;

public class LifePod : MonoBehaviour
{
    [SerializeField] bool isTriggering;
    [SerializeField] bool isAlreadyOpened;

    ToDoListSystem toDoListManager;
    void Start()
    {
        toDoListManager = FindObjectOfType<ToDoListSystem>();
    }
    void Update()
    {
        if(Input.GetButtonDown("Interact") && isTriggering && !isAlreadyOpened)
        {
            toDoListManager.OpenUI(true);
            isAlreadyOpened = true;
        }
        else if(Input.GetButtonDown("Interact") && isTriggering && isAlreadyOpened)
        {
            toDoListManager.OpenUI(false);
            isAlreadyOpened = false;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            isTriggering = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            isTriggering = false;
            toDoListManager.OpenUI(false);
            isAlreadyOpened = false;
        }
    }
}
