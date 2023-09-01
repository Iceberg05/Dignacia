using System.Collections;
using UnityEngine;

public class Par_Enemy : MonoBehaviour
{
    [Header("Main")]
    [Space]
    public float health = 100;
    [SerializeField] float knockbackDuration = 0.2f; // Knockback süresi

    public float knockbackTimer = 0f;  // kýsaca düþman knockback yediðinde  knockbackDuration deðerine gelince düþmaný durduruyor,knockbackDuration deðeri uzatýlýp knockback uzaklýðý deðiþtirilebilir
    bool isKnockedBack = false;  
    Transform player;
    Vector2 moveDirection;
    bool isMoving = false;
    public CameraShake cameraShake;
    [SerializeField] float minDistance = 5f; // Minimum uzaklýk
    [SerializeField] float maxDistance = 10f; // Maksimum uzaklýk
    [SerializeField] float stopDistance = 5f; // Durma mesafesi
    [SerializeField] float moveSpeed = 2f; // Hareket hýzý

    [Header("Arrow Attack")]
    [Space]

    [Tooltip("Okun objesidir.")]
    [SerializeField] GameObject arrow;

    [Tooltip("Kalan ok sayýsýdýr.")]
    [SerializeField] float arrowleft = 5;

    CircleCollider2D arrowAttackCollider;
    
    [Header("Melee Attack")]
    [Space]

    BoxCollider2D meleeAttackCollider;
    Rigidbody2D rb;
    CapsuleCollider2D takeDamageCollider;

    [Tooltip("Yapay zekanýn saldýrýp saldýramayacaðýný kontrol eder.")]
    [SerializeField] bool canAttack = true;
    [Tooltip("Saldýrýlar arasý bekleme süresidir.")]
    [SerializeField] float attackCooldown = 3f;
    [Tooltip("Saldýrýlar arasý bekleme süresinin zamanlayýcýsýdýr.")]
    [SerializeField] float attackTimer = 0f;

    SpriteRenderer spriteRenderer;

    [SerializeField] Color damageColor = new Color(1f, 0.5f, 0.5f, 1f); // Kýrmýzýmsý renk
    [SerializeField] float flashDuration = 0.3f; // Flash süresi

