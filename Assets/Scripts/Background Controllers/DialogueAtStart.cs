using UnityEngine;
using System.Collections;

public class DialogueAtStart : MonoBehaviour {

    public string[] lines;

    void Start () {
        Time.timeScale = 0.0f;
        PhilDialogue.Instance.AddNewDialogue(lines); 
	}

	void Update () {

        if (PhilDialogue.Instance.dialoguePanel.activeSelf)
        {
            if (Input.GetKeyUp("space"))
            {
                PhilDialogue.Instance.ContinueDialogue();
            }
        }
        else
        {
            Destroy(this);
            Time.timeScale = 1.0f;
        }
	}
}
