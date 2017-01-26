using UnityEngine;
using System.Collections;

public class TwitterController : MonoBehaviour {
    public int highscore;
    private string twitterAddress = "http://twitter.com/intent/tweet"; 
    private string tweetLanguage = "en";
    private string text;
   
    public void ShareToTwitter () //aanroepen op onClick()
    {
        text= "I escaped Trump's USA with a score of " + highscore + " points! " + "Can you beat it? ";
        Application.OpenURL(twitterAddress + "?text=" + WWW.EscapeURL(text) + "&amp;lang="
    + WWW.EscapeURL(tweetLanguage));
    }
}
