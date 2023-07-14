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

    [Tooltip("oyuncunun can�")]
    public float  MaxHealthValue = 100f;
    public  float HealthValue = 100f;
    [Tooltip("oyuncunun z�rh seviyesi")]
    public  float ArmorValue;
    [Tooltip("oyuncunun z�rh seviyesi")]
    public  float AttackValue;
    private bool Isdead;

    Animator playerAnimator;

    [Header("Additional")]
    [Tooltip("oyuncunun etkile�ime girebildi�ini g�steren ia�rettir oyuncunun �st�nde belirir")]
    public InventoryObject inventoryObject;
    public GameObject inventory;
    #region DASH_VARIABLES
    [Tooltip("Karakterin dash s�ras�ndaki hareket h�z�d�r.")]
    [SerializeField] float dashSpeed = 500f;
    [Tooltip("Dashin s�re de�eridir (saniye bi�iminden).")]
    [SerializeField] float dashDuration = 0.2f;
    [Tooltip("Dashler aras� bekleme s�resi/cooldowndur.")]
    [SerializeField] float dashCooldown = 2f;
    bool isDashing = false; // Karakterin dash hareketi yap�p yapmad���n� kontrol eder.
    float dashTimer = 0f; //Dash hareketi s�resini takip eder.
    float dashCooldownTimer = 0f; //Dash hareketleri aras�ndaki bekleme s�resini takip eder.
    #endregion

    [Header("Dungeon Talismans")]

    [Tooltip("Maksimum can� %25 artt�r�r")]
    public bool HealthTalisman;
    [Tooltip("Z�rh g�c�n� %50 artt�r�r.")]
    public bool ArmorTalisman;
    [Tooltip("Karakterin can� maksimum can�n %25�inden az oldu�unda silahlar�n verdi�i hasar %50 artar.")] //S�LAH KODLARI GEL�NCEE ���NE YAZILCAK
    public bool LastChangeTalisman;
    [Tooltip("�l�nd���nde eve 2 de�il 4 e�ya g�t�rmeyi sa�lar.")] //ROGUE L�TE ENVANTER KODLARI GEL�NCEE ���NE YAZILCAK
    public bool BiggerBagTalisman;
    [Tooltip("B��ak say�s� 1 artar.")] //S�LAH KODLARI GEL�NCE ���NE YAZILCAK
    public bool AssasinTalisman;
    [Tooltip("�lmeden zindanlardan ��k�p Heart of Dignacia�ya gitmeyi sa�lar. B�ylece o ana dek toplanan t�m e�yalar kazan�lm�� olur ancak geri d�n�ld���nde zindana s�f�rdan ba�lamak gerekir.")] //SONRADAN EKLENECEK 
    public bool EscapeTalisman;
    [Tooltip("�ld�kten sonra %50 canla dirilmeyi sa�lar.")] 
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
            //Dash hareketi s�ras�nda h�zlan
            currentMoveSpeed = dashSpeed;
            //Dash hareketi s�resini kontrol etme
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
            // normal hareket h�z�yla hareket etmesi i�in
            currentMoveSpeed = defaultMoveSpeed;
            // dash hareketi aras�ndaki bekleme s�resini kontrol etmesi i�n
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
            inventoryObject.Load(); //Envanteri y�kleme
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