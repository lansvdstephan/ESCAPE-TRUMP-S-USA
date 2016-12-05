using UnityEngine;
using System.Collections;

public class Look_at_pictures : PhilInteractable {
	
	public Animator anim; 
	public string[] dialogue = new string[1];

	public bool ready = false;
	private int goBush = Animator.StringToHash("Bush");

	public override void Interact(GameObject Interacted)
	{
		if (!PhilDialogue.Instance.dialoguePanel.activeSelf)
		{
			print("hint");
			anim.SetTrigger (goBush);
			ready = true;
			PhilDialogue.Instance.AddNewDialogue(dialogue);

		}
		else if (ready)
		{
			PhilDialogue.Instance.ContinueDialogue();
			anim.SetTrigger (goBush);
			ready = false;
		}
		else 
		{
			PhilDialogue.Instance.ContinueDialogue();
		}

	}
}

