using UnityEngine;
using System.Collections;

public class PlayMovie1 : MonoBehaviour {

	public MovieTexture movTexture;
	public Animator alarm;

	private bool updateOn = true;

	// Use this for initialization
	void Start () {
		movTexture.Play();
	}
	
	// Update is called once per frame
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

