using UnityEngine;
using System.Collections;

public class PhilInteractable : MonoBehaviour {

    public virtual void Interact(GameObject player)
    {
        Debug.Log("Interacting with base class.");
    }
}
