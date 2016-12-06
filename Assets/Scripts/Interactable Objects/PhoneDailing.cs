using UnityEngine;
using System.Collections;

public class PhoneDailing : PhilInteractable {

	public string[] dialogue;
	public override void Interact(GameObject Interacted)
	{
		if (!PhilDialogue.Instance.dialoguePanel.activeSelf)
		{
			print("hint");
			GetComponent<AudioSource>().Play();
			PhilDialogue.Instance.AddNewDialogue(dialogue);
		}
		else
		{
			PhilDialogue.Instance.ContinueDialogue();
		}

	}
}
