using UnityEngine;
public class AI_Ranged : MonoBehaviour
{
    Transform player;
    Vector2 target;

    [SerializeField] float damage;
    [SerializeField] float speed;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        Destroy(gameObject, 5f);
        speed = 3f;
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DestroyProjectile();
            switch (GetComponent<RogueLiteCharacter>().ArmorValue)
            {
                case float n when n >= 25:
                    GetComponent<RogueLiteCharacter>().HealthValue = GetComponent<RogueLiteCharacter>().HealthValue - 5;
                    break;
                case float n when n >= 50:
                    GetComponent<RogueLiteCharacter>().HealthValue = GetComponent<RogueLiteCharacter>().HealthValue - 4;
                    break;
                case float n when n >= 75:
                    GetComponent<RogueLiteCharacter>().HealthValue = GetComponent<RogueLiteCharacter>().HealthValue - 3;
                    break;
                case float n when n >= 100:
                    GetComponent<RogueLiteCharacter>().HealthValue = GetComponent<RogueLiteCharacter>().HealthValue - 2;
                    break;
                default:
                    GetComponent<RogueLiteCharacter>().HealthValue = GetComponent<RogueLiteCharacter>().HealthValue - 6;
                    break;
            }
        }
        if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
        void DestroyProjectile()
        {
            Destroy(gameObject, 3f);
        }
    }
}