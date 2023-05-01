using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    [Tooltip("Portal ile etkileþime girildikten sonra yüklenmesi istenen sahnenin ismidir.")]
    [SerializeField] string sceneNameToLoad;

    [Tooltip("Portalýn animasyon süresidir. Iþýnlanma iþlemi, bu süre sonrasýnda gerçekleþir.")]
    [SerializeField] float animDuration;

    [Tooltip("Animator içerisindeki ismiyle, ýþýnlanma esnasýnda oynamasý istenen animasyonun ismi.")]
    [SerializeField] string teleportingAnimName = "Anim_Teleporting";

    [Tooltip("Animator içerisindeki ismiyle, ýþýnlanma dýþýnda oynamasý istenen animasyonun ismi.")]
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

    //Iþýnlanma sürecidir. Animasyon oynatýlýr, animasyon süresi kadar süre geçtikten sonra yeni sahne yüklenir.
    IEnumerator TeleportProgress()
    {
        animator.Play(teleportingAnimName);
        yield return new WaitForSeconds(animDuration);
        SceneManager.LoadScene(sceneNameToLoad);
    }
}