using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;


public class UIManager : MonoBehaviour {
	UIManager uimanager;

	public MenuPanel activePanel;
	public MenuPanel mainMenuPanel;
	public MenuPanel gameOverPanel;
	public MenuPanel pausePanel;
    public bool isPaused;
    public static UIManager instance = null;

	private EventSystem eventSystem;


	// Use this for initialization
	void Start () {
        isPaused = false;

		this.eventSystem = EventSystem.current;
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

		// When TAB is pressed, we should select the next selectable UI element
		if (Input.GetKeyDown(KeyCode.Tab)) {
			Selectable next = null;
			Selectable current = null;

			// Figure out if we have a valid current selected gameobject
			if (eventSystem.currentSelectedGameObject != null) {
				// Unity doesn't seem to "deselect" an object that is made inactive
				if (eventSystem.currentSelectedGameObject.activeInHierarchy) {
					current = eventSystem.currentSelectedGameObject.GetComponent<Selectable>();
				}
			}

			if (current != null) {
				// When SHIFT is held along with tab, go backwards instead of forwards
				if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
						next = current.FindSelectableOnUp();
						print ("next on up");
				} else {
						next = current.FindSelectableOnDown();
						print ("Next on down");
				}
			} else {
				// If there is no current selected gameobject, select the first one
				if (Selectable.allSelectables.Count > 0) {
					next = Selectable.allSelectables[0];
					print ("no selectable");
				}
			}

			if (next != null)  {
				next.Select();
				print ("Next inputfiled selected");
			}
		}
		
        // PauseGame(isPaused);
		if (Input.GetKeyDown("escape")||Input.GetButtonDown("Cancel") && activePanel.panelName == "pause" && !gameOverPanel.enabled)
        {
            SwitchPause();

        }
		if (GameObject.FindWithTag ("Player") != null) {
			if (GameObject.FindWithTag ("Player").GetComponent<PhilMovement> () != null) {
				if (GameObject.FindWithTag ("Player").GetComponent<PhilMovement> ().health <= 0/*||GameObject.FindWithTag("Player").GetComponent<Movement>().health==0|| GameObject.FindWithTag("Player").GetComponent<Movement>().fuel==0*/) {
					GameOver (true);
				}
			}
            if (GameObject.FindWithTag("Count Down") != null)
            {
                if (GameObject.FindWithTag("Count Down").GetComponent<CountDown>().tijd == 0f)
                {
                    GameOver(true);
                    GameObject.FindWithTag("Count Down").GetComponent<CountDown>().tijd = 120f;
                }
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
        if (a)
        {
            Time.timeScale = 0.0f;
			gameOverPanel.gameObject.SetActive(true);
        }
        else
        {
			if (GameObject.FindWithTag ("Player").GetComponent<PhilMovement> () != null) {
				GameObject.FindWithTag("Player").GetComponent<PhilMovement>().health = 100;
			} else if (GameObject.FindWithTag ("Player").GetComponent<Movement> () != null) {
				GameObject.FindWithTag("Player").GetComponent<Movement>().health = 1000;
				GameObject.FindWithTag ("Player").GetComponent<Movement> ().fuel = 100;
			}
             
			gameOverPanel.gameObject.SetActive(false);
            Time.timeScale = 1.0f;
        }
    }


}
