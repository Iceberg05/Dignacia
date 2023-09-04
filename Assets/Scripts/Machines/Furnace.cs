using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Furnace : MonoBehaviour
{



    public GameObject furnacePanel;
    public Button WaterButton;
    public Button CopperButton;
    public Button GoldButton;
    public Button GlassButton;
    public Button UraniumButton;
    public Button AluninumButton;
    public Button IronButton;

    private int Dignoryum = 30;
    private int iceCube = 30;
    private int IronMine = 30;
    private int GoldMine = 30;
    private int CopperMine = 30;
    private int UraniumMineral = 30;
    private int AluminumMine = 30;
    private int SandStone = 30;

    #region Button
    void Waterbutton()
    {
        if (iceCube > 0 && Dignoryum > 0)
        {
            iceCube -= 1;
            Dignoryum -= 1;
            Debug.Log(iceCube);
        }
    }
    void Copperbutton()
    {
        CopperMine -= 1;
        Dignoryum -= 2;
        Debug.Log(CopperMine);
    }
    void Goldbutton()
    {
        GoldMine -= 1;
        Dignoryum -= 5;
        Debug.Log(GoldMine);
    }
    void Glassbutton()
    {
        SandStone -= 1;
        Dignoryum -= 2;
        Debug.Log(SandStone);
    }
    void Uraniumbutton()
    {
        UraniumMineral -= 1;
        Dignoryum -= 10;
        Debug.Log(UraniumMineral);
    }
    void Aluninumbutton()
    {
        AluminumMine -= 1;
        Dignoryum -= 3;
        Debug.Log(AluminumMine);
    }
    void Ironbutton()
    {
        IronMine -= 1;
        Dignoryum -= 2;
        Debug.Log(IronMine);
    }
    #endregion

    private void Start()
    {
        WaterButton.onClick.AddListener(Waterbutton);
        CopperButton.onClick.AddListener(Copperbutton);
        GoldButton.onClick.AddListener(Goldbutton);
        GlassButton.onClick.AddListener(Glassbutton);
        UraniumButton.onClick.AddListener(Uraniumbutton);
        AluninumButton.onClick.AddListener(Aluninumbutton);
        IronButton.onClick.AddListener(Ironbutton);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.E))
        {
            furnacePanel.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")

            furnacePanel.SetActive(false);
    }
}
