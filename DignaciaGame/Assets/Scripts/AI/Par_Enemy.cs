using UnityEngine;

public class Par_Enemy : MonoBehaviour
{
    public GameObject player; // Oyuncu GameObject referans�
    public float minDistance = 5f; // Minimum uzakl�k
    public float maxDistance = 10f; // Maksimum uzakl�k
    public float stopDistance = 5f; // Durma mesafesi
    public float moveSpeed = 2f; // Hareket h�z�
    public float arrowleft = 5;
    [SerializeField] GameObject arrow;
    private bool isMoving = false; // Hareket durumu kontrol�
    private Vector2 moveDirection; // Hareket y�n�
    private bool arrowattacking;
    private CircleCollider2D ArrowAttackCollider; // Arrow Sald�r� collider'� referans�
    private BoxCollider2D NormalAttackCollider;
    public bool canAttack = true; //  Sald�r� izni
    public float attackCooldown = 3f; //  Sald�r� aral���
    public float attackTimer = 0f; //  Sald�r� zamanlay�c�s�

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

    private void OnTriggerEnter2D(Collider2D other)
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

    private void OnTriggerStay2D(Collider2D other)
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

    private void AttackPlayer()
    {
        // D��man�n sald�r� collider'�
        NormalAttackCollider.enabled = true;
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
        NormalAttackCollider.enabled = false;
    }

    private void ArrowAttackPlayer()
    {
        arrowattacking = true;
        Instantiate(arrow, transform.position, Quaternion.Euler(0, 0, 0));
        arrowleft --;
        Debug.Log("OK ATI�I!");
        arrowattacking = false;
    }


}


