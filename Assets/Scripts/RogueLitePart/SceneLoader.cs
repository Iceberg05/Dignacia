using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] int[] normalRogueLiteScenes;
    [SerializeField] int[] bossRogueLiteScenes;
    [SerializeField] int[] emptyScenes;                    //Oyun Birleþtirilikren Update Fonksiyonundaki kodun aynýsý öbür sahneler için yazýlacak
    [SerializeField] bool loadRandomScene = false;
    [SerializeField] int index;
    [SerializeField] int SceneLoaded = 0;
    void Awake()
    {
        GameObject.FindGameObjectWithTag("Player");
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if (loadRandomScene)
        {
            if (SceneLoaded <= 3)
            {
                int maxIndex = normalRogueLiteScenes.Length;
                index = Random.Range(0, maxIndex);
                if (SceneManager.GetActiveScene().buildIndex != index)
                {
                    SceneManager.LoadScene(normalRogueLiteScenes[index]);
                    loadRandomScene = false;
                }

            }
            else if (SceneLoaded > 3)
            {
                int maxIndex2 = bossRogueLiteScenes.Length;
                index = Random.Range(0, maxIndex2);
                if (SceneManager.GetActiveScene().buildIndex != index)
                {
                    SceneManager.LoadScene(bossRogueLiteScenes[index]);
                    loadRandomScene = false;
                }

            }
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            loadRandomScene = true;
            DontDestroyOnLoad(col.gameObject);
            SceneLoaded += 1;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            loadRandomScene = false;
        }
    }
}
