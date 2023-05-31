using UnityEngine;

public class Par_Enemy : MonoBehaviour
{
    public GameObject player; // Oyuncu GameObject referansý
    public float minDistance = 5f; // Minimum uzaklýk
    public float maxDistance = 10f; // Maksimum uzaklýk
    public float stopDistance = 5f; // Durma mesafesi
    public float moveSpeed = 2f; // Hareket hýzý
    public float arrowleft = 5;
    [SerializeField] GameObject arrow;
    private bool isMoving = false; // Hareket durumu kontrolü
    private Vector2 moveDirection; // Hareket yönü
    private bool arrowattacking;
    private CircleCollider2D ArrowAttackCollider; // Arrow Saldýrý collider'ý referansý
    private BoxCollider2D NormalAttackCollider;
    public bool canAttack = true; //  Saldýrý izni
    public float attackCooldown = 3f; //  Saldýrý aralýðý
    public float attackTimer = 0f; //  Saldýrý zamanlayýcýsý

    void Start()
    {
        ArrowAttackCollider = GetComponent<CircleCollider2D>();
        NormalAttackCollider = GetComponent<BoxCollider2D>();
        NormalAttackCollider.enabled = false;
        ArrowAttackCollider.enabled = true;
    }


    private void Update()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown)
        {
            canAttack = true;
            if (arrowleft == 0)
            {
                ArrowAttackCollider.enabled = false;
                NormalAttackCollider.enabled = true;
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

    private void OnTriggerEnter2D(Collider2D other)
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

    private void OnTriggerStay2D(Collider2D other)
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

    private void AttackPlayer()
    {
        // Düþmanýn saldýrý collider'ý
        NormalAttackCollider.enabled = true;
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
        NormalAttackCollider.enabled = false;
    }

    private void ArrowAttackPlayer()
    {
        arrowattacking = true;
        Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, 0));
        arrowleft --;
        Debug.Log("OK ATIÞI!");
        arrowattacking = false;
    }


}


