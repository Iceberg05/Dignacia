using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject player;
    public Transform attackPoint; // Normal saldýrý için kullanýlacak transform noktasý
    public GameObject projectilePrefab; // Sülük saldýrýsý için kullanýlacak prefab
    public Collider normalAttackCollider; // Normal saldýrý için kullanýlacak collider
    public Collider spinAttackCollider; // Dönme saldýrýsý için kullanýlacak collider
    public Collider grabAttackCollider; // Kapma saldýrýsý için kullanýlacak collider
    public Collider specialAttackCollider; // Özel saldýrý için kullanýlacak collider
    public float attackRadius = 2f; // Normal saldýrý mesafesi
    public float spinAttackDuration = 3f; // Dönme saldýrýsý süresi
    public float grabAttackDamage = 10f; // Kapma saldýrýsý hasarý
    public float grabAttackForce = 10f; // Kapma saldýrýsý uygulanan kuvvet
    public float specialAttackDamage = 100f; // Özel saldýrý hasarý
    public float movementSpeed = 2f; // Boss'un hareket hýzý
    public int health;
    private bool isAttacking = false; // Normal saldýrý durumu
    private bool isSpinning = false; // Dönme saldýrýsý durumu
    private float timeSinceLastAttack = 0f; // Son saldýrýdan bu yana geçen süre
    private float attackCooldown = 6f; // Saldýrýlar arasýndaki bekleme süresi

    private void Update()
    {
        timeSinceLastAttack += Time.deltaTime;

        if (!isAttacking && !isSpinning && timeSinceLastAttack >= attackCooldown)
        {
            // Rasgele bir saldýrý türü seç ve gerçekleþtir
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