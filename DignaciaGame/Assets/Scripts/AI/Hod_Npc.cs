using UnityEngine;

public class Hod_Npc : MonoBehaviour
{
    [Header("Main")]
    [Space]

    [Tooltip("Yapay zekan�n h�z�d�r.")]
    [SerializeField] float speed;
    [Tooltip("Yapay zekan�n aras�nda hareket edece�i noktalard�r.")]
    [SerializeField] Transform movePoints;

    [Header("Movement Values")]
    [Space]

    [Tooltip("Hareket edilecek noktalar�n pozisyonlar�n�n alaca�� minimum X de�eridir.")]
    [SerializeField] float minX;
    [Tooltip("Hareket edilecek noktalar�n pozisyonlar�n�n alaca�� maksimum X de�eridir.")]
    [SerializeField] float maxX;
    [Tooltip("Hareket edilecek noktalar�n pozisyonlar�n�n alaca�� minimum Y de�eridir.")]
    [SerializeField] float minY;
    [Tooltip("Hareket edilecek noktalar�n pozisyonlar�n�n alaca�� maksimum Y de�eridir.")]
    [SerializeField] float maxY;

    [Tooltip("Noktalar aras�nda hareket ederken her noktada ne kadar beklemesi gerekti�ini ifade eden de�erdir.")]
    [SerializeField] float startTime;

    [Header("Dialogue")]
    [Space]

    [Tooltip("Konu�ma balonu resmidir.")]
    [SerializeField] GameObject messageBox;
    float waitTime;
    void Start()
    {
        movePoints.position = new Vector2(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY)
        );
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(
            transform.position,
            movePoints.position,
            speed * Time.deltaTime
        );

        if (Vector2.Distance(transform.position, movePoints.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                movePoints.position = new Vector2(
                Random.Range(minX, maxX),
                Random.Range(minY, maxY)
                );
                waitTime = startTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    //D�YALOG KISMI GEL��T�R�LECEK
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            messageBox.SetActive(true);
        }
        else
        {
            messageBox.SetActive(false);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            messageBox.SetActive(false);
        }
    }
}