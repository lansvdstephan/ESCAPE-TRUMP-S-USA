using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAfterFirstTrigger : MonoBehaviour {
    public string[] dialogue;

    private bool firstTime = true;

	void Start () {
		if (dialogue.Length == 0)
        {
            Destroy(this.gameObject);
        }
	}
	
	void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (firstTime)
            {
                PhilDialogue.Instance.AddNewDialogue(dialogue);
                firstTime = false;
            }
        }
    }
}
