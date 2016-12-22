using UnityEngine;
using System.Collections;

public class PlayerDataForServer : MonoBehaviour {
	public int counter;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp ("space")) {
			counter++;
		}
	}
}
