using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLastLevel : MonoBehaviour
{
    [Header("Dialogue")]
    public string[] missHardDriveDialogue;
    public string[] hardDriveDialogue;

    protected GameObject playerHand;

    private GameObject levelCompletedPanel;
    
    void Awake()
    {
        if (GameObject.Find("MainMenuCanvas") != null)
        {
            levelCompletedPanel = GameObject.Find("MainMenuCanvas").gameObject.transform.FindChild("Level Completed Panel").gameObject;
        }
    }
    void Start()
    {
        playerHand = PhilMovement.hand;

        if (missHardDriveDialogue.Length == 0)
        {
            missHardDriveDialogue = new string[1];
            missHardDriveDialogue[0] = "Money for nothing.";
        }
        if (hardDriveDialogue.Length == 0)
        {
            hardDriveDialogue = new string[1];
            hardDriveDialogue[0] = "That ain't working.";
        }
    }

    void OnTriggerEnter(Collider other)
    {
        print(other.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            if (playerHand.transform.childCount != 0)
            {
                if (playerHand.transform.GetChild(0).GetComponent<PickUpAble>().name.Equals("Harddrive"))
                {
                    int items = GameObject.FindWithTag("Player").transform.FindChild("Inventory").childCount + 1;
                    Time.timeScale = 0.0f;
                    string timeLeftString = GameObject.Find("CountdownText").gameObject.transform.FindChild("TimeText").GetComponent<Text>().text;
                    float timeLeft = GameObject.Find("CountdownKeeper").GetComponent<CountDown>().tijd;

                    levelCompletedPanel.GetComponent<CalculateScore>().timeBool = true;
                    levelCompletedPanel.GetComponent<CalculateScore>().itemBool = true;
                    levelCompletedPanel.GetComponent<CalculateScore>().timeLeft = (int)timeLeft;
                    levelCompletedPanel.GetComponent<CalculateScore>().items = items;
                    levelCompletedPanel.SetActive(true);
                    
                }
                else
                {
                    PhilDialogue.Instance.AddNewDialogue(this.missHardDriveDialogue);
                }
            }
            else
            {
                PhilDialogue.Instance.AddNewDialogue(this.missHardDriveDialogue);
            }
        }
    }

}
