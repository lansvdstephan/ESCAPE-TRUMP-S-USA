using UnityEngine;
using System.Collections;

public class PhilInteractable : MonoBehaviour {

    public virtual void Interact(GameObject interacted)
    {
        Debug.Log("Interacting with base class.");
    }
}
