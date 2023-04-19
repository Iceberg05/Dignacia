using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMechanic : MonoBehaviour
{
    public GameObject Boat;
    SpriteRenderer spriteRenderer;
    public Sprite BoatSprite;
    public Sprite ClassicSprite;
    public bool OnBoat;
    bool isInteractionDisabled;
    bool isInWater;
    public GameObject waterEdges;
    private float BoatSpeed;



    void Start()
    {
        
        OnBoat = false;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        BoatSpeed = 10f;
    }
    void ChangeSprite()
    {
        spriteRenderer.sprite = BoatSprite;
    }
    void Classicsprite()
    {
        spriteRenderer.sprite = ClassicSprite;
    }
    void Update()
    {
        if ((isInteractionDisabled == false) && (OnBoat == true) && (Input.GetKeyDown(KeyCode.E)))
        {
            OnBoat = false;
            Boat.SetActive(true);
            Boat.transform.position = transform.position;
            Classicsprite();
            PlayerMovement.runSpeed = 4f;
            Debug.Log("indi");
            
        }
        if (isInWater && OnBoat)
        {
            waterEdges.SetActive(true);
        }
        else
        {
            waterEdges.SetActive(false);
        }
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Water")
        {
            isInWater = true;
            Debug.Log("Suda");
        }
        if (other.gameObject.tag=="Dirt")
        {
            isInWater= false;
            Debug.Log("SudaDeil");
        }

        if (OnBoat == false & other.gameObject.tag == "Boat" && isInWater)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnBoat = true;
                ChangeSprite();
                PlayerMovement.runSpeed = BoatSpeed;
                Debug.Log("bindi");
                isInteractionDisabled = true;
                Boat.SetActive(false);
                Invoke("ActiveInteraction", 0.1f);

            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Boat" && isInWater)
        {
            if (OnBoat == false & Input.GetKeyDown(KeyCode.E))
            {
                OnBoat = true;
                ChangeSprite();
                PlayerMovement.runSpeed = BoatSpeed;
                Debug.Log("bindi");
                isInteractionDisabled = true;
                Boat.SetActive(false);
                Invoke("ActiveInteraction", 0.1f);

            }
        }
    }
    public void WhenLevelUp()
    {
        BoatSpeed += 5; //hýz deðerleri deðiþtirilebilir
                        //bu tekne geliþtirmesidir hýzý artar 
                        //bu geliþtirme level alýndýðýnda level alýnan yere eklenmelidir
                        //daha kapasitesi arttýrýlacak ve eþya falan taþýmaya baþlayacaklar...

    }




    void ActiveInteraction()
    {
        isInteractionDisabled = false;
    }
}