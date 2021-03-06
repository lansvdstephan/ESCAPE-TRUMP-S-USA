﻿using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour{

    public GameObject dialoguePanel;
	public string[] dialogue;
    public Sprite[] dialogueSprites;
	public Animator trump;
	public Animator camera;

	public CountDown cd;

	private bool stop = true;
	private int counter = 0;
	// Use this for initialization
	void Start() {
		print("start game");
		camera.SetTrigger ("TV");
        PhilMovement.player.GetComponent<PhilMovement>().animOn = true;
        PhilDialogue.Instance.AddNewDialogue(dialogue, dialogueSprites[counter], true);
        
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyUp ("space")) {
			if (dialoguePanel.activeSelf)
            { 
				counter++;
                if (counter < dialogueSprites.Length)
                {
                    PhilDialogue.Instance.ContinueDialogue(dialogueSprites[counter]);
                }
                else
                {
                    PhilDialogue.Instance.ContinueDialogue();
                }
            }
		} else if (!dialoguePanel.activeSelf) {
			camera.SetTrigger ("TV");
			cd.startcounting = true;
            PhilMovement.player.GetComponent<PhilMovement>().animOn = false;
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