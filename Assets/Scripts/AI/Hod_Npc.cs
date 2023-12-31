using UnityEngine;

public class Hod_Npc : MonoBehaviour
{
    [Header("Main")]
    [Space]

    [Tooltip("Yapay zekanın hızıdır.")]
    [SerializeField] float speed;
    [Tooltip("Yapay zekanın arasında hareket edeceği noktalardır.")]
    [SerializeField] Transform movePoints;

    [Header("Movement Values")]
    [Space]

    [Tooltip("Hareket edilecek noktaların pozisyonlarının alacağı minimum X değeridir.")]
    [SerializeField] float minX;
    [Tooltip("Hareket edilecek noktaların pozisyonlarının alacağı maksimum X değeridir.")]
    [SerializeField] float maxX;
    [Tooltip("Hareket edilecek noktaların pozisyonlarının alacağı minimum Y değeridir.")]
    [SerializeField] float minY;
    [Tooltip("Hareket edilecek noktaların pozisyonlarının alacağı maksimum Y değeridir.")]
    [SerializeField] float maxY;

    [Tooltip("Noktalar arasında hareket ederken her noktada ne kadar beklemesi gerektiğini ifade eden değerdir.")]
    [SerializeField] float startTime;

    [Header("Dialogue")]
    [Space]

    [Tooltip("Konuşma balonu resmidir.")]
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

    //DİYALOG KISMI GELİŞTİRİLECEK
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