using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleGenerator : MonoBehaviour
{
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private bool generate;
    [SerializeField] private float timeToGenerate;
    [SerializeField] private Transform parentToGenerate;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GenerateBubble());
    }


    IEnumerator GenerateBubble()
    {
        while (generate)
        {
            yield return new WaitForSeconds(timeToGenerate);
            GameObject newBubble = Instantiate(bubblePrefab, transform.position, transform.rotation);
            newBubble.transform.SetParent(parentToGenerate);
        }

        yield return null;
    }
}
