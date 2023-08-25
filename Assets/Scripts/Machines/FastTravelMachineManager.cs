//Kod Emir Baþ tarafýndan yazýldý.

using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FastTravelMachineManager : MonoBehaviour
{
    Character player;

    //Bununla alakalý bir þey yapýlmasýna gerek yok, otomatik olarak aktif fast travel makinelerini bulur.
    public List<Transform> fastTravelMachines = new List<Transform>();

    [Tooltip("En son ýþýnlanmada kullanýlan makinenin fastTravelMachines listesindeki numarasýdýr.")]
    public int currentMachineNumber;

    [Tooltip("Mevcut fast travel noktasýnýn isminin girildiði input fielddýr.")]
    public TMP_InputField nameInputField;

    [Tooltip("Fast travel pointlere ýþýnlanmayý saðlayan butonlarýn prefabidir.")]
    [SerializeField] GameObject teleportButtonObject;

    [Tooltip("Fast travel butonlarýnýn içerisinde bulunacaðý paneldir.")]
    [SerializeField] GameObject buttonPanel;

    int pointNumber;
    void Start()
    {
    }
    public void EnterNameButton()
    {
        fastTravelMachines[currentMachineNumber].GetComponent<FastTravelMachine>().pointName = nameInputField.text;
        if (fastTravelMachines[currentMachineNumber].GetComponent<FastTravelMachine>().pointName != "")
        {
            PlayerPrefs.SetString(fastTravelMachines[currentMachineNumber].GetComponent<FastTravelMachine>().pointNumber.ToString(), fastTravelMachines[currentMachineNumber].GetComponent<FastTravelMachine>().pointName);
        }
        else PlayerPrefs.SetString(fastTravelMachines[currentMachineNumber].GetComponent<FastTravelMachine>().pointNumber.ToString(), "Point " + fastTravelMachines[currentMachineNumber].GetComponent<FastTravelMachine>().pointNumber);
    }
    public void OpenUI(bool mustOpen)
    {
        if(mustOpen)
        {
            buttonPanel.SetActive(true);
            pointNumber = 0;
            for (int i = 0; i < fastTravelMachines.Count; i++)
            {
                GameObject pointButton = Instantiate(teleportButtonObject, buttonPanel.transform.Find("ScrollArea").transform.Find("Content"));
                pointButton.transform.Find("TitleText").GetComponent<TMP_Text>().text = fastTravelMachines[i].GetComponent<FastTravelMachine>().pointName;
                nameInputField.text = null;
                pointButton.GetComponent<FastTravelButton>().pointNumber = pointNumber;
                pointNumber++;
            }
        }
        else
        {
            buttonPanel.SetActive(false);
            foreach (Transform child in buttonPanel.transform.Find("ScrollArea").transform.Find("Content"))
            {
                Destroy(child.gameObject);
            }
        }
    }
}