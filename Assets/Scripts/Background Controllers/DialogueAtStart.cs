using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DialogueAtStart : MonoBehaviour {

    public string[] lines;
    public float tijd;
    public Text timeText;
    private float minutes;
    private float seconds;
    private int mseconds;

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
        else
        {
            Debug.Log("End");
            Destroy(this);
            Time.timeScale = 1.0f;
            tijd = tijd - Time.deltaTime;
            setTijdText();
        }
	}
    private void setTijdText()
    {
        minutes = tijd / 60f;
        minutes = Mathf.Floor(minutes);
        seconds = Mathf.Floor(tijd - (minutes * 60));
        mseconds = Random.Range(1, 100);
        Debug.Log(mseconds);
        timeText.text = minutes + ":" + seconds + ":" + mseconds;
    }
}
