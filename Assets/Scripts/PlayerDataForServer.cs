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

		//When first level is loaded start counting and timer
		StartGame ();


		//Send after level one is complete
		if (Application.loadedLevel == 2) {
			if (!firstLevelComplete) {
				LevelComplete (1);
			}
		}
		//Send after level two is complete
		if (Application.loadedLevel == 3) {
			if (!secondLevelComplete) {
				LevelComplete (2);
			}
		}

		//If dead send info about death
		if (GameObject.FindWithTag ("Player") != null) {
				
				if (GameObject.FindWithTag ("Player").GetComponent<PhilMovement> () != null) {
					if (GameObject.FindWithTag ("Player").GetComponent<PhilMovement> ().health <= 0) {
						if (!secondLevelGameOver) {
							GameOver (2);
						}
					} else {
						secondLevelGameOver = false;
					}
				} else if (GameObject.FindWithTag ("Player").GetComponent<Movement> () != null) {
					if (GameObject.FindWithTag ("Player").GetComponent<Movement> ().health <= 0 || GameObject.FindWithTag ("Player").GetComponent<Movement> ().fuel == 0) {
						if (!thirdLevelGameOver) {
							GameOver (3);
						}
					} else {
						thirdLevelGameOver = false;
					}
				}
			}	
		}

	void StartGame() {
		//If first level is loaded start Counting
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
		}
	}

	void ResetTimerAndCounter(){
		timer = 0f;
		counter = 0;
	}
		
	void LevelComplete(int level) {
		//make string to use for server
		string levelString = "";

		//set boolean and string
		if (level == 1) {
			firstLevelComplete = true;
			levelString = "First Level complete";
		}
		if (level == 2) {
			secondLevelComplete = true;
			levelString = "Second Level complete";
		}
		if (level == 3) {
			thirdLevelComplete = true;
			levelString = "Third Level complete";
		}

		//Print to check if working
		print (levelString + ", Spaces: "+ counter + ", Time: "+ timer);

		//Post to unity analytics
		Analytics.CustomEvent(levelString, new Dictionary<string, object>
			{
				{ "spaces", counter },
				{ "time",  timer    }
			});
		
		//Reset timer and counter
		ResetTimerAndCounter ();
	}

	void GameOver (int level) {
		if (level == 2) {
			secondLevelGameOver = true;
			print ("Second level Game-Over, Spaces: " + counter + ", Time: " + timer);
			Analytics.CustomEvent ("Second Level Game-Over", new Dictionary<string, object> {
				{ "spaces", counter },
				{ "time",  timer    },
				{ "location x", Mathf.Round(PhilMovement.player.transform.position.x)},
				{ "location z", Mathf.Round(PhilMovement.player.transform.position.z)}
			});
			ResetTimerAndCounter ();
		}  

		if (level == 3) {
			thirdLevelGameOver = true;
			float points = GameObject.FindWithTag ("Player").GetComponent<Movement> ().points;
			print ("Third level Game-Over, Distance: " + points + ", Time: " + timer);
			Analytics.CustomEvent ("Third Level Game-Over", new Dictionary<string, object> {
				{ "distance", points },
				{ "time",  timer     },
				{ "location x", Mathf.Round(PhilMovement.player.transform.position.x)},
				{ "location z", Mathf.Round(PhilMovement.player.transform.position.z)}
			});
			ResetTimerAndCounter ();
		}
	}

	public static void FoundItem (string name) {
		print ("Found Item: " + name + ", Time: " + timer);
		Analytics.CustomEvent ("Found Item: " + name, new Dictionary<string, object> {
			{"time", timer}
		});
	}

	public static void Unlocked (string name) {
		print ("Unlocked: " + name + ", Time: " + timer);
		Analytics.CustomEvent ("Unlocked: " + name, new Dictionary<string, object> {
			{"time", timer}
		});
	}
		
}