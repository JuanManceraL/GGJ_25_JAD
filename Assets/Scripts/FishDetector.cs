using UnityEngine;

public class FishDetector : MonoBehaviour
{
    [SerializeField] private Fish Fish;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Fish.Attack(other.gameObject.transform);
        }
    }
}
