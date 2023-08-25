
//Coderlar için not: Bu makine taþýnabilirdir ve oyuncu makineyi istediði yere koyabilir. Koyulduðu yerde direkt olarak bir zamanlayýcý baþlatýr, zamanlayýcý bittikten sonra
//uygun olan yüzeylerde çimen spawn eder. Çimenlerin kendi içinde kodu olacak, bu kodlarda da zaman geçtikçe onlarýn büyümesi saðlanacak. Oyuncu makineyi iþlemin neredeyse
//sonuna yaklaþýldýðýnda taþýsa bile süre 0lanýr ve ayný yere koyulsa dahi tüm süreyi baþtan bekler (ileride bunu deðiþtirebiliriz ama þimdilik böyle).

using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;

public class TreeMachine : MonoBehaviour
{
    [Header("Machine")]

    [Tooltip("Ýçerisinde aðaç kodu olan aðaç prefableridir.")]
    [SerializeField] GameObject treePrefab;


    [SerializeField] List<GameObject> dirtsInRange = new List<GameObject>();

    [Tooltip("0. sprite makinenin normal hali, 1. sprite highlighted hali, 2. sprite selected halidir.")]
    [SerializeField] Sprite[] sprites;

    [Tooltip("Makinenin ne kadarlýk bir alana ekim yapabildiðini belirtir.")]
    [SerializeField] float range;

    SpriteRenderer rangeCircle;

    [Header("Timer")]

    bool isTimerActive;

    [Tooltip("Aðaçlarýn çýkmasý için kalan zamandýr.")]
    [SerializeField] float remainingTime;

    [Tooltip("Aðaçlarýn kaç dakikada çýkacaðýný belirten deðerdir. Bu süre, oyun içerisinde 5 günde geçecek þekilde ayarlanacaktýr.")]
    [SerializeField] int startMinutes = 1; //1 deðeri test için girildi.

    [Tooltip("Aðaç ekmeye ne kadar vakit kaldýðýný gösterir.")]
    [SerializeField] TMP_Text remainingTimeText;

    void Awake()
    {
        StartTimer();
        GetComponent<CircleCollider2D>().radius = range;
        GetComponent<SpriteRenderer>().sprite = sprites[0];
        rangeCircle = transform.Find("RangeCircle").GetComponent<SpriteRenderer>();
        rangeCircle.enabled = false;
    }
    void Start()
    {
        remainingTime = startMinutes * 60;
    }
    void Update()
    {
        rangeCircle.transform.localScale = new Vector3(GetComponent<CircleCollider2D>().radius + 3, GetComponent<CircleCollider2D>().radius + 3, GetComponent<CircleCollider2D>().radius + 3);

        #region TIMER
        remainingTimeText.text = remainingTime.ToString();

        if (isTimerActive)
        {
            remainingTime = remainingTime - Time.deltaTime;
            if (remainingTime <= 0)
            {
                PlantTree();
                Start();
            }
        }
        TimeSpan time = TimeSpan.FromSeconds(remainingTime);
        remainingTimeText.text = time.ToString(@"mm\m\ ss\s");
        #endregion
    }
    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[1];
        rangeCircle.enabled = true;
    }
    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[0];
        rangeCircle.enabled = false;
    }
    //Mümkün olan tüm yüzeylerde çimen spawn eden fonksiyondur.
    void PlantTree()
    {
        foreach (GameObject plantableSurface in dirtsInRange)
        {
            GameObject tree = Instantiate(treePrefab, plantableSurface.transform, false);
            tree.transform.position = new Vector3(plantableSurface.transform.position.x, plantableSurface.transform.position.y, plantableSurface.transform.position.z);
        }
    }
    void StartTimer()
    {
        isTimerActive = true;
    }
    void StopTimer()
    {
        isTimerActive = false;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Dirt")
        {
            dirtsInRange.Add(col.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Dirt")
        {
            dirtsInRange.Remove(col.gameObject);
        }
    }

    //PLAYER KODUNA ATILACAK VE BU KOD ÝÇERÝSÝNDEN SÝLÝNECEK
    /*void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "GrassMachine")
        {
            //Etkileþim tuþunun UI'da gösteriminin saðlandýðý satýr
            if(Input.GetButtonDown("Interact"))
            {
                //Envantere makineyi ekleme
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "GrassMachine")
        {
            //Etkileþim tuþunun UI'da gösteriminin saðlandýðý satýr
            if (Input.GetButtonDown("Interact"))
            {
                //Envantere makineyi ekleme
                Destroy(gameObject);
            }
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "GrassMachine")
        {
            //Etkileþim tuþunun UI'da gösteriminin yok edilmesini saðlayan satýr
        }
    }*/
}
