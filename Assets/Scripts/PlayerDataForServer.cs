using UnityEngine;
using UnityEngine.Analytics;
using System.Collections;
using System.Collections.Generic;

public class PlayerDataForServer : MonoBehaviour {
	
	private static int counter;
	private static float timer;

	public bool firstLevelComplete = false;
	public bool secondLevelComplete = false;
	public bool thirdLevelComplete = false;
	public bool fourthLevelComplete = false;


	// Use this for initialization
	void awake () {
		DontDestroyOnLoad (this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		//Increment timer
		timer += Time.deltaTime;
		//When not having a dialogue
		if (!PhilDialogue.Instance.dialoguePanel.activeSelf) {
			if (Input.GetKeyUp ("space")) {
				counter++;
			}
		}
		//Send after level one is complete
		if (Application.loadedLevel == 2) {
			if (!firstLevelComplete) {
				firstLevelComplete = true;

				print ("spaces " + counter + " time " + timer);

				Analytics.CustomEvent ("First Level complete", new Dictionary<string, object> {
					{ "spaces", counter },
					{ "time",  timer    }
				});
			}
		}
		//Send after level two is complete
		if (Application.loadedLevel == 3) {
			if (!secondLevelComplete) {
				secondLevelComplete = true;

			print ("spaces "+ counter + " time "+ timer);

			Analytics.CustomEvent("Second Level complete", new Dictionary<string, object>
				{
					{ "spaces", counter },
					{ "time",  timer    }
				});
			}
		}
			
	}
}


				