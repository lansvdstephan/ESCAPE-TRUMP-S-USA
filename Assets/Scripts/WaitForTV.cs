using UnityEngine;
using System.Collections;

public class WaitForTV : MonoBehaviour {

	public Animator alarm;

	public MovieTexture movTexture;

	private bool updateOn = true;

	void Update(){
		if (updateOn) {
			if (!movTexture.isPlaying){
				TimerEnded ();
			}
		}
	}

	void TimerEnded () {
		alarm.SetTrigger ("Alarm");
		updateOn = false;
	}
}