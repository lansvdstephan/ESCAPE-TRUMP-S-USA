using UnityEngine;
using System.Collections;

public class HighScoreController : MonoBehaviour {

    public string addScoreURL = "https://insyprojects.ewi.tudelft.nl/ewi3620tu6/addScore.js";
    public string getScoresURL = "https://insyprojects.ewi.tudelft.nl/ewi3620tu6/getScores.js";
    private string secretKey = "secretKey";
	// Use this for initialization
	void Start () {
        StartCoroutine(getScores());
	}

    public IEnumerator addScores(string name, int score)
    {
        string hash = Md5Sum(name + score + secretKey);
        string post_url = addScoreURL + "name" + WWW.EscapeURL(name) + "&score=" + score + "&hash=" + hash;
        WWW hs_post = new WWW(post_url);
        yield return hs_post;
        if (hs_post.error != null)
        {
            print("Error posting highscore:"+hs_post.error);
        }
    }
	
    IEnumerator getScores()
    {
        gameObject.GetComponent<GUIText>().text = "Loading Scores";
        WWW hs_get = new WWW(getScoresURL);
        yield return hs_get;
        if(hs_get.error != null)
        {
            print("Error loading highscores:" + hs_get.error);
        }
        else
        {
            gameObject.GetComponent<GUIText>().text = hs_get.text;
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
