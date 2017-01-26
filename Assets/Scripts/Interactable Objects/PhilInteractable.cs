using UnityEngine;
using System.Collections;

public class PhilInteractable : MonoBehaviour {
    public bool noAnimation = false;

    public virtual void Interact(GameObject player)
    {
        Debug.Log("Interacting with base class.");
    }
}
