using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour {

	public MovieTexture movTexture;
	public Animator alarm;

	public bool playing = true;

	private bool updateOn = true;

	// Use this for initialization
	void Start () {
		movTexture.Play();
	}
	
	// Update is called once per frame
	void Update(){
		if (updateOn) {
			if (!movTexture.isPlaying) {
				playing = false;
				MovieEnded ();
			}
		} else
			Destroy (this);
		}

	void MovieEnded () {
		alarm.SetTrigger ("Alarm");
		updateOn = false;
		}
	
}

