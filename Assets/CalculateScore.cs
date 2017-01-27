using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CalculateScore : MonoBehaviour {
    public bool timeBool=false;
    public bool itemBool=false;
    public bool healthBool=false;
    public bool fuelBool=false;
    public int timeLeft;
    public int items;
    public int health;
    public int fuel;
    private string text1;
    private string text2;
    private string text21;
    private string text22;
    private string text23;
    private string text24;
    private string text3;
    private string text31;
    private string text32;
    private string text33;
    private string text34;
    public int timeScore;
    public int itemScore;
    public int healthScore;
    public int fuelScore;
    private Text Text1;
    private Text Text2;
    private Text Text3;
    private Text Text5;
    public int totalScore;
    public GameObject ContinueButton;
    public GameObject GameCompletedPanel;
    public GameObject levelCompletedPanel;
	public GameObject HighScorePanel;
    public int currentScene;
    // Use this for initialization
    void Awake()
    {
        currentScene = 1;
        Text1 = this.transform.FindChild("Text1").GetComponent<Text>();
        Text2 = this.transform.FindChild("Text2").GetComponent<Text>();
        Text3 = this.transform.FindChild("Text3").GetComponent<Text>();
        Text5 = this.transform.FindChild("Text5").GetComponent<Text>();
    }

    void OnEnable()
    {
        text1 = "";
        text2 = "";
        if (timeBool)
        {
            text1 += "Time Left:\n\n";
            text21 = timeLeft + " sec\n\n";
            text31 = "0\n\n";
        }
        if (healthBool)
        {
            text1 += "Health Left:\n\n";
            text22 = health + "\n\n";
            text32 = "0\n\n";
        }
        if (fuelBool)
        {
            text1 += "Fuel Left:\n\n";
            text23 = fuel + "\n\n";
            text33 = "0\n\n";
        }
        if (itemBool)
        {
            text1 += "Items Collected:\n\n";
            text24 = items + "\n\n";
            text34 = "0\n\n";
        }
        text2 = text21 + text22 + text23 + text24;
        text3 = text31 + text32 + text33 + text34;
        Text1.text = text1;
        Text2.text = text2;
        Text3.text = text3;
        StartCoroutine(Calculate());
    }
	
    IEnumerator Calculate()
    {
        yield return new WaitForSecondsRealtime(2f);
        if (timeBool)
        {	
			int tempTime = timeLeft;
            int speed = 10;
            while (tempTime > 0)
            {
                if(tempTime < 200)
                {
                    speed = 5;
                }
                if(tempTime < 30)
                {
                    speed = 1;
                }
                tempTime -= speed;                
                text21 = tempTime + " sec\n\n";
                text2 = text21 + text22 + text23 + text24;
                Text2.text = text2;
                timeScore += speed;
                text31 = timeScore + "\n\n";
                text3 = text31 + text32 + text33 + text34;
                Text3.text = text3;
                yield return new WaitForSecondsRealtime(0.01f);
            }
            yield return new WaitForSecondsRealtime(1f);
        }
        
        if (healthBool)
        {
            int temphealth = health;
            int speed = 10;
            while (temphealth > 0)
            {
                if (health < 200)
                {
                    speed = 5;
                }
                else if (health < 30)
                {
                    speed = 1;
                }
                if (currentScene == 3) { temphealth = temphealth - speed * 10; }
                else { temphealth = temphealth - speed; }
                if (temphealth < 0) { temphealth = 0; }
                text22 = temphealth + "\n\n";
                text2 = text21 + text22 + text23 + text24;
                Text2.text = text2;
                healthScore += speed;
                text32 = healthScore + "\n\n";
                text3 = text31 + text32 + text33 + text34;
                Text3.text = text3;
                yield return new WaitForSecondsRealtime(0.01f);
            }
            yield return new WaitForSecondsRealtime(1f);
        }

        if (fuelBool)
        {
            int speed = 10;
            int tempfuel = fuel;
            while (tempfuel > 0)
            {
                if (tempfuel < 200)
                {
                    speed = 5;
                }
                if(tempfuel < 30)
                {
                    speed = 1;
                }
                tempfuel = tempfuel - speed*10;
                if (tempfuel < 0) { tempfuel = 0; }
                text23 = tempfuel + "\n\n";
                text2 = text21 + text22 + text23 + text24;
                Text2.text = text2;
                fuelScore += speed;
                text33 = fuelScore + "\n\n";
                text3 = text31 + text32 + text33 + text34;
                Text3.text = text3;
                yield return new WaitForSecondsRealtime(0.01f);
            }
            yield return new WaitForSecondsRealtime(1f);
        }

        if (itemBool)
        {	
			int tempItems = items;
            int speed = 4;
            while (tempItems > 0)
            {
                if (tempItems < 10)
                {
                    speed = 2;
                }
                if (tempItems < 3)
                {
                    speed = 1;
                }
                tempItems -= 1;
                text24 = tempItems + "\n\n";
                text2 = text21 + text22 + text23 + text24;
                Text2.text = text2;
                itemScore += 50;
                text34 = itemScore + "\n\n";
                text3 = text31 + text32 + text33 + text34;
                Text3.text = text3;
                yield return new WaitForSecondsRealtime(0.8f/speed);
            }
        }
        totalScore += timeScore + healthScore + fuelScore + itemScore;
        yield return new WaitForSecondsRealtime(1f);
        Text5.text = totalScore + "";
        for(int i = 0; i < 12; ++i)
        {
            Text5.fontSize = 35 - i;
            yield return new WaitForSecondsRealtime(0.05f);
        }
        ContinueButton.SetActive(true);
        currentScene += 1;
    }

    public void ResetPanel()
    {
        timeBool = false;
        itemBool = false;
        healthBool = false;
        fuelBool = false;
        text1 = "";
        health = 0;
        timeLeft = 0;
        fuel = 0;
        items = 0;
        timeScore = 0;
        itemScore = 0;
        healthScore = 0;
        fuelScore = 0;
        text21 = "";
        text22 = "";
        text23 = "";
        text24 = "";
        text31 = "";
        text32 = "";
        text33 = "";
        text34 = "";
        Time.timeScale = 1.0f;
        if (currentScene != 2)
        {
            SceneManager.LoadScene(currentScene);
        }
        else
        {
            GameCompletedPanel.transform.FindChild("ScoreText").GetComponent<Text>().text = "Score: " + totalScore;
            HighScorePanel.GetComponent<HighScoreController>().score = totalScore;
            HighScorePanel.GetComponent<HighScoreController>().gameFinished = true;
            GameCompletedPanel.transform.FindChild("ShareOnTwitterButton").GetComponent<TwitterController>().highscore = totalScore;
            GameCompletedPanel.transform.FindChild("ShareOnFaceBookButton").GetComponent<FacebookController>().highscore = totalScore;
            GameCompletedPanel.SetActive(true);
        }
    }
}
