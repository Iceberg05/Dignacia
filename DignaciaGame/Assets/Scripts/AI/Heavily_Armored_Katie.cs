using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject player;
    public Transform attackPoint; // Normal sald�r� i�in kullan�lacak transform noktas�
    public GameObject projectilePrefab; // S�l�k sald�r�s� i�in kullan�lacak prefab
    public Collider normalAttackCollider; // Normal sald�r� i�in kullan�lacak collider
    public Collider spinAttackCollider; // D�nme sald�r�s� i�in kullan�lacak collider
    public Collider grabAttackCollider; // Kapma sald�r�s� i�in kullan�lacak collider
    public Collider specialAttackCollider; // �zel sald�r� i�in kullan�lacak collider
    public float attackRadius = 2f; // Normal sald�r� mesafesi
    public float spinAttackDuration = 3f; // D�nme sald�r�s� s�resi
    public float grabAttackDamage = 10f; // Kapma sald�r�s� hasar�
    public float grabAttackForce = 10f; // Kapma sald�r�s� uygulanan kuvvet
    public float specialAttackDamage = 100f; // �zel sald�r� hasar�
    public float movementSpeed = 2f; // Boss'un hareket h�z�
    public int health;
    private bool isAttacking = false; // Normal sald�r� durumu
    private bool isSpinning = false; // D�nme sald�r�s� durumu
    private float timeSinceLastAttack = 0f; // Son sald�r�dan bu yana ge�en s�re
    private float attackCooldown = 6f; // Sald�r�lar aras�ndaki bekleme s�resi

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (!isAttacking && !isSpinning && timeSinceLastAttack >= attackCooldown)
        {
            // Rasgele bir sald�r� t�r� se� ve ger�ekle�tir
            int randomAttackType = Random.Range(0, 5);

            if (randomAttackType == 0)
            {
                Attack();
            }
            else if (randomAttackType == 1)
            {
                StartSpinAttack();
            }
            else if (randomAttackType == 2)
            {
                GrabAttack();
            }
            else if (randomAttackType == 3)
            {
                ShootProjectile();
            }
            else if (randomAttackType == 4 && health < 80)
            {
                SpecialAttack();
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