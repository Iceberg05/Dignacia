using UnityEngine;

public class BoatMechanic : MonoBehaviour
{
    [Header("Objects & Sprites")]
    SpriteRenderer spriteRenderer;
    Character player;

    [Tooltip("Bot objesidir.")]
    [SerializeField] GameObject boat;

    [Tooltip("Bota bindi�inde oyuncunun bota binmi� halinin resmidir. Bu sayede oyuncunun sadece g�rseli de�i�mi� olacak ve di�er elementler korunacakt�r.")]
    [SerializeField] Sprite boatSprite;

    [Tooltip("Bottan indi�inde oyuncunun normal insan halinin resmidir. Bu sayede oyuncunun sadece g�rseli de�i�mi� olacak ve di�er elementler korunacakt�r.")]
    [SerializeField] Sprite characterSprite;

    [Header("Mechanic Details")]

    bool isInteractionDisabled;
    bool isInWater;

    [Tooltip("A��klama bekleniyor...")]
    [SerializeField] GameObject waterEdges;

    [Tooltip("Oyuncunun botu s�r�p s�rmedi�ini kontrol eden boolean de�eridir.")]
    public bool onBoat;

    [Tooltip("Oyuncu bota bindi�inde sahip olaca�� h�z de�eridir.")]
    [SerializeField] float boatSpeed;

    void Start()
    {
        onBoat = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        boatSpeed = 10f;

        player = GameObject.FindWithTag("Player").GetComponent<Character>();
    }
    void ChangeSpriteToBoatSprite()
    {
        spriteRenderer.sprite = boatSprite;
    }
    void ChangeSpriteToCharacterSprite()
    {
        spriteRenderer.sprite = characterSprite;
    }
    void Update()
    {
        if (!isInteractionDisabled && onBoat && Input.GetButtonDown("Interact"))
        {
            onBoat = false;
            boat.SetActive(true);
            boat.transform.position = transform.position;
            ChangeSpriteToCharacterSprite();
            player.currentMoveSpeed = player.defaultMoveSpeed;
            Debug.Log("�ndi");
        }
        if (isInWater && onBoat)
        {
            waterEdges.SetActive(true);
        }
        else
        {
            waterEdges.SetActive(false);
        }
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Water")
        {
            isInWater = true;
            Debug.Log("Suda");
        }
        if (col.gameObject.tag == "Dirt")
        {
            isInWater = false;
            Debug.Log("Suda De�il");
        }

        if (!onBoat && col.gameObject.tag == "Boat" && isInWater)
        {
            if (Input.GetButtonDown("Interact"))
            {
                onBoat = true;
                ChangeSpriteToBoatSprite();
                player.currentMoveSpeed = boatSpeed;
                Debug.Log("Bindi");
                isInteractionDisabled = true;
                boat.SetActive(false);
                Invoke("ActiveInteraction", 0.1f);
            }
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Boat" && isInWater)
        {
            if (!onBoat && Input.GetButtonDown("Interact"))
            {
                onBoat = true;
                ChangeSpriteToBoatSprite();
                player.currentMoveSpeed = boatSpeed;
                Debug.Log("Bindi");
                isInteractionDisabled = true;
                boat.SetActive(false);
                Invoke("ActiveInteraction", 0.1f);
            }
        }
    }
    public void WhenLevelUp()
    {
        boatSpeed += 5; //h�z de�erleri de�i�tirilebilir
                        //bu tekne geli�tirmesidir h�z� artar 
                        //bu geli�tirme level al�nd���nda level al�nan yere eklenmelidir
                        //daha kapasitesi artt�r�lacak ve e�ya falan ta��maya ba�layacaklar...

    }
    void ActiveInteraction()
    {
        isInteractionDisabled = false;
    }
}