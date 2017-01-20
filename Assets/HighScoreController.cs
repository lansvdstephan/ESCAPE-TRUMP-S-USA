using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour {

    private string addScoreURL = "https://insyprojects.ewi.tudelft.nl/ewi3620tu6/addscore.php?";
    private string getScoresURL = "https://insyprojects.ewi.tudelft.nl/ewi3620tu6/display.php";
    private string secretKey = "1dLc9f170GIfi0cdgm5QW267J91tMqM7";
    private string text;
    private string[] highscoreArray;
    private string playerName;
    public GameObject Name_field;
    public GameObject LoadingTextObject;
    public GameObject HighscoreNames1;
    public GameObject HighscoreScores1;
    public GameObject HighscoreNames2;
    public GameObject HighscoreScores2;
    public GameObject Highscores;
    public int score;
    public bool gameFinished=false;
    

    void OnEnable()
    {
        StartCoroutine(loadingScores());
        playerName = Name_field.GetComponent<Text>().text;
        if (gameFinished==true)
        {
            StartCoroutine(addScores(name, score));
            gameFinished = false;
        }
        else
        {
            StartCoroutine(getScores());
        }
	}

    public IEnumerator addScores(string name, int score)
    {
        string hash = Md5Sum(playerName + score + secretKey);
        string postURL = addScoreURL + "name=" + WWW.EscapeURL(playerName) + "&score=" + score + "&hash="+ hash;
        WWW post = new WWW(postURL);
        yield return post;
        if (post.error != null)
        {
            print("Error posting highscore:"+post.error);
        }
        StartCoroutine(getScores());
    }
	
    IEnumerator getScores()
    {
        WWW get = new WWW(getScoresURL);
        yield return get;
        text = get.text;
        highscoreArray = text.Split('&');
        print(text);
        StopCoroutine(loadingScores());
        if (get.error != null)
        {
            print(get.error);
            LoadingTextObject.GetComponent<Text>().text = "Unable To Load Highscores, Check Internet Connection.";
        }
        else
        {            
            LoadingTextObject.SetActive(false);
            HighscoreNames1.GetComponent<Text>().text = highscoreArray[0]+"\n" + highscoreArray[2]+"\n" + highscoreArray[4] + "\n" + highscoreArray[6] + "\n" + highscoreArray[8];
            HighscoreScores1.GetComponent<Text>().text = highscoreArray[1] + "\n" + highscoreArray[3] + "\n" + highscoreArray[5] + "\n" + highscoreArray[7] + "\n" + highscoreArray[9];
            HighscoreNames2.GetComponent<Text>().text = highscoreArray[10] + "\n" + highscoreArray[12] + "\n" + highscoreArray[14] + "\n" + highscoreArray[16] + "\n" + highscoreArray[18];
            HighscoreScores2.GetComponent<Text>().text = highscoreArray[11] + "\n" + highscoreArray[13] + "\n" + highscoreArray[15] + "\n" + highscoreArray[17] + "\n" + highscoreArray[19];
            Highscores.SetActive(true);
        }
    }

    IEnumerator loadingScores()
    {
        string loadingText = "Loading Highscores";
        while (LoadingTextObject.active == true)
        {
            LoadingTextObject.GetComponent<Text>().text = loadingText;
            yield return new WaitForSeconds(0.5f);
            LoadingTextObject.GetComponent<Text>().text = loadingText + ".";
            yield return new WaitForSeconds(0.5f);
            LoadingTextObject.GetComponent<Text>().text = loadingText + "..";
            yield return new WaitForSeconds(0.5f);
            LoadingTextObject.GetComponent<Text>().text = loadingText + "...";
            yield return new WaitForSeconds(0.5f);
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
