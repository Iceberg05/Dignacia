using UnityEngine;

public class BoatMechanic : MonoBehaviour
{
    [Header("Objects & Sprites")]
    SpriteRenderer spriteRenderer;
    Character player;

    [Tooltip("Bot objesidir.")]
    [SerializeField] GameObject boat;

    [Tooltip("Bota bindiðinde oyuncunun bota binmiþ halinin resmidir. Bu sayede oyuncunun sadece görseli deðiþmiþ olacak ve diðer elementler korunacaktýr.")]
    [SerializeField] Sprite boatSprite;

    [Tooltip("Bottan indiðinde oyuncunun normal insan halinin resmidir. Bu sayede oyuncunun sadece görseli deðiþmiþ olacak ve diðer elementler korunacaktýr.")]
    [SerializeField] Sprite characterSprite;

    [Header("Mechanic Details")]

    bool isInteractionDisabled;
    bool isInWater;

    [Tooltip("Açýklama bekleniyor...")]
    [SerializeField] GameObject waterEdges;

    [Tooltip("Oyuncunun botu sürüp sürmediðini kontrol eden boolean deðeridir.")]
    public bool onBoat;

    [Tooltip("Oyuncu bota bindiðinde sahip olacaðý hýz deðeridir.")]
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
            Debug.Log("Ýndi");
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
            Debug.Log("Suda Deðil");
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
        boatSpeed += 5; //hýz deðerleri deðiþtirilebilir
                        //bu tekne geliþtirmesidir hýzý artar 
                        //bu geliþtirme level alýndýðýnda level alýnan yere eklenmelidir
                        //daha kapasitesi arttýrýlacak ve eþya falan taþýmaya baþlayacaklar...

    }
    void ActiveInteraction()
    {
        isInteractionDisabled = false;
    }
}