using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour{

    public GameObject dialoguePanel;
	public string[] dialogue;
	public Animator trump;
	public Animator camera;

	public CountDown cd;

	private bool stop = true;
	private int counter;

	// Use this for initialization
	void Start() {
		print("start game");
		camera.SetTrigger ("TV");
		PhilDialogue.Instance.AddNewDialogue(dialogue);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp ("space")) {
			if (dialoguePanel.activeSelf) {
				PhilDialogue.Instance.ContinueDialogue ();

				counter++;
			}
		} else if (!dialoguePanel.activeSelf) {
			camera.SetTrigger ("TV");
			cd.startcounting = true;
			Destroy (this);
		}
		if (counter == 8) {
			if (stop) {
				stop = false;
				print ("Playing door kicking sounds");
				trump.SetTrigger ("Trump at the door");
			}
		}
	}
}