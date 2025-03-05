using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorCloser : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Animator Fade;
    [SerializeField] private GameObject Fader;

    private void Start()
    {
        Fader.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("Close");
            Invoke("Transicion", 3f);
        }
    }

    void Transicion()
    {
        Fade.SetTrigger("Fade");
        Invoke("CambiarEscena", 2);
    }

    void CambiarEscena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
