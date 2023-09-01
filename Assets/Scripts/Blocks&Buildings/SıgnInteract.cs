using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SıgnInteract : MonoBehaviour
{
    public TMP_Text tableText;
    public TMP_InputField inputField;
    private string savedTextKey = "table_text";

    public GameObject tablePanel;

    [SerializeField] bool isSignInteractible;
    [SerializeField] bool isPanelActive;


    Character player;
    private void Start()
    {
        LoadSavedText();
        player = FindObjectOfType<Character>();
    }

    private void LoadSavedText()
    {
        if (PlayerPrefs.HasKey(savedTextKey))
        {
            string savedText = PlayerPrefs.GetString(savedTextKey);
            tableText.text = savedText;
        }
    }
    void Update()
    {
        if (isSignInteractible && Input.GetButtonDown("Interact") && !isPanelActive)
        {
            tablePanel.SetActive(true);
            Invoke("MakePanelActive", 0.1f);
            isSignInteractible = false;
        }

        if (isPanelActive) player.GetComponent<Character>().enabled = false;
        else player.GetComponent<Character>().enabled = true;

        if (isPanelActive && (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Interact")) && !isSignInteractible)
        {
            tablePanel.SetActive(false);
            isPanelActive = false;
        }
    }
    public void SaveText()
    {
        string inputText = inputField.text;
        PlayerPrefs.SetString(savedTextKey, inputText);
        tableText.text = inputText;
        inputField.text = "";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isSignInteractible = true;
            Debug.Log("Girdi");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isPanelActive = false;
        isSignInteractible = false;
    }
    public void MakePanelActive()
    {
        isPanelActive = true;
    }
}