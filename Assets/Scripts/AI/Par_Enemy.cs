using System.Collections;
using UnityEngine;

public class Par_Enemy : MonoBehaviour
{
    [Header("Main")]
    [Space]
    public float health = 100;
    [SerializeField] float knockbackDuration = 0.2f; // Knockback s�resi

    public float knockbackTimer = 0f;  // k�saca d��man knockback yedi�inde  knockbackDuration de�erine gelince d��man� durduruyor,knockbackDuration de�eri uzat�l�p knockback uzakl��� de�i�tirilebilir
    bool isKnockedBack = false;  
    Transform player;
    Vector2 moveDirection;
    bool isMoving = false;
    public CameraShake cameraShake;
    [SerializeField] float minDistance = 5f; // Minimum uzakl�k
    [SerializeField] float maxDistance = 10f; // Maksimum uzakl�k
    [SerializeField] float stopDistance = 5f; // Durma mesafesi
    [SerializeField] float moveSpeed = 2f; // Hareket h�z�

    [Header("Arrow Attack")]
    [Space]

    [Tooltip("Okun objesidir.")]
    [SerializeField] GameObject arrow;

    [Tooltip("Kalan ok say�s�d�r.")]
    [SerializeField] float arrowleft = 5;

    CircleCollider2D arrowAttackCollider;
    
    [Header("Melee Attack")]
    [Space]

    BoxCollider2D meleeAttackCollider;
    Rigidbody2D rb;
    CapsuleCollider2D takeDamageCollider;

    [Tooltip("Yapay zekan�n sald�r�p sald�ramayaca��n� kontrol eder.")]
    [SerializeField] bool canAttack = true;
    [Tooltip("Sald�r�lar aras� bekleme s�residir.")]
    [SerializeField] float attackCooldown = 3f;
    [Tooltip("Sald�r�lar aras� bekleme s�resinin zamanlay�c�s�d�r.")]
    [SerializeField] float attackTimer = 0f;

    SpriteRenderer spriteRenderer;

    [SerializeField] Color damageColor = new Color(1f, 0.5f, 0.5f, 1f); // K�rm�z�ms� renk
    [SerializeField] float flashDuration = 0.3f; // Flash s�resi

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

                // Knockback bitti�inde d��man�n h�z�n� azalt
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.velocity = rb.velocity * 0f; // H�z� yava�lat, istedi�iniz fakt�r� ayarlayabilirsiniz
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
                // D��man�n oyuncudan mevcut uzakl���
                float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

                // Hareket y�n� (d��man�n oyuncudan uzakla�mas� i�in ters y�nde)
                moveDirection = (transform.position - player.transform.position).normalized;

                // Hedef pozisyonu 
                float targetDistance = Mathf.Clamp(distanceToPlayer, minDistance, maxDistance);
                Vector2 targetPosition = (Vector2)transform.position + (moveDirection * targetDistance);

                // D��man�n hedef pozisyona do�ru hareketi
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                // Hedef mesafesine ula��ld���nda hareketi durmas�
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
                canAttack = false; // Sald�r� yap�ld���nda sald�r� iznini kapan�yor
                attackTimer = 0f; // Sald�r� yap�ld�ktan sonra zamanlay�c�y� s�f�rlan�yor
            }
        }

        // D��man�n oyuncuya temas etti�inde �al��acak kod ok atma kodu
        if ((arrowleft > 0) && (canAttack))
        {
            ArrowAttackPlayer();
            canAttack = false; // Sald�r� yap�ld���nda sald�r� iznini kapan�yor
            attackTimer = 0f; // Sald�r� yap�ld�ktan sonra zamanlay�c�y� s�f�rlan�yor
            Debug.Log("OK ATI�I!");
        }
        if (other.CompareTag("Bullet"))
        {
            // burda mermideki kaps�l kolider a��ksa e�er mermi de�di�i zaman hasar yiyoruz "Bullet_Damag" isimli kodda d��mana de�di�i an a��lmas�n� sa�l�yoruz
            if (other is CapsuleCollider2D)
            {
                RogueLiteCharacter character = player.GetComponent<RogueLiteCharacter>();
                TakeDamage(other);

                // oyuncu pozisyonuna g�re knockback y�n�n� hesaplama
                Vector2 knockbackDirection = (transform.position - player.transform.position).normalized;

                // D��mana fizik tabanl� knockback uygulama yeri, knockback g�c�n� RoguelikeCharacter scriptinden �ekiyoruz
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.AddForce(knockbackDirection * character.KnockBackValue , ForceMode2D.Impulse);


                // Knockback ba�lad���nda ayarlar� g�ncelle
                isKnockedBack = true;
                knockbackTimer = 0f;

                Destroy(other.gameObject);
            }
        }
    }



    void OnTriggerStay2D(Collider2D other)
    {

        // D��man�n oyuncuya temas etti�inde �al��acak kod
        if ((arrowleft > 0) && (canAttack))
        {
            ArrowAttackPlayer();
            canAttack = false; // Sald�r� yap�ld���nda sald�r� iznini kapan�yor
            attackTimer = 0f; // Sald�r� yap�ld�ktan sonra zamanlay�c�y� s�f�rlan�yor
            Debug.Log("OK ATI�I!");
        }

       // D��man�n oyuncuya temas etti�inde �al��acak kod
        if ((arrowleft == 0))
        {
            if (other.CompareTag("Player") && canAttack)
            {
                AttackPlayer();
                canAttack = false; // Sald�r� yap�ld���nda sald�r� iznini kapan�yor
                attackTimer = 0f; // Sald�r� yap�ld�ktan sonra zamanlay�c�y� s�f�rlan�yor
            }
        }
 
    
    
    }
    void AttackPlayer()
    {
        // D��man�n sald�r� collider'�
        meleeAttackCollider.enabled = true;
        //Bu kod Hasar Veren Objelere Eklenebilir De�erler De�i�kenlik G�sterebilir
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
        //al�nan hasar� temsil eder attackvalue oyuncunun hasar�n� temsil eder hasar art�nca azalan can miktar� artmaktad�r
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

        // Sprite'� k�rm�z�ms� yap ve sonra normale d�nd�r
        if (!isFlashing)
        {
            StartCoroutine(FlashSprite());
        }

    }
    IEnumerator FlashSprite()
    {
        isFlashing = true;

        // K�rm�z�ms� yap
        spriteRenderer.color = damageColor;

        yield return new WaitForSeconds(flashDuration);

        // Normale d�nd�r
        spriteRenderer.color = Color.white;

        isFlashing = false;
    }
    void ArrowAttackPlayer()
    {
        Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, 0));
        arrowleft --;
        Debug.Log("OK ATI�I!");
    }
}


