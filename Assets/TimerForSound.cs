using UnityEngine;
using System.Collections;

public class TimerForSound : MonoBehaviour {

	public Animator alarm;
	public float timer;

	private bool updateOn = true;

	void Update(){
		if (updateOn) {
			timer -= Time.deltaTime;
			if (timer <= 0.0f) {
				TimerEnded ();
			}
		}
	}

	void TimerEnded () {
		alarm.SetTrigger ("Alarm");
		updateOn = false;
	}
}