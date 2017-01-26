using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadHighscores : MonoBehaviour {
    public GameObject HighScorePanel;
    public void StartGlobalHighscores()
    {
        HighScorePanel.GetComponent<HighScoreController>().startGetScores();
    }

    public void StartLocalHighscores()
    {
        HighScorePanel.GetComponent<HighScoreController>().startGetLocalScores();
    }
}
