using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private float velShrink;
    [SerializeField] private float velFloat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale -= Vector3.one * velShrink * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, transform.position.y + velFloat * Time.deltaTime, transform.position.z);

        if (gameObject.transform.localScale.x <= 0.3f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log(other.name);
            Oxigen oxigen = other.GetComponent<Oxigen>();

            oxigen.IncreaseOxigen();
            Destroy(gameObject);
        }
    }
}
