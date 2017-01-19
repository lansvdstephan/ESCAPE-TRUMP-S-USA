using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LoadLastLevel : MonoBehaviour
{
    [Header("Dialogue")]
    public string[] missHardDriveDialogue;
    public string[] hardDriveDialogue;

    protected GameObject playerHand;

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
                    SceneManager.LoadScene("Jumper level");
                    PhilDialogue.Instance.AddNewDialogue(this.hardDriveDialogue);
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
