using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractButton : MonoBehaviour
{
    public List<string> Requirements = new List<string>();

    [SerializeField] private GameManager gameManager;
    [SerializeField] private string misionToComplete;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnInteract(List<string> obtObj)
    {
        bool FulFills = true;
        //Debug.Log("Req: ");
        
        for (int i = 0; i < Requirements.Count; i++)
        {
            //Debug.Log("obj " + i + " : " + Requirements[i]);
            //Debug.Log(obtObj.Contains(Requirements[i]));
            if (!obtObj.Contains(Requirements[i]))
            {
                FulFills = false;
            }
        }

        if (FulFills)
        {
            Debug.Log("Tienes todo");
            gameManager.missionComplete(misionToComplete);
        }
        else
        {
            Debug.Log("Aún te falta");
        }

    }
}
