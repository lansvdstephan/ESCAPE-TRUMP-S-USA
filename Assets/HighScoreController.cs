using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour {

    private string addScoreURL = "https://insyprojects.ewi.tudelft.nl/ewi3620tu6/addscore.php?";
    private string getScoresURL = "https://insyprojects.ewi.tudelft.nl/ewi3620tu6/display.php";
    private string getLocalScoresURL = "https://insyprojects.ewi.tudelft.nl/ewi3620tu6/displaylocal.php?";
    private string secretKey = "1dLc9f170GIfi0cdgm5QW267J91tMqM7";
    private string text;
    private string[] highscoreArray;
    public string playerName;
    public GameObject LoadingTextObject;
    public GameObject HighscoreNames1;
    public GameObject HighscoreScores1;
    public GameObject HighscoreNames2;
    public GameObject HighscoreScores2;
    public GameObject Highscores;
    public GameObject Title;

    public int score;
    public bool gameFinished=false;
    public bool online=true;

    void OnEnable()
    {
        StartCoroutine(loadingScores());
        if (gameFinished==true&&online)
        {
            StartCoroutine(addScores());
            gameFinished = false;
        }
        else
        {
            StartCoroutine(getScores());
        }
	}

    public IEnumerator addScores()
    {
        string hash = Md5Sum(playerName + score + secretKey);
        string postURL = addScoreURL + "username=" + playerName + "&score=" + score + "&hash="+ hash;
        WWW post = new WWW(postURL);
        yield return post;
        if (post.error != null)
        {
            print("Error posting highscore:"+post.error);
        }
        StartCoroutine(getScores());
    }
	
    public void startGetScores()
    {
        StartCoroutine(getScores());
    }

    IEnumerator getScores()
    {
        Highscores.SetActive(false);
        StartCoroutine(loadingScores());
        Title.GetComponent<Text>().text = "Global High Scores";
        HighscoreNames1.GetComponent<Text>().text = "";
        HighscoreScores1.GetComponent<Text>().text = "";
        HighscoreNames2.GetComponent<Text>().text = "";
        HighscoreScores2.GetComponent<Text>().text = "";
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
            if (highscoreArray[0] == null)
            {
                LoadingTextObject.GetComponent<Text>().text = "No Local Highscores Found.";
            }
            int i = 0;
            while (i<highscoreArray.Length)
            {
                if (i < 10)
                {
                    if (i % 2 == 0)
                    {
                        HighscoreNames1.GetComponent<Text>().text += highscoreArray[i] + "\n";
                    }
                    else
                    {
                        HighscoreScores1.GetComponent<Text>().text += highscoreArray[i] + "\n";
                    }
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        HighscoreNames2.GetComponent<Text>().text += highscoreArray[i] + "\n";
                    }
                    else
                    {
                        HighscoreScores2.GetComponent<Text>().text += highscoreArray[i] + "\n";
                    }
                }
                i++;
            }
            Highscores.SetActive(true);
        }
    }

    public void startGetLocalScores()
    {
        StartCoroutine(getLocalScores());
    }

    IEnumerator getLocalScores()
    {
        Highscores.SetActive(false);
        StartCoroutine(loadingScores());
        Title.GetComponent<Text>().text = "Local High Scores";
        HighscoreNames1.GetComponent<Text>().text = "";
        HighscoreScores1.GetComponent<Text>().text = "";
        HighscoreNames2.GetComponent<Text>().text = "";
        HighscoreScores2.GetComponent<Text>().text = "";
        string postURL = getLocalScoresURL + "username=" + playerName;
        WWW post = new WWW(postURL);
        yield return post;
        text = post.text;
        highscoreArray = text.Split('&');
        print(text);
        StopCoroutine(loadingScores());
        if (post.error != null)
        {
            print(post.error);
            LoadingTextObject.GetComponent<Text>().text = "Unable To Load Highscores, Check Internet Connection.";
        }
        else
        {
            LoadingTextObject.SetActive(false);
            if (highscoreArray[0] == null)
            {
                LoadingTextObject.GetComponent<Text>().text = "No Local Highscores Found.";
            }
            int i = 0;
            while (i < highscoreArray.Length)
            {
                if (i < 10)
                {
                    if (i % 2 == 0)
                    {
                        HighscoreNames1.GetComponent<Text>().text += highscoreArray[i] + "\n";
                    }
                    else
                    {
                        HighscoreScores1.GetComponent<Text>().text += highscoreArray[i] + "\n";
                    }
                }
                else
                {
                    if (i % 2 == 0)
                    {
                        HighscoreNames2.GetComponent<Text>().text += highscoreArray[i] + "\n";
                    }
                    else
                    {
                        HighscoreScores2.GetComponent<Text>().text += highscoreArray[i] + "\n";
                    }
                }
                i++;
            }
            Highscores.SetActive(true);
        }
    }

    IEnumerator loadingScores()
    {
        string loadingText = "Loading Highscores";
        while (LoadingTextObject.activeSelf == true)
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
