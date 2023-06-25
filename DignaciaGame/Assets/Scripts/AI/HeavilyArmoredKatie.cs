using UnityEngine;

public class HeavilyArmoredKatie : MonoBehaviour
{

    [Header("Main")]
    [Space]

    Transform player;

    [Tooltip("Normal saldýrý için kullanýlacak transform noktasýdýr.")]
    [SerializeField] Transform attackPoint;
    [Tooltip("Projectile saldýrýsý için kullanýlacak prefabdir.")]
    [SerializeField] GameObject projectilePrefab;

    [Header("Colliders")]
    [Space]

    [Tooltip("Normal saldýrý için kullanýlacak colliderdýr.")]
    [SerializeField] Collider normalAttackCollider;
    [Tooltip("Dönen saldýrý için kullanýlacak colliderdýr.")]
    [SerializeField] Collider spinAttackCollider;
    [Tooltip("Kapma saldýrýsý için kullanýlacak colliderdýr.")]
    [SerializeField] Collider grabAttackCollider;
    [Tooltip("Özel saldýrý için kullanýlacak colliderdýr.")]
    [SerializeField] Collider specialAttackCollider;

    [Header("Values")]
    [Space]

    [Tooltip("Bossun can miktarýdýr.")]
    [SerializeField] float health;
    [Tooltip("Bossun hareket hýzýdýr.")]
    [SerializeField] float movementSpeed = 2f;
    [Tooltip("Normal saldýrý mesafesidir.")]
    [SerializeField] float attackRadius = 2f;
    [Tooltip("Dönme saldýrýsý süresidir.")]
    [SerializeField] float spinAttackDuration = 3f;
    [Tooltip("Kapma saldýrýsý hasarýdýr.")]
    [SerializeField] float grabAttackDamage = 10f;
    [Tooltip("Kapma saldýrýsýnda uygulanan kuvvet miktarýdýr.")]
    [SerializeField] float grabAttackForce = 10f;
    [Tooltip("Normal saldýrý hasarýdýr.")]
    [SerializeField] float specialAttackDamage = 100f;

    [Header("Attack Control")]
    [Space]

    bool isAttacking = false; // Normal saldýrý durumu
    bool isSpinning = false; // Dönme saldýrýsý durumu
    float timeSinceLastAttack = 0f; // Son saldýrýdan bu yana geçen süre
    float attackCooldown = 6f; // Saldýrýlar arasýndaki bekleme süresi

    private void Awake()
    {
        player = FindObjectOfType<Character>().transform;
    }
    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (!isAttacking && !isSpinning && timeSinceLastAttack >= attackCooldown)
        {
            // Rasgele bir saldýrý türü seç ve gerçekleþtir
            int randomAttackType = Random.Range(0, 5);

            switch (randomAttackType)
            {
                case 0: Attack(); break;
                case 1: StartSpinAttack(); break;
                case 2: GrabAttack(); break;
                case 3: ShootProjectile(); break;
                case 4: if (health < 80) SpecialAttack(); break;
            }

            timeSinceLastAttack = 0f; // Son saldýrýdan sonra geçen süreyi sýfýrla
        }

        if (!isAttacking && !isSpinning)
        {
            // Boss, saldýrmýyor ve dönme saldýrýsý yapmýyorsa oyuncuya doðru yavaþça ilerle
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * movementSpeed * Time.deltaTime;
        }
    }

    private void Attack()
    {
        isAttacking = true;

        // Normal saldýrý animasyonu burada oynatýlabilir

        // Normal saldýrý collider'ýný aktif hale getir
        normalAttackCollider.enabled = true;

        // Oyuncuya hasar verme iþlemleri burada yapýlabilir
        //player.GetComponent<PlayerController>().TakeDamage(attackDamage);

        // Normal saldýrý sonrasý beklemek için bir süre sonra isAttacking'i false yap
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void StartSpinAttack()
    {
        isAttacking = true;
        isSpinning = true;

        // Dönme saldýrýsý animasyonu burada oynatýlabilir

        // Dönme saldýrýsý collider'ýný aktif hale getir
        spinAttackCollider.enabled = true;

        // Dönme saldýrýsý süresi boyunca hasar verme iþlemleri burada yapýlabilir
        Invoke(nameof(EndSpinAttack), spinAttackDuration);
    }

    private void EndSpinAttack()
    {
        isSpinning = false;

        // Dönme saldýrýsý collider'ýný pasif hale getir
        spinAttackCollider.enabled = false;

        // Dönme saldýrýsý sonrasý beklemek için bir süre sonra isAttacking'i false yap
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void GrabAttack()
    {
        isAttacking = true;

        // Kapma saldýrýsý animasyonu burada oynatýlabilir

        // Kapma saldýrýsý collider'ýný aktif hale getir
        grabAttackCollider.enabled = true;

        // Oyuncuya kapma saldýrýsýyla hasar verme ve fýrlatma iþlemleri burada yapýlabilir

        // Kapma saldýrýsý sonrasý beklemek için bir süre sonra isAttacking'i false yap
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void ShootProjectile()
    {
        isAttacking = true;

        // Sülük saldýrýsý animasyonu burada oynatýlabilir

        // Sülük saldýrýsý yapma iþlemleri burada yapýlabilir

        // Sülük saldýrýsý sonrasý beklemek için bir süre sonra isAttacking'i false yap
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void SpecialAttack()
    {
        isAttacking = true;

        // Özel saldýrý animasyonu burada oynatýlabilir

        // Özel saldýrý collider'ýný aktif hale getir
        specialAttackCollider.enabled = true;

        // Oyuncuya özel saldýrýyla yüksek hasar verme iþlemleri burada yapýlabilir

        // Özel saldýrý sonrasý beklemek için bir süre sonra isAttacking'i false yap
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void ResetAttack()
    {
        isAttacking = false;

        // Tüm collider'larý pasif hale getir
        normalAttackCollider.enabled = false;
        spinAttackCollider.enabled = false;
        grabAttackCollider.enabled = false;
        specialAttackCollider.enabled = false;
    }
}