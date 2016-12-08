using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour{

	public string[] dialogue;
	public GameObject dialoguePanel;

	// Use this for initialization
	void Start() {
		print("start game");
		PhilDialogue.Instance.AddNewDialogue(dialogue);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp ("space")) {
			if (dialoguePanel.activeSelf) {
				PhilDialogue.Instance.ContinueDialogue ();
			}
		} else if (!dialoguePanel.activeSelf) {
			Destroy (this);
		}
	}
}