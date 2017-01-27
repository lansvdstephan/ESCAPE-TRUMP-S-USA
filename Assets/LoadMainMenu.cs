using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour {


    private GameObject LevelCompletedPanel;
    public GameObject ThisPanel;
    public GameObject StartButton;
    public GameObject StartButtonNonLogin;
    public GameObject UIManager;
    public GameObject MainMenuPanel;
    public GameObject SoundManager;
    public AudioClip MainMenuMusic;
	// Use this for initialization
    void Awake()
    {
        LevelCompletedPanel = GameObject.Find("MainMenuCanvas").transform.FindChild("Level Completed Panel").gameObject;
    }

    public void BackToMainMenu()
    {
        UIManager.GetComponent<PlayerDataForServer>().startGame=false;
        LevelCompletedPanel.GetComponent<CalculateScore>().currentScene = 1;
        ThisPanel.SetActive(false);
        StartButton.SetActive(true);
        StartButtonNonLogin.SetActive(false);
        MainMenuPanel.SetActive(true);
        SoundManager.GetComponent<SoundManager>().PlayMusic(MainMenuMusic);
        SceneManager.LoadScene(0);
    }
}