    bool isFlashing = false;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = FindObjectOfType<RogueLiteCharacter>().transform;
        cameraShake = GameObject.Find("PlayerCamera").GetComponent<CameraShake>();
        takeDamageCollider = GetComponent<CapsuleCollider2D>();
        arrowAttackCollider = GetComponent<CircleCollider2D>();
        meleeAttackCollider = GetComponent<BoxCollider2D>();
        meleeAttackCollider.enabled = false;
        arrowAttackCollider.enabled = true;
    }
    void Update()
    {
        if (isKnockedBack)
        {
            knockbackTimer += Time.deltaTime;
            if (knockbackTimer >= knockbackDuration)
            {
                isKnockedBack = false;
                knockbackTimer = 0f;

                // Knockback bittiðinde düþmanýn hýzýný azalt
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.velocity = rb.velocity * 0f; // Hýzý yavaþlat, istediðiniz faktörü ayarlayabilirsiniz
            }
        }

        if (health < 1)
        {
            Destroy(gameObject);
        }
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown)
        {
            canAttack = true;
            if (arrowleft == 0)
            {
                arrowAttackCollider.enabled = false;
                meleeAttackCollider.enabled = true;
            }
        }
        if (arrowleft > 0)
        {  
            if (isMoving == true)
            {
                // Düþmanýn oyuncudan mevcut uzaklýðý
                float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

                // Hareket yönü (düþmanýn oyuncudan uzaklaþmasý için ters yönde)
                moveDirection = (transform.position - player.transform.position).normalized;

                // Hedef pozisyonu 
                float targetDistance = Mathf.Clamp(distanceToPlayer, minDistance, maxDistance);
                Vector2 targetPosition = (Vector2)transform.position + (moveDirection * targetDistance);

                // Düþmanýn hedef pozisyona doðru hareketi
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                // Hedef mesafesine ulaþýldýðýnda hareketi durmasý
                if (distanceToPlayer <= stopDistance)
                {
                    isMoving = false;
                }
            }
            else
            {
                float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

                if (distanceToPlayer < stopDistance)
                {
                    isMoving = true;
                }
            }
        }
        if (arrowleft == 0)
        {
            if (Vector2.Distance(transform.position, player.transform.position) < maxDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (arrowleft == 0)
        {
            if (other.CompareTag("Player") && canAttack)
            {
                AttackPlayer();
                canAttack = false; // Saldýrý yapýldýðýnda saldýrý iznini kapanýyor
                attackTimer = 0f; // Saldýrý yapýldýktan sonra zamanlayýcýyý sýfýrlanýyor
            }
        }

        // Düþmanýn oyuncuya temas ettiðinde çalýþacak kod ok atma kodu
        if ((arrowleft > 0) && (canAttack))
        {
            ArrowAttackPlayer();
            canAttack = false; // Saldýrý yapýldýðýnda saldýrý iznini kapanýyor
            attackTimer = 0f; // Saldýrý yapýldýktan sonra zamanlayýcýyý sýfýrlanýyor
            Debug.Log("OK ATIÞI!");
        }
        if (other.CompareTag("Bullet"))
        {
            // burda mermideki kapsül kolider açýksa eðer mermi deðdiði zaman hasar yiyoruz "Bullet_Damag" isimli kodda düþmana deðdiði an açýlmasýný saðlýyoruz
            if (other is CapsuleCollider2D)
            {
                RogueLiteCharacter character = player.GetComponent<RogueLiteCharacter>();
                TakeDamage(other);

                // oyuncu pozisyonuna göre knockback yönünü hesaplama
                Vector2 knockbackDirection = (transform.position - player.transform.position).normalized;

                // Düþmana fizik tabanlý knockback uygulama yeri, knockback gücünü RoguelikeCharacter scriptinden çekiyoruz
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.AddForce(knockbackDirection * character.KnockBackValue , ForceMode2D.Impulse);


                // Knockback baþladýðýnda ayarlarý güncelle
                isKnockedBack = true;
                knockbackTimer = 0f;

                Destroy(other.gameObject);
            }
        }
    }



    void OnTriggerStay2D(Collider2D other)
    {

        // Düþmanýn oyuncuya temas ettiðinde çalýþacak kod
        if ((arrowleft > 0) && (canAttack))
        {
            ArrowAttackPlayer();
            canAttack = false; // Saldýrý yapýldýðýnda saldýrý iznini kapanýyor
            attackTimer = 0f; // Saldýrý yapýldýktan sonra zamanlayýcýyý sýfýrlanýyor
            Debug.Log("OK ATIÞI!");
        }

       // Düþmanýn oyuncuya temas ettiðinde çalýþacak kod
        if ((arrowleft == 0))
        {
            if (other.CompareTag("Player") && canAttack)
            {
                AttackPlayer();
                canAttack = false; // Saldýrý yapýldýðýnda saldýrý iznini kapanýyor
                attackTimer = 0f; // Saldýrý yapýldýktan sonra zamanlayýcýyý sýfýrlanýyor
            }
        }
 
    
    
    }
    void AttackPlayer()
    {
        // Düþmanýn saldýrý collider'ý
        meleeAttackCollider.enabled = true;
        //Bu kod Hasar Veren Objelere Eklenebilir Deðerler Deðiþkenlik Gösterebilir
        RogueLiteCharacter character = player.GetComponent<RogueLiteCharacter>();
        float armorValue = character.ArmorValue;

        switch (true)
        {
            case var n when armorValue >= 100:
                character.HealthValue -= 2;
                cameraShake.ShakeCamera(0.2f);
                break;
            case var n when armorValue >= 75:
                character.HealthValue -= 3;
                cameraShake.ShakeCamera(0.2f);
                break;
            case var n when armorValue >= 50:
                character.HealthValue -= 4;
                cameraShake.ShakeCamera(0.2f);
                break;
            case var n when armorValue >= 25:
                character.HealthValue -= 5;
                cameraShake.ShakeCamera(0.2f);
                break;
            default:
                character.HealthValue -= 6;
                cameraShake.ShakeCamera(0.2f);
                break;
        }
       
        Debug.Log("PAR ENEMY ATTACK");
        meleeAttackCollider.enabled = false;

    }

    void TakeDamage(Collider2D bulletCollider)
    {
        //alýnan hasarý temsil eder attackvalue oyuncunun hasarýný temsil eder hasar artýnca azalan can miktarý artmaktadýr
        RogueLiteCharacter character = player.GetComponent<RogueLiteCharacter>();
        float AttackValue = character.AttackValue;

        switch (true)
        {
            case var n when AttackValue >= 5:
                health -= 5;
                cameraShake.ShakeCamera(0.1f);
                break;
            case var n when AttackValue >= 4:
                health -= 4;
                cameraShake.ShakeCamera(0.1f);
                break;
            case var n when AttackValue >= 3:
                health -= 3;
                cameraShake.ShakeCamera(0.1f);
                break;
            case var n when AttackValue >= 2:
                health -= 2;
                cameraShake.ShakeCamera(0.1f);
                break;
            default:
                health -= 1;
                cameraShake.ShakeCamera(0.1f);
                break;
        }

        // Sprite'ý kýrmýzýmsý yap ve sonra normale döndür
        if (!isFlashing)
        {
            StartCoroutine(FlashSprite());
        }

    }
    IEnumerator FlashSprite()
    {
        isFlashing = true;

        // Kýrmýzýmsý yap
        spriteRenderer.color = damageColor;

        yield return new WaitForSeconds(flashDuration);

        // Normale döndür
        spriteRenderer.color = Color.white;

        isFlashing = false;
    }
    void ArrowAttackPlayer()
    {
        Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, 0));
        arrowleft --;
        Debug.Log("OK ATIÞI!");
    }
}


