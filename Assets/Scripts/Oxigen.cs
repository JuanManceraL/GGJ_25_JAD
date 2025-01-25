using UnityEngine;
using UnityEngine.UI;

public class Oxigen : MonoBehaviour
{
    [SerializeField] private float maxOxigen;
    [SerializeField] [Range(0,100)] private float actualOxigen;
    [SerializeField] private float increaseOxigen;
    [SerializeField] private float speedDecreaseOxigen;

    [SerializeField] private Slider sliderOxigen;
    [SerializeField] private Health healthPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        healthPlayer = gameObject.GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        RestOxigen();
        if (actualOxigen <= 0)
        {
            healthPlayer.Damage();
        }
    }

    private void RestOxigen()
    {
        actualOxigen -= Time.deltaTime * speedDecreaseOxigen;
        sliderOxigen.value = actualOxigen;
    }

    public void IncreaseOxigen()
    {
        actualOxigen += increaseOxigen;
        sliderOxigen.value = actualOxigen;
    }

    
}
