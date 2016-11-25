using UnityEngine;
using System.Collections;

public class SwitchController : PhilInteractable {

    private bool switchFlipped = false;
    public GameObject switched;

    public override void Interact(GameObject interacted)
    {
        print("puched switch");
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
}
