using UnityEngine;

public class BossController : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;
    public float specialAttackDelay;
    public float attackRange;
    public float attackDelay;
    public float attackDamage;
    public float specialAttackDamage;
    public GameObject player;
    public GameObject groundSlamEffect;
    public GameObject grabAttackEffect;
    public GameObject leechProjectile;
    public Transform groundSlamPoint;
    public Transform grabAttackPoint;
    public Transform specialAttackPoint;

    private bool isAttacking;
    private bool isGrabbing;
    private bool isPerformingSpecialAttack;
    private float nextAttackTime;
    private float nextSpecialAttackTime;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (!isAttacking && !isGrabbing && !isPerformingSpecialAttack)
        {
            MoveTowardsPlayer();
            RotateTowardsPlayer();

            if (Time.time > nextAttackTime)
            {
                Attack();
            }
            else if (Time.time > nextSpecialAttackTime)
            {
                PerformSpecialAttack();
            }
        }
    }
    private void PerformSpecialAttack()
    {
        // �zel sald�r�y� ger�ekle�tir
        isPerformingSpecialAttack = true;

        // G�ky�z�ne f�rlatma animasyonu burada oynat�labilir

        // Oyuncuya y�ksek hasar ver
        GetComponent<RogueLiteCharacter>().HealthValue = GetComponent<RogueLiteCharacter>().HealthValue - 30;

        // Sald�r� animasyonu tamamland���nda isPerformingSpecialAttack'i false yap
        // Animasyon s�resine ba�l� olarak isPerformingSpecialAttack'i ayarlay�n
        // �zel sald�r� gecikmesini ayarla
        nextSpecialAttackTime = Time.time + specialAttackDelay;
    }
    private void MoveTowardsPlayer()
    {
        // Boss'un oyuncuya do�ru hareket etmesi
        Vector3 direction = player.transform.position - transform.position;
        transform.Translate(direction.normalized * movementSpeed * Time.deltaTime, Space.World);
    }

    private void RotateTowardsPlayer()
    {
        // Boss'un oyuncuya do�ru d�nmesi
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        // Normal sald�r�y� ger�ekle�tir
        isAttacking = true;

        // Bacaklar�yla yere vurma ve d�nme animasyonlar� burada oynat�labilir

        // Yerdeki oyuncuya hasar ver
        RaycastHit2D[] hits = Physics2D.CircleCastAll(groundSlamPoint.position, attackRange, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Player"))
            {
                GetComponent<RogueLiteCharacter>().HealthValue = GetComponent<RogueLiteCharacter>().HealthValue - 12;
            }
        }

        // Sald�r� gecikmesini ayarla
        nextAttackTime = Time.time + attackDelay;

        // Sald�r� animasyonu tamamland���nda isAttacking'i false yap
        // Animasyon s�resine ba�l� olarak isAttacking'i ayarlay�n
    }

    private void PerformGrabAttack()
    {
        // Kapma sald�r�s�n� ger�ekle�tir
        isGrabbing = true;

        // Kapma sald�r�s� animasyonu burada oynat�labilir

        // Oyuncuyu tutma ve hasar verme efektlerini olu�tur

        // Oyuncuya hasar ver ve f�rlat
        GetComponent<RogueLiteCharacter>().HealthValue = GetComponent<RogueLiteCharacter>().HealthValue - 30;
        player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10f, ForceMode2D.Impulse);

        // Sald�r� animasyonu tamamland���nda isGrabbing'i false yap
        // Animasyon s�resine

    } }