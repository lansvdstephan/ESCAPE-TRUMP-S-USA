using UnityEngine;
using System.Collections;

public class PhoneDailing : PhilInteractable {

	public string[] dialogue;
	public override void Interact(GameObject Interacted)
	{
		if (!PhilDialogue.Instance.dialoguePanel.activeSelf)
		{
			print("hint");
			if (dialogue != null) PhilDialogue.Instance.AddNewDialogue(dialogue);
			GetComponent<AudioSource>().Play();
		}
		else
		{
			PhilDialogue.Instance.ContinueDialogue();
		}

	}
}
