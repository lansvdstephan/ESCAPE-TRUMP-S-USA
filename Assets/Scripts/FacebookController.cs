using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour
{
    private string facebookID = "1327549797288752";
    private string facebookURL = "http://www.facebook.com/dialog/feed";


    void ShareToFacebook(string linkParameter, string nameParameter, string captionParameter, string descriptionParameter, string pictureParameter, string redirectParameter)
    {
        Application.OpenURL(facebookURL + "?app_id=" + facebookID + "&link=" + WWW.EscapeURL(linkParameter) + "&name=" + WWW.EscapeURL(nameParameter) + "&caption=" + WWW.EscapeURL(captionParameter) + "&description=" + WWW.EscapeURL(descriptionParameter) + "&picture=" + WWW.EscapeURL(pictureParameter) + "&redirect_uri=" + WWW.EscapeURL(redirectParameter));
    }
}
