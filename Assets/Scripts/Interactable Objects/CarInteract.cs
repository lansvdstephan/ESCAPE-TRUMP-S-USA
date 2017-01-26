using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarInteract : PhilInteractable {
	public string name;
    public bool unlocked = true;
    public int keyCode = 0;

    public string[] lockedDialogue;
    public string[] unLockedDialogue;
    public string[] wrongKey;

    private GameObject levelCompletedPanel;
    
    void Awake()
    {
        if (GameObject.Find("MainMenuCanvas") != null)
        {
            levelCompletedPanel = GameObject.Find("MainMenuCanvas").gameObject.transform.FindChild("Level Completed Panel").gameObject;
        }
        
        if (lockedDialogue.Length == 0)
        {
            lockedDialogue = new string[1];
            lockedDialogue[0] = "It is locked.";
        }
        if (unLockedDialogue.Length == 0)
        {
            unLockedDialogue = new string[1];
            unLockedDialogue[0] = "It is unlocked.";
        }
        if (wrongKey.Length == 0)
        {
            wrongKey = new string[1];
            wrongKey[0] = "This is the wrong key.";
        }
    }


		
	public void NextLevel () {
        int items = GameObject.FindWithTag("Player").transform.FindChild("Inventory").childCount + 1;
        Time.timeScale = 0.0f;
        float timeLeft = GameObject.Find("CountdownKeeper").GetComponent<CountDown>().tijd;
        int healthLeft = GameObject.FindWithTag("Player").GetComponent<PhilMovement>().health;

        levelCompletedPanel.GetComponent<CalculateScore>().timeBool = true;
        levelCompletedPanel.GetComponent<CalculateScore>().itemBool = true;
        levelCompletedPanel.GetComponent<CalculateScore>().healthBool = true;
        levelCompletedPanel.GetComponent<CalculateScore>().timeLeft = (int)timeLeft;
        levelCompletedPanel.GetComponent<CalculateScore>().items = items;
        levelCompletedPanel.GetComponent<CalculateScore>().health = healthLeft;
        levelCompletedPanel.SetActive(true);
	}

    public override void Interact(GameObject player)
    {

        if (!PhilDialogue.Instance.dialoguePanel.activeSelf)
        {
        	if (PhilMovement.hand.transform.childCount != 0 && PhilMovement.hand.transform.GetChild(0).GetComponent<Key>() != null)
            {
                if (PhilMovement.hand.transform.GetChild(0).GetComponent<Key>().keyCode == this.keyCode)
                {
                    unlocked = true;
					PlayerDataForServer.Unlocked (name);
                    PhilDialogue.Instance.AddNewDialogue(unLockedDialogue);
					player.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
					player.transform.parent = this.transform;
					this.GetComponent<Animator>().SetTrigger("Drive Away");
					transform.FindChild ("Lights and smoke").gameObject.SetActive (true);
                }
                else PhilDialogue.Instance.AddNewDialogue(wrongKey);
            }
            else PhilDialogue.Instance.AddNewDialogue(lockedDialogue);
        }
        else
        {
            PhilDialogue.Instance.ContinueDialogue();
        }
    }
}
