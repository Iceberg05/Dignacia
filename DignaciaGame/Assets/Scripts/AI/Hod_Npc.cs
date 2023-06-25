using UnityEngine;

public class Hod_Npc : MonoBehaviour
{
    [Header("Main")]
    [Space]

    [Tooltip("Yapay zekanýn hýzýdýr.")]
    [SerializeField] float speed;
    [Tooltip("Yapay zekanýn arasýnda hareket edeceði noktalardýr.")]
    [SerializeField] Transform movePoints;

    [Header("Movement Values")]
    [Space]

    [Tooltip("Hareket edilecek noktalarýn pozisyonlarýnýn alacaðý minimum X deðeridir.")]
    [SerializeField] float minX;
    [Tooltip("Hareket edilecek noktalarýn pozisyonlarýnýn alacaðý maksimum X deðeridir.")]
    [SerializeField] float maxX;
    [Tooltip("Hareket edilecek noktalarýn pozisyonlarýnýn alacaðý minimum Y deðeridir.")]
    [SerializeField] float minY;
    [Tooltip("Hareket edilecek noktalarýn pozisyonlarýnýn alacaðý maksimum Y deðeridir.")]
    [SerializeField] float maxY;

    [Tooltip("Noktalar arasýnda hareket ederken her noktada ne kadar beklemesi gerektiðini ifade eden deðerdir.")]
    [SerializeField] float startTime;

    [Header("Dialogue")]
    [Space]

    [Tooltip("Konuþma balonu resmidir.")]
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

    //DÝYALOG KISMI GELÝÞTÝRÝLECEK
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