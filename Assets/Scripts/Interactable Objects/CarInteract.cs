﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CarInteract : PhilInteractable {
	public string name;
    public bool unlocked = true;
    public int keyCode = 0;



    public string[] lockedDialogue;
    public string[] unLockedDialogue;
    public string[] wrongKey;


    void Awake()
    {
        if (lockedDialogue.Length == 0)
        {
            lockedDialogue = new string[1];
            lockedDialogue[0] = "It is locked.";
        }
        if (unLockedDialogue.Length == 0)
        {
            unLockedDialogue = new string[1];
            unLockedDialogue[0] = "It is unlocked.";
        }
        if (wrongKey.Length == 0)
        {
            wrongKey = new string[1];
            wrongKey[0] = "This is the wrong key.";
        }
    }


		
	public void NextLevel () {
		SceneManager.LoadScene("Driving level");
	}

    public override void Interact(GameObject player)
    {

        if (!PhilDialogue.Instance.dialoguePanel.activeSelf)
        {
            if (unlocked)
            {
				player.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
				player.transform.parent = this.transform;
                this.GetComponent<Animator>().SetTrigger("Drive Away");
				transform.FindChild ("Lights and smoke").gameObject.SetActive (true);
            }
            else if (PhilMovement.hand.transform.childCount != 0 && PhilMovement.hand.transform.GetChild(0).GetComponent<Key>() != null)
            {
                if (PhilMovement.hand.transform.GetChild(0).GetComponent<Key>().keyCode == this.keyCode)
                {
                    unlocked = true;
					PlayerDataForServer.Unlocked (name);
                    PhilDialogue.Instance.AddNewDialogue(unLockedDialogue);
                }
                else PhilDialogue.Instance.AddNewDialogue(wrongKey);
            }
            else PhilDialogue.Instance.AddNewDialogue(lockedDialogue);
        }
        else
        {
            PhilDialogue.Instance.ContinueDialogue();
        }
    }
}
