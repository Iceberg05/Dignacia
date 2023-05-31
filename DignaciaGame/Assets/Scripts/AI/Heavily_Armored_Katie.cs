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
        // Özel saldýrýyý gerçekleþtir
        isPerformingSpecialAttack = true;

        // Gökyüzüne fýrlatma animasyonu burada oynatýlabilir

        // Oyuncuya yüksek hasar ver
        GetComponent<RogueLiteCharacter>().HealthValue = GetComponent<RogueLiteCharacter>().HealthValue - 30;

        // Saldýrý animasyonu tamamlandýðýnda isPerformingSpecialAttack'i false yap
        // Animasyon süresine baðlý olarak isPerformingSpecialAttack'i ayarlayýn
        // Özel saldýrý gecikmesini ayarla
        nextSpecialAttackTime = Time.time + specialAttackDelay;
    }
    private void MoveTowardsPlayer()
    {
        // Boss'un oyuncuya doðru hareket etmesi
        Vector3 direction = player.transform.position - transform.position;
        transform.Translate(direction.normalized * movementSpeed * Time.deltaTime, Space.World);
    }

    private void RotateTowardsPlayer()
    {
        // Boss'un oyuncuya doðru dönmesi
        Vector3 direction = player.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        // Normal saldýrýyý gerçekleþtir
        isAttacking = true;

        // Bacaklarýyla yere vurma ve dönme animasyonlarý burada oynatýlabilir

        // Yerdeki oyuncuya hasar ver
        RaycastHit2D[] hits = Physics2D.CircleCastAll(groundSlamPoint.position, attackRange, Vector2.zero);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Player"))
            {
                GetComponent<RogueLiteCharacter>().HealthValue = GetComponent<RogueLiteCharacter>().HealthValue - 12;
            }
        }

        // Saldýrý gecikmesini ayarla
        nextAttackTime = Time.time + attackDelay;

        // Saldýrý animasyonu tamamlandýðýnda isAttacking'i false yap
        // Animasyon süresine baðlý olarak isAttacking'i ayarlayýn
    }

    private void PerformGrabAttack()
    {
        // Kapma saldýrýsýný gerçekleþtir
        isGrabbing = true;

        // Kapma saldýrýsý animasyonu burada oynatýlabilir

        // Oyuncuyu tutma ve hasar verme efektlerini oluþtur

        // Oyuncuya hasar ver ve fýrlat
        GetComponent<RogueLiteCharacter>().HealthValue = GetComponent<RogueLiteCharacter>().HealthValue - 30;
        player.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 10f, ForceMode2D.Impulse);

        // Saldýrý animasyonu tamamlandýðýnda isGrabbing'i false yap
        // Animasyon süresine

    } }