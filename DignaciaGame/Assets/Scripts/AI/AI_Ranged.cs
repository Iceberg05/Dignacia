using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Ranged : MonoBehaviour
{
    private Transform player;
    public float Damage;
    private Vector2 target;
    public float speed;
    Rigidbody2D rb;


    void Start()
    {
      
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
        Destroy(gameObject, 5f);
        rb = GetComponent<Rigidbody2D>();
        speed = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
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