using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    [SerializeField] GameObject Player;
    Rigidbody2D rb;
    
    Vector2 mousePos;
    public Camera cam;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }
    void Update()
    {
        
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }
    private void FixedUpdate() {
        
        transform.position = Player.transform.position;
        
        
        
        

        Vector2 lookdir = mousePos - rb.position;
        float angel = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angel;
        
    }


}