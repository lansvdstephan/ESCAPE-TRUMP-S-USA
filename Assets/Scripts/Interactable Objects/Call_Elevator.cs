using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Call_Elevator : Switchable{

	public Animator anim; 
	public GameObject controller;
	public GameObject player;
	int goUpHash = Animator.StringToHash("Go up");
	int goDownHash = Animator.StringToHash("Go down");
    public GameObject levelCompletedPanel;

    public int items;



	void Awake() {
        levelCompletedPanel = GameObject.Find("MainMenuCanvas").gameObject.transform.FindChild("Level Completed Panel").gameObject;
        anim = GetComponent<Animator> ();
	}



	public override void SwitchOn() {
		controller.tag = "Untagged";
		anim.SetTrigger (goUpHash);
	}
		
	public override void SwitchOff() {
		player.GetComponent<PhilMovement>().enabled = false;
		player.GetComponent<Animator> ().SetBool ("Walking", false);
		anim.SetTrigger (goDownHash);
	}

	public void HidePlayer () {
        items = GameObject.Find("Obama").transform.FindChild("Inventory").childCount + 1;
        PhilMovement.player.SetActive (false);
    }

	public void NextLevel () {
        Time.timeScale = 0.0f;
        string timeLeftString = GameObject.Find("CountdownText").gameObject.transform.FindChild("TimeText").GetComponent<Text>().text;
        float timeLeft = GameObject.Find("CountdownKeeper").GetComponent<CountDown>().tijd;

        levelCompletedPanel.GetComponent<CalculateScore>().timeBool = true;
        levelCompletedPanel.GetComponent<CalculateScore>().itemBool = true;
        levelCompletedPanel.GetComponent<CalculateScore>().timeLeft = (int) timeLeft;
        levelCompletedPanel.GetComponent<CalculateScore>().items = items;
        levelCompletedPanel.SetActive(true);
    }
}
