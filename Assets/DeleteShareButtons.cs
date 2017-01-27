using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteShareButtons : MonoBehaviour {

    public GameObject ShareOnTwitterButton;
    public GameObject ShareOnFacebookButton;
    public GameObject HighScorePanel;

    void OnEnable()
	{
		if (!HighScorePanel.GetComponent<HighScoreController> ().online) {
			ShareOnTwitterButton.SetActive (false);
			ShareOnFacebookButton.SetActive (false);
		} else {
			ShareOnTwitterButton.SetActive (true);
			ShareOnFacebookButton.SetActive (true);
		}
	}
}
