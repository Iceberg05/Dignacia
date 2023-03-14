using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playeremir : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Horse;
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
            Horse.SetActive(true);
            Horse.transform.position = transform.position;
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
                PlayerMovement.runSpeed = 10f;
                Debug.Log("bindi");
                Horse.SetActive(false);
                isInteractionDisabled = true;
                Invoke("ActiveInteraction", 0.1f);
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
                    PlayerMovement.runSpeed = 10f;
                Debug.Log("bindi");
                Horse.SetActive(false);
                isInteractionDisabled = true;
                Invoke("ActiveInteraction", 0.1f);
            }

        }
        }



    }

