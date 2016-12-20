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

	// Use this for initialization
	public override void SwitchOn() {
		controller.tag = "Untagged";
		anim.SetTrigger (goUpHash);
	}

	public IEnumerator NextLevel(){
		float sec = 2f;
		yield return new WaitForSeconds (sec);
		PhilMovement.player.SetActive(false);
		yield return new WaitForSeconds (sec);
		SceneManager.LoadScene(nextLevelName);
	}

	public override void SwitchOff() {
		player.GetComponent<PhilMovement>().enabled = false;
		player.GetComponent<Animator> ().SetBool ("Walking", false);
		anim.SetTrigger (goDownHash);
		StartCoroutine(NextLevel());
	}
}
