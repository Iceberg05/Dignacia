using UnityEngine;

public class Par_Enemy : MonoBehaviour
{
    [Header("Main")]
    [Space]

    Transform player;
    Vector2 moveDirection;
    bool isMoving = false;

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

    [Tooltip("Yapay zekanýn saldýrýp saldýramayacaðýný kontrol eder.")]
    [SerializeField] bool canAttack = true;
    [Tooltip("Saldýrýlar arasý bekleme süresidir.")]
    [SerializeField] float attackCooldown = 3f;
    [Tooltip("Saldýrýlar arasý bekleme süresinin zamanlayýcýsýdýr.")]
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
        // Düþmanýn oyuncuya temas ettiðinde çalýþacak kod
        if (arrowleft > 0 && canAttack)
        {
            ArrowAttackPlayer();
            canAttack = false; // Saldýrý yapýldýðýnda saldýrý iznini kapanýyor
            attackTimer = 0f; // Saldýrý yapýldýktan sonra zamanlayýcýyý sýfýrlanýyor
            Debug.Log("OK ATIÞI!");
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (arrowleft > 0)
        {
            if (other.CompareTag("Player") && canAttack)
            {
                ArrowAttackPlayer();
                canAttack = false; // Saldýrý yapýldýðýnda saldýrý iznini kapanýyor
                attackTimer = 0f; // Saldýrý yapýldýktan sonra zamanlayýcýyý sýfýrlanýyor
            }
        } // Düþmanýn oyuncuya temas ettiðinde çalýþacak kod
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
        Debug.Log("OK ATIÞI!");
    }
}


