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
    // Use this for initialization
    void Awake()
    {
        Text1 = this.transform.FindChild("Text1").GetComponent<Text>();
        Text2 = this.transform.FindChild("Text2").GetComponent<Text>();
        Text3 = this.transform.FindChild("Text3").GetComponent<Text>();
        Text5 = this.transform.FindChild("Text5").GetComponent<Text>();
    }

    void Start () {
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
            while (timeLeft > 0)
            {
                timeLeft -= 1;                
                text21 = timeLeft + " sec\n\n";
                text2 = text21 + text22 + text23 + text24;
                Text2.text = text2;
                timeScore += 1;
                text31 = timeScore + "\n\n";
                text3 = text31 + text32 + text33 + text34;
                Text3.text = text3;
                yield return new WaitForSecondsRealtime(0.01f);
            }
            yield return new WaitForSecondsRealtime(1f);
        }
        
        if (healthBool)
        {
            while (health > 1)
            {
                health -= 1;
                text22 = health + "\n\n";
                text2 = text21 + text22 + text23 + text24;
                Text2.text = text2;
                healthScore += 1;
                text32 = healthScore + "\n\n";
                text3 = text31 + text32 + text33 + text34;
                Text3.text = text3;
                yield return new WaitForSecondsRealtime(0.02f);
            }
            yield return new WaitForSecondsRealtime(1f);
        }

        if (fuelBool)
        {
            while (fuel > 0)
            {
                fuel -= 1;
                text23 = fuel + "\n\n";
                text2 = text21 + text22 + text23 + text24;
                Text2.text = text2;
                fuelScore += 1;
                text33 = fuelScore + "\n\n";
                text3 = text31 + text32 + text33 + text34;
                Text3.text = text3;
                yield return new WaitForSecondsRealtime(0.01f);
            }
            yield return new WaitForSecondsRealtime(1f);
        }
        if (itemBool)
        {
            while (items > 0)
            {
                items -= 1;
                text24 = items + "\n\n";
                text2 = text21 + text22 + text23 + text24;
                Text2.text = text2;
                itemScore += 50;
                text34 = itemScore + "\n\n";
                text3 = text31 + text32 + text33 + text34;
                Text3.text = text3;
                yield return new WaitForSecondsRealtime(0.8f);
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
        SceneManager.LoadScene(SceneManager.sceneCount + 1);
    }
}
