using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class RogueLiteCharacter : MonoBehaviour
{
    GameObject[] guns;

    [Header("Main")]

    public float currentMoveSpeed = 100f;
    public float defaultMoveSpeed = 100f;

    Rigidbody2D rb;
    Vector2 movement;

    [Tooltip("oyuncunun caný")]
    public float  MaxHealthValue = 100f;
    public  float HealthValue = 100f;
    [Tooltip("oyuncunun zýrh seviyesi")]
    public  float ArmorValue;
    [Tooltip("oyuncunun zýrh seviyesi")]
    public  float AttackValue;
    private bool Isdead;

    Animator playerAnimator;

    [Header("Additional")]
    [Tooltip("oyuncunun etkileþime girebildiðini gösteren iaþrettir oyuncunun üstünde belirir")]
    public InventoryObject inventoryObject;
    public GameObject inventory;
    #region DASH_VARIABLES
    [Tooltip("Karakterin dash sýrasýndaki hareket hýzýdýr.")]
    [SerializeField] float dashSpeed = 500f;
    [Tooltip("Dashin süre deðeridir (saniye biçiminden).")]
    [SerializeField] float dashDuration = 0.2f;
    [Tooltip("Dashler arasý bekleme süresi/cooldowndur.")]
    [SerializeField] float dashCooldown = 2f;
    bool isDashing = false; // Karakterin dash hareketi yapýp yapmadýðýný kontrol eder.
    float dashTimer = 0f; //Dash hareketi süresini takip eder.
    float dashCooldownTimer = 0f; //Dash hareketleri arasýndaki bekleme süresini takip eder.
    #endregion

    [Header("Dungeon Talismans")]

    [Tooltip("Maksimum caný %25 arttýrýr")]
    public bool HealthTalisman;
    [Tooltip("Zýrh gücünü %50 arttýrýr.")]
    public bool ArmorTalisman;
    [Tooltip("Karakterin caný maksimum canýn %25’inden az olduðunda silahlarýn verdiði hasar %50 artar.")] //SÝLAH KODLARI GELÝNCEE ÝÇÝNE YAZILCAK
    public bool LastChangeTalisman;
    [Tooltip("Ölündüðünde eve 2 deðil 4 eþya götürmeyi saðlar.")] //ROGUE LÝTE ENVANTER KODLARI GELÝNCEE ÝÇÝNE YAZILCAK
    public bool BiggerBagTalisman;
    [Tooltip("Býçak sayýsý 1 artar.")] //SÝLAH KODLARI GELÝNCE ÝÇÝNE YAZILCAK
    public bool AssasinTalisman;
    [Tooltip("Ölmeden zindanlardan çýkýp Heart of Dignacia’ya gitmeyi saðlar. Böylece o ana dek toplanan tüm eþyalar kazanýlmýþ olur ancak geri dönüldüðünde zindana sýfýrdan baþlamak gerekir.")] //SONRADAN EKLENECEK 
    public bool EscapeTalisman;
    [Tooltip("Öldükten sonra %50 canla dirilmeyi saðlar.")] 
    public bool SoulControlTalisman;

    void Start()
    {
        //TILSIM KODLARI
        if (HealthTalisman == true)
        {
            MaxHealthValue = MaxHealthValue + MaxHealthValue % 25;
        }
        if (ArmorTalisman == true)
        {
            ArmorValue = ArmorValue + ArmorValue % 50;
        }
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        HealthValue = MaxHealthValue;
    }
    void Update()
    {
        //-------------------------Talismanlar---------------------------
        if (Isdead && SoulControlTalisman)
        {
            Isdead = false;
            HealthValue = MaxHealthValue % 50;
            SoulControlTalisman = false;
        }
        //-------------------------Talismanlar---------------------------

        #region CHARACTER_MOVEMENT
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButton(1))
        {
            playerAnimator.SetBool("IsBFAttack", true);
            playerAnimator.SetBool("IsSwordAttack", false);
            playerAnimator.SetBool("isWalking", false);
        }
        else
        {
            playerAnimator.SetBool("IsBFAttack", false);
        }
        if (Input.GetMouseButton(0))
        {
            playerAnimator.SetBool("IsSwordAttack", true);
            playerAnimator.SetBool("IsBFAttack", false);
            playerAnimator.SetBool("isWalking", false);
        }
        else
        {
            playerAnimator.SetBool("IsBFAttack", false);
            playerAnimator.SetBool("IsSwordAttack", false);
            playerAnimator.SetBool("isWalking", true);


        }

        if (movement.x != 0 || movement.y != 0)
        {
            playerAnimator.SetFloat("Horizontal", movement.x);
            playerAnimator.SetFloat("Vertical", movement.y);
            playerAnimator.SetBool("isWalking", true);
        }
        else
        {
            playerAnimator.SetBool("isWalking", false);
            StopMoving();
        }
        #endregion

        #region DASH_PART
        if (isDashing)
        {
            //Dash hareketi sýrasýnda hýzlan
            currentMoveSpeed = dashSpeed;
            //Dash hareketi süresini kontrol etme
            dashTimer += Time.deltaTime;
            if (dashTimer >= dashDuration)
            {
                isDashing = false;
                gameObject.layer = 0;  // 0: Default layer
                dashTimer = 0f;
            }
        }
        else
        {
            // normal hareket hýzýyla hareket etmesi için
            currentMoveSpeed = defaultMoveSpeed;
            // dash hareketi arasýndaki bekleme süresini kontrol etmesi içn
            if (dashCooldownTimer > 0f)
            {
                dashCooldownTimer -= Time.deltaTime;
            }
            else if (Input.GetButtonDown("Dash"))
            {
                isDashing = true;
                dashCooldownTimer = dashCooldown;
            }
        }
        #endregion

        #region ROGUE_LITE_PART
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            guns[0].SetActive(true);
            guns[1].SetActive(false);
            guns[2].SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            guns[0].SetActive(false);
            guns[1].SetActive(true);
            guns[2].SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            guns[0].SetActive(false);
            guns[1].SetActive(false);
            guns[2].SetActive(true);
        }
        #endregion

        #region SAVE_AND_LOAD_GAME_FOR_DEVELOPERS
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inventoryObject.Save(); // Envanteri kaydetme
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            inventoryObject.Load(); //Envanteri yükleme
        }
        #endregion

    }
    private void FixedUpdate()
    {
        Vector2 moveDir = movement.normalized;
        rb.velocity = moveDir * currentMoveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.B))
        {
            inventory.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            inventory.SetActive(false);
        }

    }
    void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }
}