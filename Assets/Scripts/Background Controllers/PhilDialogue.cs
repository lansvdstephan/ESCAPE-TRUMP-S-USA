using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PhilDialogue : MonoBehaviour {
    public static PhilDialogue Instance { get; set; }
    public GameObject dialoguePanel;

    private List<string> dialogueLines;
    private Text dialogueText;
    private int dialogueIndex;

    void Awake ()
    {
        dialogueText = dialoguePanel.transform.FindChild("Dialogue Text").GetComponent<Text>();
        dialoguePanel.SetActive(false);        

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
	
    public void AddNewDialogue(string[] lines)
    {
        dialogueIndex = 0;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);
        Time.timeScale = 0f;
        CreateDialogue();
    }

    public void CreateDialogue()
    {
        print( "started dialog");
		dialogueText.text = dialogueLines[dialogueIndex].Replace("\n", Environment.NewLine);
        dialoguePanel.SetActive(true);
    }

    public void ContinueDialogue()
    {
        if (dialogueIndex < dialogueLines.Count -1)
        {
            dialogueIndex++;
			dialogueText.text = dialogueLines [dialogueIndex].Replace("\n", Environment.NewLine);
        }
        else
        {
            dialoguePanel.SetActive(false);
            print("Dialogue ended");
            Time.timeScale = 1f;
        }
    }
}
