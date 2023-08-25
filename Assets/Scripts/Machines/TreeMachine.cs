
//Coderlar i�in not: Bu makine ta��nabilirdir ve oyuncu makineyi istedi�i yere koyabilir. Koyuldu�u yerde direkt olarak bir zamanlay�c� ba�lat�r, zamanlay�c� bittikten sonra
//uygun olan y�zeylerde �imen spawn eder. �imenlerin kendi i�inde kodu olacak, bu kodlarda da zaman ge�tik�e onlar�n b�y�mesi sa�lanacak. Oyuncu makineyi i�lemin neredeyse
//sonuna yakla��ld���nda ta��sa bile s�re 0lan�r ve ayn� yere koyulsa dahi t�m s�reyi ba�tan bekler (ileride bunu de�i�tirebiliriz ama �imdilik b�yle).

using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;

public class TreeMachine : MonoBehaviour
{
    [Header("Machine")]

    [Tooltip("��erisinde a�a� kodu olan a�a� prefableridir.")]
    [SerializeField] GameObject treePrefab;


    [SerializeField] List<GameObject> dirtsInRange = new List<GameObject>();

    [Tooltip("0. sprite makinenin normal hali, 1. sprite highlighted hali, 2. sprite selected halidir.")]
    [SerializeField] Sprite[] sprites;

    [Tooltip("Makinenin ne kadarl�k bir alana ekim yapabildi�ini belirtir.")]
    [SerializeField] float range;

    SpriteRenderer rangeCircle;

    [Header("Timer")]

    bool isTimerActive;

    [Tooltip("A�a�lar�n ��kmas� i�in kalan zamand�r.")]
    [SerializeField] float remainingTime;

    [Tooltip("A�a�lar�n ka� dakikada ��kaca��n� belirten de�erdir. Bu s�re, oyun i�erisinde 5 g�nde ge�ecek �ekilde ayarlanacakt�r.")]
    [SerializeField] int startMinutes = 1; //1 de�eri test i�in girildi.

    [Tooltip("A�a� ekmeye ne kadar vakit kald���n� g�sterir.")]
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
    //M�mk�n olan t�m y�zeylerde �imen spawn eden fonksiyondur.
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

    //PLAYER KODUNA ATILACAK VE BU KOD ��ER�S�NDEN S�L�NECEK
    /*void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "GrassMachine")
        {
            //Etkile�im tu�unun UI'da g�steriminin sa�land��� sat�r
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
            //Etkile�im tu�unun UI'da g�steriminin sa�land��� sat�r
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
            //Etkile�im tu�unun UI'da g�steriminin yok edilmesini sa�layan sat�r
        }
    }*/
}
