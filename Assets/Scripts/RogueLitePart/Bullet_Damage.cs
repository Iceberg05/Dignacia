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
        damageCol.enabled = false; //kapsül kollider baþta kapalý oluyor çünkü ok atan düþmanlarýn circle kolliderla menzilleri büyük ona deðince
                                   //hitbox kocaman oluyor bunu baþta kapalý yaparak düþmanýn gerçek hitboxýna deðince "þuanlýk capsule collider yaptým" çalýþýyor
    }
    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") & other is CapsuleCollider2D )  //karþýsýndaki düþmanýn Capsule colliderina deðince çalýþýr
        {
            damageCol.enabled = true;  // kapsül colliderý açýyoruz
            Destroy(gameObject, 0.1f);
        }

    }
}