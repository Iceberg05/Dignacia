using UnityEngine;

public class HeavilyArmoredKatie : MonoBehaviour
{

    [Header("Main")]
    [Space]

    Transform player;

    [Tooltip("Normal sald�r� i�in kullan�lacak transform noktas�d�r.")]
    [SerializeField] Transform attackPoint;
    [Tooltip("Projectile sald�r�s� i�in kullan�lacak prefabdir.")]
    [SerializeField] GameObject projectilePrefab;

    [Header("Colliders")]
    [Space]

    [Tooltip("Normal sald�r� i�in kullan�lacak colliderd�r.")]
    [SerializeField] Collider normalAttackCollider;
    [Tooltip("D�nen sald�r� i�in kullan�lacak colliderd�r.")]
    [SerializeField] Collider spinAttackCollider;
    [Tooltip("Kapma sald�r�s� i�in kullan�lacak colliderd�r.")]
    [SerializeField] Collider grabAttackCollider;
    [Tooltip("�zel sald�r� i�in kullan�lacak colliderd�r.")]
    [SerializeField] Collider specialAttackCollider;

    [Header("Values")]
    [Space]

    [Tooltip("Bossun can miktar�d�r.")]
    [SerializeField] float health;
    [Tooltip("Bossun hareket h�z�d�r.")]
    [SerializeField] float movementSpeed = 2f;
    [Tooltip("Normal sald�r� mesafesidir.")]
    [SerializeField] float attackRadius = 2f;
    [Tooltip("D�nme sald�r�s� s�residir.")]
    [SerializeField] float spinAttackDuration = 3f;
    [Tooltip("Kapma sald�r�s� hasar�d�r.")]
    [SerializeField] float grabAttackDamage = 10f;
    [Tooltip("Kapma sald�r�s�nda uygulanan kuvvet miktar�d�r.")]
    [SerializeField] float grabAttackForce = 10f;
    [Tooltip("Normal sald�r� hasar�d�r.")]
    [SerializeField] float specialAttackDamage = 100f;

    [Header("Attack Control")]
    [Space]

    bool isAttacking = false; // Normal sald�r� durumu
    bool isSpinning = false; // D�nme sald�r�s� durumu
    float timeSinceLastAttack = 0f; // Son sald�r�dan bu yana ge�en s�re
    float attackCooldown = 6f; // Sald�r�lar aras�ndaki bekleme s�resi

    private void Awake()
    {
        player = FindObjectOfType<Character>().transform;
    }
    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (!isAttacking && !isSpinning && timeSinceLastAttack >= attackCooldown)
        {
            // Rasgele bir sald�r� t�r� se� ve ger�ekle�tir
            int randomAttackType = Random.Range(0, 5);

            switch (randomAttackType)
            {
                case 0: Attack(); break;
                case 1: StartSpinAttack(); break;
                case 2: GrabAttack(); break;
                case 3: ShootProjectile(); break;
                case 4: if (health < 80) SpecialAttack(); break;
            }

            timeSinceLastAttack = 0f; // Son sald�r�dan sonra ge�en s�reyi s�f�rla
        }

        if (!isAttacking && !isSpinning)
        {
            // Boss, sald�rm�yor ve d�nme sald�r�s� yapm�yorsa oyuncuya do�ru yava��a ilerle
            Vector3 direction = (player.transform.position - transform.position).normalized;
            transform.position += direction * movementSpeed * Time.deltaTime;
        }
    }

    private void Attack()
    {
        isAttacking = true;

        // Normal sald�r� animasyonu burada oynat�labilir

        // Normal sald�r� collider'�n� aktif hale getir
        normalAttackCollider.enabled = true;

        // Oyuncuya hasar verme i�lemleri burada yap�labilir
        //player.GetComponent<PlayerController>().TakeDamage(attackDamage);

        // Normal sald�r� sonras� beklemek i�in bir s�re sonra isAttacking'i false yap
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void StartSpinAttack()
    {
        isAttacking = true;
        isSpinning = true;

        // D�nme sald�r�s� animasyonu burada oynat�labilir

        // D�nme sald�r�s� collider'�n� aktif hale getir
        spinAttackCollider.enabled = true;

        // D�nme sald�r�s� s�resi boyunca hasar verme i�lemleri burada yap�labilir
        Invoke(nameof(EndSpinAttack), spinAttackDuration);
    }

    private void EndSpinAttack()
    {
        isSpinning = false;

        // D�nme sald�r�s� collider'�n� pasif hale getir
        spinAttackCollider.enabled = false;

        // D�nme sald�r�s� sonras� beklemek i�in bir s�re sonra isAttacking'i false yap
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void GrabAttack()
    {
        isAttacking = true;

        // Kapma sald�r�s� animasyonu burada oynat�labilir

        // Kapma sald�r�s� collider'�n� aktif hale getir
        grabAttackCollider.enabled = true;

        // Oyuncuya kapma sald�r�s�yla hasar verme ve f�rlatma i�lemleri burada yap�labilir

        // Kapma sald�r�s� sonras� beklemek i�in bir s�re sonra isAttacking'i false yap
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void ShootProjectile()
    {
        isAttacking = true;

        // S�l�k sald�r�s� animasyonu burada oynat�labilir

        // S�l�k sald�r�s� yapma i�lemleri burada yap�labilir

        // S�l�k sald�r�s� sonras� beklemek i�in bir s�re sonra isAttacking'i false yap
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void SpecialAttack()
    {
        isAttacking = true;

        // �zel sald�r� animasyonu burada oynat�labilir

        // �zel sald�r� collider'�n� aktif hale getir
        specialAttackCollider.enabled = true;

        // Oyuncuya �zel sald�r�yla y�ksek hasar verme i�lemleri burada yap�labilir

        // �zel sald�r� sonras� beklemek i�in bir s�re sonra isAttacking'i false yap
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    private void ResetAttack()
    {
        isAttacking = false;

        // T�m collider'lar� pasif hale getir
        normalAttackCollider.enabled = false;
        spinAttackCollider.enabled = false;
        grabAttackCollider.enabled = false;
        specialAttackCollider.enabled = false;
    }
}