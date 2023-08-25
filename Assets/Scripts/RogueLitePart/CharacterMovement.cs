using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    Rigidbody2D rb;
    Vector2 movement;
    Vector2 mousePos;
    public Camera cam;
    Animator playerAnimator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        playerAnimator.SetFloat("Horizontal", movement.x);
        playerAnimator.SetFloat("Vertical", movement.y);
        playerAnimator.SetFloat("Speed", movement.sqrMagnitude);
    }
    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement *moveSpeed * Time.fixedDeltaTime);

        //if (Input.GetKey(KeyCode.Mouse0))
        //{



        //    Vector2 lookdir = mousePos - rb.position;
        //    float angel = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
        //    rb.rotation = angel;
        //}
    }
}