using UnityEngine;

public class DoorCloser : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetTrigger("Close");
        }
    }
}
