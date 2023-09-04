using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public GameObject KnifePrefab;
    public float KnifeForce = 10f;
    public bool isHaveKnife;
    private Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        isHaveKnife = true;
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Attack();
        }

        if (Input.GetMouseButtonDown(1))
        {
            Throw();
            isHaveKnife = false;
        }
    }
    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit" + enemy.name);
        }
    }
    void Throw()
    {

        if (isHaveKnife)
        {
        GameObject knife = Instantiate(KnifePrefab, attackPoint.position, attackPoint.rotation);
        Rigidbody2D rb = knife.GetComponent<Rigidbody2D>();
        rb.AddForce(attackPoint.up * KnifeForce, ForceMode2D.Impulse);
            spriteRenderer.enabled = false;
        }

    }
    void OnDrawGizmos()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ThrowingKnife")
        {
            isHaveKnife = true;
        }
    }
    IEnumerator KnifeStopInAir()
    {
        yield return new WaitForSeconds(2f);
        if (rb != null)
        {
            // Býçaðý tamamen durdur
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }
}