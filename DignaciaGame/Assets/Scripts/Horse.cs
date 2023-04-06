using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horse : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerMovement PlayerMovement;
    public AnimalScript animalscript;
    public GameObject Horsee;
    public SpriteRenderer spriteRenderer;
    public Sprite HorseSprite;
    public Sprite ClassicSprite;
    public bool OnHorse;
    bool isInteractionDisabled;
 void Start()
    {
        isInteractionDisabled = true;
           OnHorse = false;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    void ChangeSprite()
    {
        spriteRenderer.sprite = HorseSprite;
    }
    void Classicsprite()
    {
        spriteRenderer.sprite = ClassicSprite;
    }

        

    // Update is called once per frame
    void Update()
    {
        if ((isInteractionDisabled == false) && (OnHorse == true) && (Input.GetKeyDown(KeyCode.E)))
        {
            OnHorse = false;
            Horsee.SetActive(true);
            Horsee.transform.position = transform.position;
            Classicsprite();
            PlayerMovement.runSpeed = 4f;
            Debug.Log("indi");

        }
    }


    void ActiveInteraction()
    {
        isInteractionDisabled = false;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (OnHorse == false & other.gameObject.tag == "Horse")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnHorse = true;
                ChangeSprite();
                
                Debug.Log("bindi");
                Horsee.SetActive(false);
                isInteractionDisabled = true;
                Invoke("ActiveInteraction", 0.1f);
                if (animalscript.hungerLevel == 100f)
                {
                    PlayerMovement.runSpeed = 10f;
                }
                if (animalscript.hungerLevel >= 50f)
                {
                    PlayerMovement.runSpeed = 8f;
                }
                if (animalscript.hungerLevel <= 50f)
                {
                    PlayerMovement.runSpeed = 6f;
                }
            }

        }
    }
    void OnTriggerStay2D(Collider2D other)
        {
            if (other.gameObject.tag == "Horse")
            {
            if (OnHorse == false & Input.GetKeyDown(KeyCode.E))
            {
                OnHorse = true;
                    ChangeSprite();
                    
                Debug.Log("bindi");
                Horsee.SetActive(false);
                isInteractionDisabled = true;
                Invoke("ActiveInteraction", 0.1f);
                if (animalscript.hungerLevel == 100f)
                {
                  PlayerMovement.runSpeed = 10f;
                }
                if (animalscript.hungerLevel >= 50f)
                {
                    PlayerMovement.runSpeed = 8f;
                }
                if (animalscript.hungerLevel <= 50f)
                {
                    PlayerMovement.runSpeed = 6f;
                }

            }

        }
        }



    }

