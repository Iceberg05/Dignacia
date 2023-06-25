using System.Collections;
using UnityEngine;

public class Pam_Enemy : MonoBehaviour
{
    [Header("Main")]
    [Space]

    Transform player;
    bool spearAttacking;
    Vector2 currentPos;
    [SerializeField] bool flip;
    [SerializeField] float speed;

    [Header("Spear Attack")]
    [Space]

    [SerializeField] GameObject spear;
    [SerializeField] bool haveSpear = true;

    [Tooltip("Sald�r� �ans�n�n de�eridir.")]
    [SerializeField] float spearAttackChance = 0.1f;
    [SerializeField] float distance;

    [Header("Melee Attack")]

    [Tooltip("Yak�n sald�r� yap�p yapamayaca��n�n de�eridir.")]
    [SerializeField] bool canAttack = true;
    [Tooltip("Yak�n sald�r�lar aras� bekleme s�residir.")]
    [SerializeField] float attackCooldown = 3f;
    [Tooltip("Yak�n sald�r� cooldown kontrol zamanlay�c�s�d�r.")]
    [SerializeField] float attackTimer = 0f;

    Collider2D attackCollider; // Sald�r� collider'� referans�

    void Start()
    {
        player = FindObjectOfType<Character>().transform;
        attackCollider = GetComponent<Collider2D>();
        currentPos = GetComponent<Transform>().position;
        attackCollider.enabled = true;
        StartCoroutine(SpearAttack());
    }
    IEnumerator SpearAttack()
    {
        if (haveSpear && Random.value < spearAttackChance)
        {
            SpearAttackPlayer();
            Debug.Log("SPEAR ATTACK NUMERATOR �ALI�TI");         
        }
        else
        {
            Debug.Log("SPEAR ATTACK NUMERATOR �ALI�TI AMA SPEAR ATMADI");
            yield return new WaitForSeconds(5);
            StartCoroutine(SpearAttack());
        }
    }
    private void Update()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackCooldown)
        {
            canAttack = true;
            attackCollider.enabled = true;
        }
        if (Vector2.Distance(transform.position, player.position) < distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else
        {
            if (!(Vector2.Distance(transform.position, currentPos) <= 0))
            {
                transform.position = Vector2.MoveTowards(transform.position, currentPos, speed * Time.deltaTime);
            }
        }
        Vector3 scale = transform.localScale;
        if (player.transform.position.x > transform.position.x)
        {
            scale.x = Mathf.Abs(scale.x) * -1 * (flip ? -1 : 1);
            transform.Translate(x: speed * Time.deltaTime, y: 0, z: 0);
        }
        else
        {
            scale.x = Mathf.Abs(scale.x) * (flip ? -1 : 1);
            transform.Translate(x: speed * Time.deltaTime * -1, y: 0, z: 0);
        }
        transform.localScale = scale;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // D��man�n oyuncuya temas etti�inde �al��acak kod
        if (!spearAttacking)
        {
             if (other.CompareTag("Player") && canAttack)
            {
                AttackPlayer();
                canAttack = false; // Sald�r� yap�ld���nda sald�r� iznini kapan�yor
                attackTimer = 0f; // Sald�r� yap�ld�ktan sonra zamanlay�c�y� s�f�rlan�yor
            }
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        // D��man�n oyuncuya temas etti�inde �al��acak kod
        if (!spearAttacking)
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
        attackCollider.enabled = true;
        //Oyuncunun Can� Azal�r
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
    
        Debug.Log("Attack");
        attackCollider.enabled = false;
    }
    void SpearAttackPlayer()
    {
        spearAttacking = true;
        Instantiate(spear, transform.position, Quaternion.Euler(0, 0, 0));
        haveSpear = false;
        Debug.Log("Spear Attack");
        spearAttacking = false;
    }
}


