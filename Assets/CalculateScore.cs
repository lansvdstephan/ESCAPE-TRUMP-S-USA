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

    void OnEnable () {
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
            int speed = 4;
            while (timeLeft > 0)
            {
                if(timeLeft < 200)
                {
                    speed = 2;
                }
                if(timeLeft < 75)
                {
                    speed = 1;
                }
                timeLeft -= speed;                
                text21 = timeLeft + " sec\n\n";
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
            int speed = 4;
            while (health > 0)
            {
                if (health < 200)
                {
                    speed = 2;
                }
                if(health < 75)
                {
                    speed = 1;
                }
                health -= speed;
                text22 = health + "\n\n";
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
            int speed = 4;
            while (fuel > 0)
            {
                if (fuel < 200)
                {
                    speed = 2;
                }
                if(fuel < 75)
                {
                    speed = 1;
                }
                fuel -= speed;
                text23 = fuel + "\n\n";
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
            int speed = 4;
            while (items > 0)
            {
                if (items < 10)
                {
                    speed = 2;
                }
                if (items < 3)
                {
                    speed = 1;
                }
                items -= 1;
                text24 = items + "\n\n";
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
    }

    public void ResetPanel()
    {
        timeBool = false;
        itemBool = false;
        healthBool = false;
        fuelBool = false;
        text1 = "";
        timeScore = 0;
        itemScore = 0;
        healthScore = 0;
        fuelScore = 0;
        ContinueButton.SetActive(false);
        Time.timeScale = 1.0f;
        if (currentScene != 6)
        {
            currentScene += 1;
            SceneManager.LoadScene(currentScene);
        }
        else
        {
            GameCompletedPanel.transform.FindChild("ScoreText").GetComponent<Text>().text = "Score: " + totalScore;
            GameObject.Find("High Score Panel").GetComponent<HighScoreController>().score = totalScore;
            GameObject.Find("High Score Panel").GetComponent<HighScoreController>().gameFinished = true;
            GameCompletedPanel.SetActive(true);
        }
    }
}
