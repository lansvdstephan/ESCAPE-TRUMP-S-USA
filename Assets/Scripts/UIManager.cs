using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    public GameObject mainMenuPanel;
    public GameObject gameOverPanel;
    public GameObject pausePanel;
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

        DontDestroyOnLoad(gameObject);
    }

// Update is called once per frame
void Update () {
        PauseGame(isPaused);
        if (Input.GetButtonDown("Cancel")&&mainMenuPanel.activeSelf==false)
        {
            SwitchPause();
        }
        if (GameObject.FindWithTag("Player").GetComponent<PhilMovement>().health == 0/*||GameObject.FindWithTag("Player").GetComponent<Movement>().health==0|| GameObject.FindWithTag("Player").GetComponent<Movement>().fuel==0*/)
        {
            GameOver(true);
        }
        
	
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
        pausePanel.SetActive(state);
    }

    public void SwitchPause()
    {
        isPaused = !isPaused;
    }

    public void GameOver(bool a)
    {
        if (a == true)
        {
            Time.timeScale = 0.0f;
            gameOverPanel.SetActive(true);
        }
        else
        {
            GameObject.FindWithTag("Player").GetComponent<PhilMovement>().health = 100;
            gameOverPanel.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }


}
