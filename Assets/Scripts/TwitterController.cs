using UnityEngine;
using System.Collections;

public class TwitterController : MonoBehaviour {

    public string twitterAddress = "http://twitter.com/intent/tweet"; 
    public string tweetLanguage = "en";
    public string textToDisplay = "test tweet to Unity";


    public void ShareToTwitter () //aanroepen op onClick()
    {
        Application.OpenURL(twitterAddress + "?text=" + WWW.EscapeURL(textToDisplay) + "&amp;lang="
+ WWW.EscapeURL(tweetLanguage));
    }
}
