using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class CodeDoorController : SwitchController {
    public GameObject inputPanel;
    private string code = "0000";

    private InputField getCode;

    void Awake()
    {
        getCode = inputPanel.transform.FindChild("Door Code entry").GetComponent<InputField>();
    }

	void Start()
	{
		code = PlayerDataForServer.doorCodeLevelTwo;

		if (code == null) {
			code = "1946";
		}
		print ("Door: " + code);
	}

    public override void Interact(GameObject Player)
    {
        if (!PhilDialogue.Instance.dialoguePanel.activeSelf)
        {
            InputSystem.Instance.inputPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(getCode.gameObject);
        }
        else
        {
            PhilDialogue.Instance.ContinueDialogue();
        }
    }

    public void CodeChecker()
    {
        if (getCode.text.Equals(code))
        {
            switched.GetComponent<Switchable>().SwitchOn();
			PlayerDataForServer.Unlocked (name);
            InputSystem.Instance.inputPanel.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            InputSystem.Instance.inputPanel.SetActive(false);
            if(wrongKey.Length !=0) PhilDialogue.Instance.AddNewDialogue(wrongKey);
            Time.timeScale = 1f;
        }
    }

}
