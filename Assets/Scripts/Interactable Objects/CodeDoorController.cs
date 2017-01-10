using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class CodeDoorController : SwitchController {
    public GameObject inputPanel;
    public string code = "0000";

    private InputField getCode;

    void Awake()
    {
        getCode = inputPanel.transform.FindChild("Door Code entry").GetComponent<InputField>();
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
        }
        else
        {
            InputSystem.Instance.inputPanel.SetActive(false);
            if(wrongKey.Length !=0) PhilDialogue.Instance.AddNewDialogue(wrongKey);
        }
    }

}
