using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PhilDialogue : MonoBehaviour {
    public static PhilDialogue Instance { get; set; }
    public GameObject dialoguePanel;
    public bool Noanimation = false;
    public Sprite joe;

    private List<string> dialogueLines;
    private Image narrator;
    private Text dialogueText;
    private int dialogueIndex;

    void Awake ()
    {
        dialogueText = dialoguePanel.transform.FindChild("Dialogue Text").GetComponent<Text>();
        narrator = dialoguePanel.transform.FindChild("Image").GetComponent<Image>();
        dialoguePanel.SetActive(false);

        narrator.sprite = joe;

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
        if(Noanimation) Time.timeScale = 0f;
        CreateDialogue();
        narrator.sprite = joe;
    }

    public void AddNewDialogue(string[] lines, Sprite converstationPartner)
    {
        dialogueIndex = 0;
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);
        if (Noanimation) Time.timeScale = 0f;
        CreateDialogue();
        if (converstationPartner != null)
        {
            narrator.sprite = converstationPartner;
        }
        else
        {
            narrator.sprite = joe;
        }
    }

    public void CreateDialogue()
    {
        print( "started dialog");
		dialogueText.text = dialogueLines[dialogueIndex].Replace("*", Environment.NewLine);
        dialoguePanel.SetActive(true);
    }

    public void ContinueDialogue()
    {
        if (dialogueIndex < dialogueLines.Count -1)
        {
            dialogueIndex++;
			dialogueText.text = dialogueLines [dialogueIndex].Replace("*", Environment.NewLine);
        }
        else
        {
            dialoguePanel.SetActive(false);
            print("Dialogue ended");
            Time.timeScale = 1f;
        }
    }
}
