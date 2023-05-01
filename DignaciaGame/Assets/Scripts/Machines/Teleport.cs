using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [Tooltip("Portal ile etkile�ime girildikten sonra y�klenmesi istenen sahnenin ismidir.")]
    [SerializeField] string sceneNameToLoad;

    [Tooltip("Portal�n animasyon s�residir. I��nlanma i�lemi, bu s�re sonras�nda ger�ekle�ir.")]
    [SerializeField] float animDuration;

    [Tooltip("Animator i�erisindeki ismiyle, ���nlanma esnas�nda oynamas� istenen animasyonun ismi.")]
    [SerializeField] string teleportingAnimName = "Anim_Teleporting";

    [Tooltip("Animator i�erisindeki ismiyle, ���nlanma d���nda oynamas� istenen animasyonun ismi.")]
    [SerializeField] string idleAnimName = "Anim_Idle";

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(idleAnimName);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (Input.GetButtonDown("Interact"))
            {
                StartCoroutine(TeleportProgress());
            }
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (Input.GetButtonDown("Interact"))
            {
                StartCoroutine(TeleportProgress());
            }
        }
    }

    //I��nlanma s�recidir. Animasyon oynat�l�r, animasyon s�resi kadar s�re ge�tikten sonra yeni sahne y�klenir.
    IEnumerator TeleportProgress()
    {
        animator.Play(teleportingAnimName);
        yield return new WaitForSeconds(animDuration);
        SceneManager.LoadScene(sceneNameToLoad);
    }
}