using UnityEngine;
using System.IO;
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
        switch (actualMission[0])
        {
            case 'm':
                Debug.Log("Mision: " + actualMission.Substring(2));
                missionsUI.SetActive(true);
                missionsBox.text = actualMission.Substring(2);
                break;
            case 'd':
                Debug.Log("Dialogue: " + actualMission.Substring(4) + "\nDetener en:" + actualMission[2]);
                dialogueManager.ShowText(actualMission.Substring(2));
                break;
            case 'w':
                Debug.Log("Wait: " + actualMission.Substring(2));
                break;
            default:
                break;
        }
    }

    /*
     * if (missionsText != null)
        {
            

            foreach (string line in lines)
            {
                Debug.Log(line);
            }

        }*/
}
