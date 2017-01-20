using UnityEngine;
using System.Collections;

public class TwitterController : MonoBehaviour {

    private string twitterAddress = "http://twitter.com/intent/tweet"; 
    private string tweetLanguage = "en";
    private string text = "I escaped Trump's USA with a high score of 1000! " + "Can you beat it? ";
    
    public void ShareToTwitter () //aanroepen op onClick()
    {
        Application.OpenURL(twitterAddress + "?text=" + WWW.EscapeURL(text) + "&amp;lang="
    + WWW.EscapeURL(tweetLanguage));
    }
}
