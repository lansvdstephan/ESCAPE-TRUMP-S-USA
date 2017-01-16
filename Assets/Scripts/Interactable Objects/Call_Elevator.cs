using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Call_Elevator : Switchable{

	public Animator anim; 
	public GameObject controller;
	public GameObject player;
	public string nextLevelName;
	int goUpHash = Animator.StringToHash("Go up");
	int goDownHash = Animator.StringToHash("Go down");


	void Awake() {
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
		PhilMovement.player.SetActive (false);
	}

	public void NextLevel () {
		SceneManager.LoadScene("Tunnel Oval Office");
	}
}
