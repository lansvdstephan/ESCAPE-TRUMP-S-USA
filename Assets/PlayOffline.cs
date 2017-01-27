using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayOffline : MonoBehaviour {

    public GameObject HighScorePanel;
    
    public void StartOffline()
    {
        HighScorePanel.GetComponent<HighScoreController>().online = false;
    }
}
