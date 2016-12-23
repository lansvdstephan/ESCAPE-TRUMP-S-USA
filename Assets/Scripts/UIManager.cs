﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {
	UIManager uimanager;

	public MenuPanel activePanel;
	public MenuPanel mainMenuPanel;
	public MenuPanel gameOverPanel;
	public MenuPanel pausePanel;
    public bool isPaused;
    public static UIManager instance = null;
	// Use this for initialization
	void Start () {
        isPaused = false;	
	}

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(this);
    }

// Update is called once per frame
	void Update () {
//        PauseGame(isPaused);
		if (Input.GetButtonDown("Cancel") && activePanel.panelName == "pause")
        {
            SwitchPause();

        }
		if (GameObject.FindWithTag ("Player") != null) {
			if (GameObject.FindWithTag ("Player").GetComponent<PhilMovement> ().health == 0/*||GameObject.FindWithTag("Player").GetComponent<Movement>().health==0|| GameObject.FindWithTag("Player").GetComponent<Movement>().fuel==0*/) {
				GameOver (true);
			}
        
		}
	}

	public void SetActivePanel(MenuPanel panel) {
		activePanel = panel;
	}

    void PauseGame(bool state)
    {
        if (state)
        {
            Time.timeScale = 0.0f;  //Paused
        }
        else
        {
            Time.timeScale = 1.0f;  //Unpaused       
        }
        pausePanel.gameObject.SetActive(state);
    }

    public void SwitchPause()
    {
        isPaused = !isPaused;
		activePanel.gameObject.SetActive(isPaused);
		if (isPaused)
		{
			Time.timeScale = 0.0f;  //Paused
		}
		else
		{
			Time.timeScale = 1.0f;  //Unpaused       
		}
		Debug.Log (isPaused);
    }

    public void GameOver(bool a)
    {
        if (a == true)
        {
            Time.timeScale = 0.0f;
			gameOverPanel.gameObject.SetActive(true);
        }
        else
        {
            GameObject.FindWithTag("Player").GetComponent<PhilMovement>().health = 100;
			gameOverPanel.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }


}
