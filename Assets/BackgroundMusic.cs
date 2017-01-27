using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {

    private GameObject SoundManager;
    public AudioClip clip;
	// Use this for initialization
	void Awake () {
        if (GameObject.Find("MainMenuCanvas") != null)
        {
            SoundManager = GameObject.Find("MainMenuCanvas").transform.FindChild("SoundManager").gameObject;
            SoundManager.GetComponent<SoundManager>().PlayMusic(clip);
        }
	}
	
}
