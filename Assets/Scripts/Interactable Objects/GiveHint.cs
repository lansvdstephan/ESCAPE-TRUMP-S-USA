using UnityEngine;
using System.Collections;

public class GiveHint : PhilInteractable {
    
    public string[] dialogue;

    public override void Interact(GameObject Interacted)
    {
        if (!PhilDialogue.Instance.dialoguePanel.activeSelf)
        {
            if (dialogue != null)
            {
                PhilDialogue.Instance.AddNewDialogue(dialogue, dialogueSprite);
            }
        }
        else
        {
            PhilDialogue.Instance.ContinueDialogue();
        }
        
    }
}
