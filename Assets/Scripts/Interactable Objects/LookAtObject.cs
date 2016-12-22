using UnityEngine;
using System.Collections;

public class LookAtObject : PhilInteractable {
	
	public Animator anim;
	public string trigger;
	public string[] dialogue = new string[1];

	private bool ready = false;

	public override void Interact(GameObject Interacted)
	{
		if (!PhilDialogue.Instance.dialoguePanel.activeSelf)
		{
			print("hint");
			anim.SetTrigger (Animator.StringToHash(trigger));
			ready = true;
			PhilDialogue.Instance.AddNewDialogue(dialogue);

		}
		else 
		{
			PhilDialogue.Instance.ContinueDialogue();
			anim.SetTrigger (Animator.StringToHash(trigger));
		}

	}
}

