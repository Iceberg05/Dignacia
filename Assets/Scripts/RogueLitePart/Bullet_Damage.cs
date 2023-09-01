using UnityEngine;
public class Bullet_Damage : MonoBehaviour
{
    Transform player;
    public CameraShake cameraShake;
    [SerializeField] float damage;
    CapsuleCollider2D damageCol;
    void Start()
    {
        damageCol = GetComponent<CapsuleCollider2D>();
        cameraShake = GameObject.Find("PlayerCamera").GetComponent<CameraShake>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Destroy(gameObject, 5f);
        damageCol.enabled = false; //kaps�l kollider ba�ta kapal� oluyor ��nk� ok atan d��manlar�n circle kolliderla menzilleri b�y�k ona de�ince
                                   //hitbox kocaman oluyor bunu ba�ta kapal� yaparak d��man�n ger�ek hitbox�na de�ince "�uanl�k capsule collider yapt�m" �al���yor
    }
    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") & other is CapsuleCollider2D )  //kar��s�ndaki d��man�n Capsule colliderina de�ince �al���r
        {
            damageCol.enabled = true;  // kaps�l collider� a��yoruz
            Destroy(gameObject, 0.1f);
        }

    }
}