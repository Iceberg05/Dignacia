using UnityEngine;
using TMPro;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Character : MonoBehaviour
{
    GameObject[] guns;

    [Header("Main")]

    public float currentMoveSpeed = 100f;
    public float defaultMoveSpeed = 100f;

    Rigidbody2D rb;
    Vector2 movement;

    Animator playerAnimator;

    [Header("Additional")]

    public int moneyValue;
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

    [Header("Prosthetic Arm")]

    public string modeName;
    bool isChanging = false;
    public int currentModeNumber;

    [Header("User Interface Part")]

    [SerializeField] GameObject changePanel;

    [SerializeField] TMP_Text moneyText;
    void Start()
    {
        //currentModeNumber = 0;
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        //moneyText.text = moneyValue.ToString();

        #region CHARACTER_MOVEMENT
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if(Input.GetMouseButton(1))
        {
            playerAnimator.SetBool("IsBFAttack" , true);
            playerAnimator.SetBool("IsSwordAttack" , false);
            playerAnimator.SetBool("isWalking" , false);
           
        }
        else
        {
            playerAnimator.SetBool("IsBFAttack" , false);
        }
        
        

        
        if(Input.GetMouseButton(0))
        {
            playerAnimator.SetBool("IsSwordAttack" , true);
            playerAnimator.SetBool("IsBFAttack" , false);
            playerAnimator.SetBool("isWalking" , false);
        }
        else
        {
            playerAnimator.SetBool("IsBFAttack" , false);
            playerAnimator.SetBool("IsSwordAttack" , false);
            playerAnimator.SetBool("isWalking" , true);

           
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

        #region PROSTHETIC_ARM_CHANGING
        if (Input.GetButtonDown("OpenChangePanel"))
        {
            isChanging = !isChanging;
        }
        if (isChanging)
        {
            changePanel.SetActive(true);
            //Envanter kodunda slotlar aras� item ge�i� butonlar�n�n �al��mas�n� kontrol eden boolean� "false" yap
            if (Input.GetButtonDown("Next"))
            {
                if (currentModeNumber == 3)
                {
                    currentModeNumber = 0;
                }
                else
                {
                    currentModeNumber++;
                }
            }
            else if (Input.GetButtonDown("Previous"))
            {
                if (currentModeNumber == 0)
                {
                    currentModeNumber = 3;
                }
                else
                {
                    currentModeNumber--;
                }
            }
        }
        else
        {
            changePanel.SetActive(false);
            //Envanter kodunda slotlar aras� item ge�i� butonlar�n�n �al��mas�n� kontrol eden boolean� "true" yap
        }
        switch (currentModeNumber)
        {
            case 0: modeName = "Null"; break;
            case 1: modeName = "Farming"; break;
            case 2: modeName = "Building"; break;
            case 3: modeName = "Fighting"; break;
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
    private void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -20);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Shop")
        {
            if (Input.GetButtonDown("Interact"))
            {

            }
        }
        if (col.gameObject.tag == "LaserAmmo")
        {
            LaserShooting.laserAmmo = LaserShooting.laserAmmo + 10;
        }
        if (col.gameObject.tag == "MagnumAmmo")
        {
            MagnumShooting.magnumAmmo = MagnumShooting.magnumAmmo + 10;
        }
        if (col.gameObject.tag == "MKSAmmo")
        {
            MKSShooting.MKSAmmo = MKSShooting.MKSAmmo + 10;
        }

        //Envanter k�sm�
        var item = col.GetComponent<GroundItem>();
        if (col.gameObject.tag == "Item")
        {
            Item _item = new Item(item.item);
            Debug.Log(_item.Id);
            inventoryObject.AddItem(_item, 1);
            Destroy(col.gameObject);
        }
    }
    //Oyuncu ellerini tu�lardan �ekti�i zaman karakterin durmas�n� sa�layan fonksiyon
    void StopMoving()
    {
        rb.velocity = Vector3.zero;
    }
    private void OnApplicationQuit()
    {
        inventoryObject.Container.Items = new InventorySlot[12];
    }
    //Protez kol men�s�nde oyuncunun butonlardan se�ebilmesini sa�layan fonksiyon
    public void ChangeProstheticArmMode(int modeInt)
    {
        currentModeNumber = modeInt;
    }
}
