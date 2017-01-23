using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogueAtStart : MonoBehaviour {
    public string[] lines;

    void Start () {
        Time.timeScale = 0.0f;
        PhilDialogue.Instance.AddNewDialogue(lines); 
	}

	void Update () {
        if (PhilDialogue.Instance.dialoguePanel.activeSelf)
        {
            if (Input.GetKeyUp("space"))
            {
                PhilDialogue.Instance.ContinueDialogue();
            }
        }
        else if (GameObject.Find("CountdownKeeper") != null)
        {
            GameObject.Find("CountdownKeeper").GetComponent<CountDown>().startcounting = true;
            Time.timeScale = 1.0f;
            Destroy(this.gameObject);
        }
        else
        { 
            Time.timeScale = 1.0f;
            Destroy(this.gameObject);
        }
	}
   
}
