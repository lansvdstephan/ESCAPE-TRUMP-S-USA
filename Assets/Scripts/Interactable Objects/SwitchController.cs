using UnityEngine;
using System.Collections;

public class SwitchController : PhilInteractable {

    public GameObject switched;
    public bool unlocked = true;

    private bool switchFlipped = false;
    private string[] dialogue;

    void Awake()
    {
        dialogue = new string[1];
        dialogue[0] = "It is locked";
    }

    public override void Interact(GameObject interacted)
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
        else
        {
            PhilDialogue.Instance.AddNewDialogue(dialogue);
        }


    }
}
