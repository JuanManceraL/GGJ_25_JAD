using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<string> obtainedObjects = new List<string>();
    public GameObject[] obj1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ObtainObjects(string numObj)
    {
        obtainedObjects.Add(numObj);
        Debug.Log(numObj);

        switch (numObj)
        {
            case "Lámpara":
                obj1[0].SetActive(true);
                break;
            case "Manivela":
                obj1[1].SetActive(true);
                break;
            case "RecolectorDeOxigeno":
                obj1[2].SetActive(true);
                break;
            case "Soldador":
                obj1[3].SetActive(true);
                break;
            default:
                break;
        }
    }
}
