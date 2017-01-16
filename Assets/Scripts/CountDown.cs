using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CountDown : MonoBehaviour
{
    public float tijd;
    public Text timeText;
    public Text timeLeft;

	public bool startcounting = false;

    private float minutes;
    private float seconds;
    private float mseconds;

    private bool changeCol;
    private Color orange;
    private Color green;
    // Use this for initialization
    void Start ()
    {
        mseconds = 99f;
        orange = new Color(1F, 0.5F, 0.0F);
        green = new Color(0F, 1F, 0F);
    }
	
	// Update is called once per frame
	void Update ()
	{
		setTijdText ();
		setColor ();
		if (startcounting) {
			if (tijd >= 0.012)
				tijd = tijd - Time.deltaTime;
			else
				tijd = 0;
		}
	}

    private void setTijdText()
    {
        minutes = tijd / 60f;
        minutes = Mathf.Floor(minutes);
        seconds = Mathf.Floor(tijd - (minutes * 60f));
        mseconds = Mathf.Floor(100*(tijd - minutes * 60f - seconds));
        if (mseconds < 10f)
            timeText.text = "0"+ minutes + ":" + seconds + ":" + "0" + mseconds;
        else
            timeText.text = "0" + minutes + ":" + seconds + ":" + mseconds;
    }

    private void setColor()
    {
        if (tijd > 30f)
        {
            timeText.color = green;
            timeLeft.color = green;
        }
        else if (tijd > 11f)
        {
            timeText.color = orange;
            timeLeft.color = orange;
        }
        else if ((tijd >= 10 && tijd < 11f) || (tijd >= 8f && tijd < 9f) || (tijd >= 6f && tijd < 7f) || (tijd >= 4.5f && tijd < 5f) || (tijd >= 3.5f && tijd < 4f) || (tijd >= 2.5f && tijd < 3f) || (tijd >= 1.5f && tijd < 2f) || (tijd >= 0.5f && tijd < 1f) || tijd <0.02f)
        {
            timeText.color = Color.red;
            timeLeft.color = Color.red;
        }
        else
        {
            timeText.color = orange;
            timeLeft.color = orange;
        }
    }
}
