using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicAtTime : MonoBehaviour {

	private AudioSource audio;
	public float startTime;
	public float stopTime;

	// Use this for initialization
	void Start () {
		audio = this.GetComponent<AudioSource> ();
		audio.time = startTime;
		audio.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (audio.time > stopTime) {
			Destroy (gameObject);
		}
	}
}

