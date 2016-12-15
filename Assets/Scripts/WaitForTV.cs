using UnityEngine;
using System.Collections;

public class WaitForTV : MonoBehaviour {

	public Animator alarm;
	public Animator trump;
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
		trump.SetTrigger ("Trump at the door");
		updateOn = false;
	}
}