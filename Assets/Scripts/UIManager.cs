using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

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
        if (Input.GetButtonDown("Cancel"))
        {
            SwitchPause();
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
}
