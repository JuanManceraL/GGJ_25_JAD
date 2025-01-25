using UnityEngine;
using System.IO;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextAsset missionsText;
    [SerializeField] private int actualMissionNumber;
    [SerializeField] private string actualMission;
    private string[] missions;
    [SerializeField] private bool nextM;
    private DialogueManager dialogueManager;

    [SerializeField] GameObject missionsUI;
    [SerializeField] private TMP_Text missionsBox;
    [SerializeField] private GameObject[] objectsToActivate;
    private string lastMission;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
        Debug.Log(reader.ReadToEnd());
        Debug.Log(reader.ReadLine());
        reader.Close();
        */
        dialogueManager = gameObject.GetComponent<DialogueManager>();

        missions = missionsText.text.Split('\n');

        actualMissionNumber = -1;
        NextMission();
        nextM = false;

        lastMission = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (nextM == true)
        {
            nextM = false;
            NextMission();
        }
    }

    public void NextMission()
    {
        actualMissionNumber++;
        if (actualMissionNumber >= missions.Length)
        {
            return;
        }
        actualMission = missions[actualMissionNumber];
        //Debug.Log(actualMission);
        //Debug.Log(actualMission[0]);
        /*
         * m (actions: number a/d)
            m (2,0)
         * */

        switch (actualMission[0])
        {
            case 'm':
                //Debug.Log("Mision: " + actualMission.Substring(2));
                missionsUI.SetActive(true);
                missionsBox.text = actualMission.Substring(2);
                NextMission();
                break;
            case 'd':
                //Debug.Log("Dialogue: " + actualMission.Substring(4) + "\nDetener en:" + actualMission[2]);
                dialogueManager.ShowText(actualMission.Substring(2));
                break;
            case 't':
                //Debug.Log("Wait Time: " + actualMission[3] + actualMission[3]);
                Invoke("WaitToNextMission", int.Parse(actualMission[2].ToString() + actualMission[3]));
                break;
            case 'a':
                //Debug.Log("Activate object no: " + actualMission[2] + actualMission[3] + "  " + actualMission[5]);
                int obj = int.Parse(actualMission[2].ToString() + actualMission[3]);
                if (obj < actualMission.Length)
                {
                    if (actualMission[5] == '1')
                    {
                        objectsToActivate[obj].SetActive(true);
                    }
                    else
                    {
                        objectsToActivate[obj].SetActive(false);
                    }
                }
                else
                {
                    Debug.Log("Error, objeto no existente");
                }
                NextMission();
                break;
            case 'w':
                //Debug.Log("Wait until: " + actualMission.Substring(2));
                lastMission = actualMission.Substring(2);
                break;
            default:
                break;
        }
    }

    string GetDifference(string str1, string str2)
    {
        int minLength = Mathf.Min(str1.Length, str2.Length);

        // Encuentra el índice donde las cadenas comienzan a diferir
        int index = 0;
        while (index < minLength && str1[index] == str2[index])
        {
            index++;
        }

        // Devuelve la parte diferente de la cadena más larga
        return index < str2.Length ? str2.Substring(index) : "";
    }

    public void missionComplete(string mc)
    {   
        if (lastMission.Contains(mc))
        {
            lastMission = "";
            NextMission();
        }
    }

    private void WaitToNextMission()
    {
        NextMission();
    }

}
