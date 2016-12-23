using UnityEngine;
using System.Collections;

public class PlayerDataForServer : MonoBehaviour {
	public int counter;
	public float timer;


	// Use this for initialization
	void awake () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		if (Input.GetKeyUp ("space")) {
			counter++;
		}
//		if (Application.loadedLevel == 2){
//			Analytics.CustomEvent("First Level", new Dictionary<string, object>
//				{
//					{ "spaces", counter },
//					{ "time",  timer    }
//				});
//		}
//		if (Application.loadedLevel == 3){
//			Analytics.CustomEvent("Second Level", new Dictionary<string, object>
//				{
//					{ "spaces", counter },
//					{ "time",  timer    }
//				});
		}
	}


				