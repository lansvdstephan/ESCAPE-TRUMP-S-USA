using UnityEngine;
using System.Collections;

public class SwitchController : PhilInteractable {

    public GameObject switched;
    public bool unlocked = true;
    public int keyCode = 00; 
    public bool switchFlipped = false;

    private string[] lockedDialogue;
    private string[] unLockedDialogue;
    private string[] wrongKey;


    void Awake()
    {
        lockedDialogue = new string[1];
        lockedDialogue[0] = "It is locked.";
        unLockedDialogue = new string[1];
        unLockedDialogue[0] = "It is unlocked.";
        wrongKey = new string[1];
        wrongKey[0] = "This is the wrong key.";
    }

    public override void Interact(GameObject Player)
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
        else if(Player.transform.FindChild("Hand").childCount !=0 && Player.transform.FindChild("Hand").GetChild(0).GetComponent<Key>() != null)
        {
            if (Player.transform.FindChild("Hand").GetChild(0).GetComponent<Key>().keyCode == this.keyCode)
            {
                PhilDialogue.Instance.AddNewDialogue(unLockedDialogue);
                unlocked = true;
            }
            else PhilDialogue.Instance.AddNewDialogue(wrongKey);
        }
        else  PhilDialogue.Instance.AddNewDialogue(lockedDialogue);
    }

	public void Lock(){
		unlocked = false;
	}
}
