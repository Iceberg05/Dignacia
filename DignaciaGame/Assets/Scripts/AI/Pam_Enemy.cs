using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pam_Enemy : MonoBehaviour
{
    public GameObject player;
    [SerializeField] Transform target;
    public bool flip;
    public float speed;
    [SerializeField] GameObject spear;
    [SerializeField] bool HaveSpear = true;
    public float SpearAttackChance = 0.1f; // Saldýrý þansý (10'da 1)
    private bool spearattacking;
    private Transform playerPos;
    private Vector2 currenPos;
    public float distance;
    public bool canAttack = true; // yakýn Saldýrý izni
    public float attackCooldown = 3f; // yakýn Saldýrý aralýðý
    public float attackTimer = 0f; // yakýn Saldýrý zamanlayýcýsý
    private Collider2D attackCollider; // Saldýrý collider'ý referansý

    // Start is called before the first frame update
    void Start()
    {
        attackCollider = GetComponent<Collider2D>();
        playerPos = player.GetComponent<Transform>();
        currenPos = GetComponent<Transform>().position;
        attackCollider.enabled = true;
        StartCoroutine(SpearAttack());
    }

    // Update is called once per frame

    public IEnumerator SpearAttack()
    {
  if (HaveSpear  == true && Random.value < SpearAttackChance)
        {
            SpearAttackPlayer();
            Debug.Log("SPEAR ATTACK NUMERATOR ÇALIÞTI");         
        }
        else
        {
 Debug.Log("SPEAR ATTACK NUMERATOR ÇALIÞTI AMA SPEAR ATMADI");
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

        if (Vector2.Distance(transform.position, playerPos.position) < distance)
        {

            transform.position = Vector2.MoveTowards(transform.position, playerPos.position, speed * Time.deltaTime);

        }
        else
        {
            if (Vector2.Distance(transform.position, currenPos) <= 0)
            {

            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, currenPos, speed * Time.deltaTime);

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
        private void OnTriggerEnter2D(Collider2D other)
        {
        // Düþmanýn oyuncuya temas ettiðinde çalýþacak kod
        if (spearattacking == false)
        {
             if (other.CompareTag("Player") && canAttack)
            {
                AttackPlayer();
                canAttack = false; // Saldýrý yapýldýðýnda saldýrý iznini kapanýyor
                attackTimer = 0f; // Saldýrý yapýldýktan sonra zamanlayýcýyý sýfýrlanýyor
            }
        }
          
        }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Düþmanýn oyuncuya temas ettiðinde çalýþacak kod
        if (spearattacking == false)
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
        attackCollider.enabled = true;
        //Oyuncunun Caný Azalýr
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
    
    Debug.Log("ATTACK");
        attackCollider.enabled = false;
    }

    private void SpearAttackPlayer()
    {
        spearattacking = true;
        Instantiate(spear, transform.position, Quaternion.Euler(0, 0, 0));
        HaveSpear = false;
        Debug.Log("spear ATTACK");
        spearattacking = false;
    }


    }


