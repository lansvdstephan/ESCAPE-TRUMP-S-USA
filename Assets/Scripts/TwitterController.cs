using UnityEngine;
using System.Collections;

public class TwitterController : MonoBehaviour {

    private string twitterAddress = "http://twitter.com/intent/tweet"; 
    private string tweetLanguage = "en";
    private string textToDisplay = "test tweet from Unity";


    void ShareToTwitter (string textToDisplay) //aanroepen op onClick()
    {
        Application.OpenURL(twitterAddress + "?text=" + WWW.EscapeURL(textToDisplay) + "&amp;lang="
    + WWW.EscapeURL(tweetLanguage));
    }
}
