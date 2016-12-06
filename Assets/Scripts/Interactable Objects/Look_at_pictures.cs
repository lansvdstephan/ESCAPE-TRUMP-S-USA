using UnityEngine;
using System.Collections;

public class Look_at_pictures : PhilInteractable {
	public Animator anim; 
	public string[] dialogue = new string[1];
	int goBush = Animator.StringToHash("Bush");

	IEnumerator WaitForKeyDown(KeyCode keyCode)
	{
		while (!Input.GetKeyDown(keyCode))
			yield return null;
	}

	IEnumerator Dialogue() {
			anim.SetTrigger (goBush);
			PhilDialogue.Instance.AddNewDialogue (dialogue);
			yield return StartCoroutine (WaitForKeyDown (KeyCode.Space));
			anim.SetTrigger (goBush);
			PhilDialogue.Instance.ContinueDialogue ();
			anim.SetTrigger (goBush);
	}
		

	public override void Interact(GameObject Interacted)
	{
		
		if (!PhilDialogue.Instance.dialoguePanel.activeSelf)
		{
			Dialogue ();
		}
	}
}

