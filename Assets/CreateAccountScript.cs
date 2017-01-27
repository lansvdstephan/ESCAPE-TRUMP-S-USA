using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class CreateAccountScript : MonoBehaviour {

    private string secretKey = "1dLc9f170GIfi0cdgm5QW267J91tMqM7";
    public GameObject Username_Field;
    public GameObject Password_Field1;
    public GameObject Password_Field2;
    public GameObject TitleText;
    public GameObject LoginUsername_Field;
    public GameObject CreateAccountPanel;
    private string username;
    public string password1;
    public string password2;
    private string registerURL = "https://insyprojects.ewi.tudelft.nl/ewi3620tu6/register.php?";

    // Use this for initialization
    void OnEnable()
    {
        TitleText.GetComponent<Text>().text = "Create Account";
        username = Username_Field.GetComponent<InputField>().text;
        password1 = Password_Field1.GetComponent<InputField>().text;
        password2 = Password_Field2.GetComponent<InputField>().text;
        StartCoroutine(Register());
    }

    public IEnumerator Register()
    {
        if (username.Length < 4)
        {
            TitleText.GetComponent<Text>().text = "Username must be at least 4 characters long.";
            this.enabled = false;
        }
        else if (string.CompareOrdinal(password1, password2) != 0)
        {
            TitleText.GetComponent<Text>().text = "Passwords must match.";
            this.enabled = false;
        }
        else
        {
            string hash = Md5Sum(username + password1 + secretKey);
            string postURL = registerURL + "username=" + WWW.EscapeURL(username) + "&password=" + WWW.EscapeURL(password1) + "&hash=" + hash;
            WWW post = new WWW(postURL);
            yield return post;
            string data = System.Text.Encoding.UTF8.GetString(post.bytes, 1, post.bytes.Length - 1);
            if (post.error != null)
            {
                print("Error Logging in:" + post.error);
                this.enabled = false;
            }
            else if (string.CompareOrdinal(data, "account created") == 1)
            {
                TitleText.GetComponent<Text>().text = "Account Created.";
                LoginUsername_Field.GetComponent<InputField>().text = username;
                this.enabled = false;
                yield return new WaitForSeconds(1f);
                CreateAccountPanel.SetActive(false);
            }
            else
            {
                TitleText.GetComponent<Text>().text = "Username Already Exists.";
                Password_Field1.GetComponent<InputField>().text = "";
                Password_Field2.GetComponent<InputField>().text = "";
                this.enabled = false;
            }
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
