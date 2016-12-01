using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Call_Elevator : Switchable{

	public Animator anim; 
	public GameObject controller;
	public string nextLevelName;
	int goUpHash = Animator.StringToHash("Go up");
	int goDownHash = Animator.StringToHash("Go down");


	void Awake() {
		anim = GetComponent<Animator> ();
	}

	// Use this for initialization
	public override void SwitchOn() {
		controller.GetComponent<SwitchController> ().Lock();
		anim.SetTrigger (goUpHash);
	}

	public IEnumerator NextLevel(){
		float sec = 1.4f;
		yield return new WaitForSeconds (sec);
		PhilMovement.player.SetActive(false);
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene(nextLevelName);
	}

	public override void SwitchOff() {
		anim.SetTrigger (goDownHash);
		StartCoroutine(NextLevel());
	}
}
