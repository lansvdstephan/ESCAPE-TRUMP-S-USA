using UnityEngine;
using UnityEngine.Analytics;
using System.Collections;
using System.Collections.Generic;

public class PlayerDataForServer : MonoBehaviour {
	
	private static int counter;
	private static float timer;
	private bool secondLevelGameOver;
	private bool thirdLevelGameOver;


	public int spaces;
	public float timeSpent;

	public bool startGame = false;
	public bool firstLevelComplete = false;
	public bool secondLevelComplete = false;
	public bool thirdLevelComplete = false;
	public bool fourthLevelComplete = false;


	// Use this for initialization
	void awake () {
		DontDestroyOnLoad (this.gameObject);
		secondLevelGameOver = false;
		thirdLevelGameOver = false;
	}
	
	// Update is called once per frame
	void Update () {
		spaces = counter;
		timeSpent = timer;

		if (Application.loadedLevel == 1) {
			if (!startGame) {
				startGame = true;
			}
		}

		if (startGame){
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

				print ("First level Conmplete, Spaces: "+ counter + ", Time: "+ timer);

				Analytics.CustomEvent ("First Level complete", new Dictionary<string, object> {
					{ "spaces", counter },
					{ "time",  timer    }
				});
				timer = 0f;
				counter = 0;
			}
		}
		//Send after level two is complete
		if (Application.loadedLevel == 3) {
			if (!secondLevelComplete) {
				secondLevelComplete = true;

			print ("Second level Conmplete, Spaces: "+ counter + ", Time: "+ timer);

			Analytics.CustomEvent("Second Level complete", new Dictionary<string, object>
				{
					{ "spaces", counter },
					{ "time",  timer    }
				});
				timer = 0f;
				counter = 0;
			}
		}
		if (GameObject.FindWithTag ("Player") != null) {
				
				if (GameObject.FindWithTag ("Player").GetComponent<PhilMovement> () != null) {
					if (GameObject.FindWithTag ("Player").GetComponent<PhilMovement> ().health <= 0) {
						if (!secondLevelGameOver) {
							secondLevelGameOver = true;
							print ("Second level Game-Over, Spaces: " + counter + ", Time: " + timer);
							Analytics.CustomEvent ("Second Level Game-Over", new Dictionary<string, object> {
								{ "spaces", counter },
								{ "time",  timer    }
							});
							timer = 0f;
							counter = 0;
						}  
					} else {
						secondLevelGameOver = false;
					}
				} else if (GameObject.FindWithTag ("Player").GetComponent<Movement> () != null) {
					if (GameObject.FindWithTag ("Player").GetComponent<Movement> ().health <= 0 || GameObject.FindWithTag ("Player").GetComponent<Movement> ().fuel == 0) {
						if (!thirdLevelGameOver) {
							thirdLevelGameOver = true;
							float points = GameObject.FindWithTag ("Player").GetComponent<Movement> ().points;
							print ("Third level Game-Over, Distance: " + points + ", Time: " + timer);
							Analytics.CustomEvent ("Third Level Game-Over", new Dictionary<string, object> {
								{ "distance", points },
								{ "time",  timer     }
							});
							timer = 0f;
						}
					} else {
						thirdLevelGameOver = false;
					}
				}
			}	
		}
	}
}