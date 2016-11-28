using UnityEngine;
using System.Collections;

public class GiveHint : PhilInteractable {
    
    public string[] dialogue;
    public override void Interact(GameObject Interacted)
    {
        if (!PhilDialogue.Instance.dialoguePanel.activeSelf)
        {
            print("hint");
            PhilDialogue.Instance.AddNewDialogue(dialogue);
        }
        else
        {
            PhilDialogue.Instance.ContinueDialogue();
        }
        
    }
}
