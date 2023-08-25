using UnityEngine;

public class Par_Enemy : MonoBehaviour
{
    [Header("Main")]
    [Space]

    Transform player;
    Vector2 moveDirection;
    bool isMoving = false;

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

    [Tooltip("Yapay zekan�n sald�r�p sald�ramayaca��n� kontrol eder.")]
    [SerializeField] bool canAttack = true;
    [Tooltip("Sald�r�lar aras� bekleme s�residir.")]
    [SerializeField] float attackCooldown = 3f;
    [Tooltip("Sald�r�lar aras� bekleme s�resinin zamanlay�c�s�d�r.")]
    [SerializeField] float attackTimer = 0f;

    void Start()
    {
        player = FindObjectOfType<Character>().transform;

        arrowAttackCollider = GetComponent<CircleCollider2D>();
        meleeAttackCollider = GetComponent<BoxCollider2D>();
        meleeAttackCollider.enabled = false;
        arrowAttackCollider.enabled = true;
    }
    void Update()
    {
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
        // D��man�n oyuncuya temas etti�inde �al��acak kod
        if (arrowleft > 0 && canAttack)
        {
            ArrowAttackPlayer();
            canAttack = false; // Sald�r� yap�ld���nda sald�r� iznini kapan�yor
            attackTimer = 0f; // Sald�r� yap�ld�ktan sonra zamanlay�c�y� s�f�rlan�yor
            Debug.Log("OK ATI�I!");
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (arrowleft > 0)
        {
            if (other.CompareTag("Player") && canAttack)
            {
                ArrowAttackPlayer();
                canAttack = false; // Sald�r� yap�ld���nda sald�r� iznini kapan�yor
                attackTimer = 0f; // Sald�r� yap�ld�ktan sonra zamanlay�c�y� s�f�rlan�yor
            }
        } // D��man�n oyuncuya temas etti�inde �al��acak kod
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
        switch (GetComponent<RogueLiteCharacter>().ArmorValue)
        {
            case float n when n >= 25:
                GetComponent<RogueLiteCharacter>().HealthValue = GetComponent<RogueLiteCharacter>().HealthValue - 5;
                break;
            case float n when n >= 50:
                GetComponent<RogueLiteCharacter>().HealthValue = GetComponent<RogueLiteCharacter>().HealthValue - 4;
                break;
            case float n when n >= 75:
                GetComponent<RogueLiteCharacter>().HealthValue = GetComponent<RogueLiteCharacter>().HealthValue - 3;
                break;
            case float n when n >= 100:
                GetComponent<RogueLiteCharacter>().HealthValue = GetComponent<RogueLiteCharacter>().HealthValue - 2;
                break;
            default:
                GetComponent<RogueLiteCharacter>().HealthValue = GetComponent<RogueLiteCharacter>().HealthValue - 6;
                break;
        }
        Debug.Log("PAR ENEMY ATTACK");
        meleeAttackCollider.enabled = false;
    }
    void ArrowAttackPlayer()
    {
        Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, 0));
        arrowleft --;
        Debug.Log("OK ATI�I!");
    }
}


