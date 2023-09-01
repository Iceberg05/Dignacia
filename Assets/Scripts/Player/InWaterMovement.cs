using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InWaterMovement : MonoBehaviour
{
    public float currentMoveSpeed;
    public float defaultMoveSpeed = 100;

    [SerializeField] bool canRiseTheSurface = false;

    [SerializeField] float gravityScale;

    bool isMoving = false;

    float positionX;

    public float movementSmoothing = 0.05f;
    public Vector3 velocity = Vector3.zero;

    Rigidbody2D rb;
    Vector2 movement;

    #region DASH_VARIABLES
    [Tooltip("Karakterin dash s?ras?ndaki hareket h?z?d?r.")]
    [SerializeField] float dashSpeed = 500f;
    [Tooltip("Dashin s?re de?eridir (saniye bi?iminden).")]
    [SerializeField] float dashDuration = 0.2f;
    [Tooltip("Dashler aras? bekleme s?resi/cooldowndur.")]
    [SerializeField] float dashCooldown = 2f;
    bool isDashing = false; // Karakterin dash hareketi yap?p yapmad???n? kontrol eder.
    float dashTimer = 0f; //Dash hareketi s?resini takip eder.
    float dashCooldownTimer = 0f; //Dash hareketleri aras?ndaki bekleme s?resini takip eder.
    #endregion

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentMoveSpeed = 130;
        transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerTransformX"), transform.position.y, transform.position.z);
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        if (isMoving)
        {
            rb.gravityScale = 0f;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }
        #region DASH_PART
        if (isDashing)
        {
            //Dash hareketi sýrasýnda hýzlan
            currentMoveSpeed = dashSpeed;
            //Dash hareketi suresini kontrol etme
            dashTimer += Time.deltaTime;
            if (dashTimer >= dashDuration)
            {
                isDashing = false;
                gameObject.layer = 0;  // 0: Default layer
                dashTimer = 0f;
            }
        }
        else
        {
            currentMoveSpeed = defaultMoveSpeed;
            if (dashCooldownTimer > 0f)
            {
                dashCooldownTimer -= Time.deltaTime;
            }
            else if (Input.GetButtonDown("Dash"))
            {
                isDashing = true;
                dashCooldownTimer = dashCooldown;
            }
        }
        #endregion
        if (Input.GetKeyDown(KeyCode.LeftControl) && canRiseTheSurface)
        {
            PlayerPrefs.SetFloat("PlayerTransformX", positionX);
            SceneManager.LoadScene(5);
        }
    }
    private void FixedUpdate()
    {
        //Vector2 moveDir = movement.normalized;
        //rb.velocity = moveDir * currentMoveSpeed * Time.deltaTime;
        Move(movement.x, Vector2.right.x);
        Move(movement.y, Vector2.up.y);
        positionX = transform.position.x;
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Surface")
        {
            canRiseTheSurface = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Surface")
        {
            canRiseTheSurface = false;
        }
    }
    private void LateUpdate()
    {
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -20);
    }
    void Move(float move, float direction)
    {
        Vector3 targetVelocity = new Vector2(move * 10f, direction);
        targetVelocity = movement.normalized;
        rb.velocity = Vector3.SmoothDamp(rb.velocity * currentMoveSpeed / 100, targetVelocity, ref velocity, movementSmoothing);
    }
}