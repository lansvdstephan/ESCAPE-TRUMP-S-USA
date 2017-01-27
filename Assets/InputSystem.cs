using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputSystem : MonoBehaviour {
    public static InputSystem Instance { get; set; }
    public GameObject inputPanel;

    private InputField codeInput;

    void Awake()
    {
        inputPanel.SetActive(false);
        codeInput = inputPanel.GetComponent<InputField>();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void GetInput()
    {
        inputPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
