using UnityEngine;
using System.Collections;

public class Portal : PhilInteractable {

	public override void Interact(GameObject interacted)
    {
        Debug.Log("Interacting with base class.");
        Application.LoadLevel("Tunnel Oval Office");
    }
}

