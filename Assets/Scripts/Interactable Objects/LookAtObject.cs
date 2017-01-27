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
			PhilMovement.player.GetComponent<PhilMovement> ().animOn = true;
			PhilDialogue.Instance.AddNewDialogue(dialogue,dialogueSprite,true);

		}
		else 
		{
			PhilDialogue.Instance.ContinueDialogue();
			anim.SetTrigger (Animator.StringToHash(trigger));
			PhilMovement.player.GetComponent<PhilMovement> ().animOn = false;
		}

	}
}

