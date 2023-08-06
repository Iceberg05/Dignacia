using UnityEngine;
using TMPro;

[RequireComponent(typeof(BoxCollider2D))]
public class TableSlot : MonoBehaviour
{
    [SerializeField] bool isEmpty = true;
    bool isInteractable;
    bool isBuyable;

    public float priceOfSlot;

    [SerializeField] TMP_InputField priceInputField;
    [SerializeField] GameObject pricePanel;
    void Awake()
    {
        if (PlayerPrefs.GetInt("Is " + transform.parent.name + gameObject.name + " Empty", 1) == 1) isEmpty = true;
        else isEmpty = false;

        if (PlayerPrefs.GetInt("Is " + transform.parent.name + gameObject.name + " Buyable", 1) == 1) isBuyable = true;
        else isBuyable = false;
    }
    void Update()
    {
        if(isInteractable && (Input.GetButtonDown("Interact") || Input.GetButtonDown("Fire1")))
        {
            //Envanteri açýp bir item seçilmesi istenir. Item seçildikten sonra ise bir input field'dan fiyat biçilmesi istenir. Aþaðýda çaðýrýlan fonksiyon, eþya seçildikten sonra çaðýrýlacaktýr.
            //Not: Sadece envanter deðil, sandýklardaki veya farklý depolardaki itemlara da eriþilebilir. Böylece oyuncu satýþ için tek tek sandýktan eþya toplayýp gelmek zorunda kalmaz.
            pricePanel.SetActive(true);
        }

        priceOfSlot = PlayerPrefs.GetFloat("Price of " + transform.parent.name + gameObject.name, 0f);
    }
    void OnMouseEnter()
    {
        if(isEmpty) GetComponent<SpriteRenderer>().color = Color.green;
        else GetComponent<SpriteRenderer>().color = Color.yellow;
        isInteractable = true;
    }
    void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
        isInteractable = false;
    }
    void PutUpForSale(GameObject item)
    {
        GameObject itemOnTable = Instantiate(item, transform);
        itemOnTable.transform.position = new Vector3(0f, 0f, 0f);
        isEmpty = false;
        isBuyable = true;
        PlayerPrefs.SetInt("Is " + transform.parent.name + gameObject.name + " Empty", 0);
        PlayerPrefs.SetInt("Is " + transform.parent.name + gameObject.name + " Buyable", 1);
    }
    public void OKButton()
    {
        if(priceInputField.text != "")
        {
            PlayerPrefs.SetFloat("Price of " + transform.parent.name + gameObject.name, float.Parse(priceInputField.text));
            pricePanel.SetActive(false);
            priceInputField.text = "";

            //PutUpForSale(Seçili obje);
        }
    }
}