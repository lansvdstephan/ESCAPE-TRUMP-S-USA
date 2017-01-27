using UnityEngine;
using System.Collections;

public class PhoneDailing : PhilInteractable {

	public string[] dialogue;
    public Sprite obama;

	public override void Interact(GameObject Interacted)
	{
		if (!PhilDialogue.Instance.dialoguePanel.activeSelf)
		{
            PhilDialogue.Instance.joe = obama;
			print("hint");
			GetComponent<AudioSource>().Play();
			if (dialogue != null) PhilDialogue.Instance.AddNewDialogue(dialogue);
		}
		else
		{
			PhilDialogue.Instance.ContinueDialogue();
		}

	}
}
