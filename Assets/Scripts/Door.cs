using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        anim.SetTrigger("Open");
    }

    private void OnDisable()
    {
        anim.SetTrigger("Close");
    }
}
