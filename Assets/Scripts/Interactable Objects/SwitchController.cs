using UnityEngine;
using System.Collections;

public class SwitchController : PhilInteractable {

    public GameObject switched;
	public string name;
    public bool unlocked = true;
    public int keyCode = 00; 
    public bool switchFlipped = false;
	public string[] lockedDialogue;
   	public string[] unLockedDialogue;
    public string[] pushedOnDialogue;
    public string[] pushedOffDialogue;
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

    public override void Interact(GameObject Player)
    {
        if (!PhilDialogue.Instance.dialoguePanel.activeSelf)
        {
            print("puched switch");
            if (unlocked)
            {
                switchFlipped = !switchFlipped;
                if (switchFlipped)
                {
                    if (pushedOnDialogue.Length != 0)
                    {
						PhilDialogue.Instance.AddNewDialogue(pushedOnDialogue,dialogueSprite);
                    }
                    switched.GetComponent<Switchable>().SwitchOn();
                }
                else
                {
                    if (pushedOnDialogue.Length != 0)
                    {
                        PhilDialogue.Instance.AddNewDialogue(pushedOffDialogue);
                    }
                    switched.GetComponent<Switchable>().SwitchOff();
                }
            }
            else if (PhilMovement.hand.transform.childCount != 0 && PhilMovement.hand.transform.GetChild(0).GetComponent<Key>() != null)
            {
                if (PhilMovement.hand.transform.GetChild(0).GetComponent<Key>().keyCode == this.keyCode)
                {
                    unlocked = true;
					PlayerDataForServer.Unlocked (name);
                    switched.GetComponent<Switchable>().SwitchOn();
					if (this.name.Equals ("Elevator")) {
						PhilDialogue.Instance.AddNewDialogue (unLockedDialogue,dialogueSprite,true);
					} 
					else {
						PhilDialogue.Instance.AddNewDialogue (unLockedDialogue);
					}
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

	public void Lock(){
		unlocked = false;
	}
}
