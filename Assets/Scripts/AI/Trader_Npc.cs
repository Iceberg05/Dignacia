using UnityEngine;

public class Trader_Npc : MonoBehaviour
{
    [Tooltip("Maðaza UI objesidir.")]
    [SerializeField] GameObject store;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            store.SetActive(true);
        }
        else
        {
            store.SetActive(false);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            store.SetActive(false);
        }
    }
}
