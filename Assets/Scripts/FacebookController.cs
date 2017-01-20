using UnityEngine;
using System.Collections;

public class FacebookController : MonoBehaviour
{
    private string facebookID = "1327549797288752";
    private string facebookURL = "http://www.facebook.com/dialog/feed";
    private string link = "https://escapetrumpsusa.itch.io/escape-trumps-usa";
    private string name = "I played the game Escape Trump's USA!";
    private string caption= "Try it out now!";
    private string description = "I escaped Trump's USA with a high score of 1000! " + "Can you beat it?";
    private string redirect = "http://facebook.com"; 


    public void ShareToFacebook()
    {
        Application.OpenURL(facebookURL + "?app_id=" + facebookID + "&link=" + WWW.EscapeURL(link) + "&name=" + WWW.EscapeURL(name) + "&caption=" + WWW.EscapeURL(caption) + "&description=" + WWW.EscapeURL(description) + "&redirect_uri=" + WWW.EscapeURL(redirect));
    }
}
