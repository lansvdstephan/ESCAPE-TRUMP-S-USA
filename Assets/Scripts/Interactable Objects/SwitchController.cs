using UnityEngine;
using System.Collections;

public class SwitchController : PhilInteractable {

    public GameObject switched;
    public bool unlocked = true;
    public int keyCode = 00; 
    public bool switchFlipped = false;
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
                    switched.GetComponent<Switchable>().SwitchOn();
                }
                else
                {
                    switched.GetComponent<Switchable>().SwitchOff();
                }
            }
            else if (Player.transform.FindChild("Hand").childCount != 0 && Player.transform.FindChild("Hand").GetChild(0).GetComponent<Key>() != null)
            {
                if (Player.transform.FindChild("Hand").GetChild(0).GetComponent<Key>().keyCode == this.keyCode)
                {
                    unlocked = true;
                    switched.GetComponent<Switchable>().SwitchOn();
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

	public void Lock(){
		unlocked = false;
	}
}
