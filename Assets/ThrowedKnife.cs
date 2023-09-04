using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowedKnife : MonoBehaviour
{

   public Knife knife;
    public float stoptimer;
    public float stopduration = 0.3f;
    CapsuleCollider2D knifeAttackCollider;
    private void Start()
    {
        stopduration = 0.5f;
        knifeAttackCollider = GetComponent<CapsuleCollider2D>();
        knife = FindObjectOfType<Knife>();
        StartCoroutine(KnifeCollider());
    }
    private void Update()
    {
        Knife knifescript = knife.GetComponent<Knife>();
        if (knifescript.isHaveKnife == false)
        {
            stoptimer += Time.deltaTime;
            if (stoptimer >= stopduration)
            {
               // isKnockedBack = false;      alýnca deðiþcek
                stoptimer = 0f;
                // Knockback bittiðinde düþmanýn hýzýný azalt
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.velocity = rb.velocity * 0f; // Hýzý yavaþlat, istediðiniz faktörü ayarlayabilirsiniz
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Knife knifescript = knife.GetComponent<Knife>();
        if (other.CompareTag("Player") && knifescript.isHaveKnife == false)
        {
            knifescript.spriteRenderer.enabled = true;
            knifescript.isHaveKnife = true;
            Destroy(gameObject);
        }
    }

    IEnumerator KnifeCollider()
    {
        knifeAttackCollider.enabled = false;
        yield return new WaitForSeconds(2);
        knifeAttackCollider.enabled = true;
    }
}
