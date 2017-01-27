using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginScript : MonoBehaviour {

    private string secretKey = "1dLc9f170GIfi0cdgm5QW267J91tMqM7";
    public GameObject Username_Field;
    public GameObject Password_Field;
    public GameObject TitleText;
    public GameObject HighScorePanel;
    public GameObject LoginPanel;
    public GameObject StartButtonNonLogin;
    public GameObject StartButton;
    public string username;
    private string password;
    private string logInURL = "https://insyprojects.ewi.tudelft.nl/ewi3620tu6/login.php?";

    // Use this for initialization
    void OnEnable () {
        TitleText.GetComponent<Text>().text = "Please Log In";
        username = Username_Field.GetComponent<InputField>().text;
        password = Password_Field.GetComponent<InputField>().text;
        StartCoroutine(LogIn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public IEnumerator LogIn()
    {
        string hash = Md5Sum(username + password + secretKey);
        string postURL = logInURL + "username=" + username + "&password=" + password + "&hash=" + hash;
        WWW post = new WWW(postURL);
        yield return post;
        string data = post.text;
        print(string.CompareOrdinal(data, "Logged in succesfully"));
        print(data);
        if (post.error != null)
        {
            print("Error Logging in:" + post.error);
            this.enabled = false;
        }
        else if(string.CompareOrdinal(data, "Logged in succesfully")==1){
            TitleText.GetComponent<Text>().text = "Logged in succesfully.";
            HighScorePanel.GetComponent<HighScoreController>().playerName = username;
            this.enabled = false;
            StartButtonNonLogin.SetActive(false);
            StartButton.SetActive(true);
            yield return new WaitForSeconds(1f);
            LoginPanel.SetActive(false);
        }
        else
        {
            TitleText.GetComponent<Text>().text = "Invalid Username/Password.";
            Password_Field.GetComponent<InputField>().text = "";
            this.enabled = false;
        }
    }

    public string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }

}
