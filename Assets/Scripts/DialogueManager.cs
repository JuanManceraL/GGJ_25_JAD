using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text dialogueBox;
    [SerializeField] private GameObject dialogueUI;

    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowText(string info)
    {
        //Debug.Log(info[0] + "  " + time)    ;
        //Debug.Log("Desactivando en: " + time);    match.Groups[2].Value;
        Match match = Regex.Match(info, @"^(\d+)\s*(.*)");

        Invoke("CloseUi", int.Parse(match.Groups[1].Value));

        //string text = info.Substring(2);
        string text = match.Groups[2].Value;
        dialogueUI.SetActive(true);
        dialogueBox.text = text;
    }

    private void CloseUi()
    {
        //Animacion desactivar
        dialogueUI.SetActive(false);
        gameManager.NextMission();
        //Debug.Log("Bye");
    }
}
