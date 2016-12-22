using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	public float timer = 180;
	private Text timerText;
	private bool activated = true;



	// Use this for initialization
	void Start () {
		timerText = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (activated) {
			timer -= Time.deltaTime;
			timerText.text = timer.ToString ("f2");
		}
	}
}
