using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuizDialogue : MonoBehaviour
{
    public static QuizDialogue Instance { get; set; }
    public GameObject quizPanel;
    public string[] questions;
    public bool[] answers;
    public int questionAmount;

    private List<string> questionLines;
    private List<bool> questionAnswers;
    private List<int> answeredQuestions;
    private Text trueFalseText; //deze text zie je op je scherm 
    private Text quizText; // deze text zie je op je scherm
    private int quizIndex;
  

    void Awake()
    {
        quizText = quizPanel.transform.FindChild("Quiz Text").GetComponent<Text>();
        trueFalseText = quizPanel.transform.FindChild("TrueFalse Text").GetComponent<Text>();
        quizPanel.SetActive(false);

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void StartQuiz() //aanroepen op spatie
    {
        Time.timeScale = 0.0f;
        questionLines = new List<string>(questions.Length);
        questionLines.AddRange(questions); //kopieer questions naar de lijst 
        questionAnswers = new List<bool>(answers.Length);
        questionAnswers.AddRange(answers); //kopieer answers naar de lijst 
        answeredQuestions = new List<int>(questions.Length); //lijst is leeg 
        quizIndex = Random.Range(0, questions.Length); 
        answeredQuestions.Add(quizIndex);
        trueFalseText.text = "True or False: ";
        quizText.text = questionLines[quizIndex]; 
        quizPanel.SetActive(true);
        
    }


    public void CheckAnswer(bool ans) // deze functie aanroepen on click 
    {
        if (questionAnswers[quizIndex] == ans) 
        {
            questionAmount++;
            if (questionAmount == 5)
            {
                quizText.text = "You found the code: 1946";
                if (Input.GetButtonUp("space"))
                {
                    Time.timeScale = 1.0f;
                    quizPanel.SetActive(false);
                }
            }
            else
            {
                NextQuestion(); 
            }
            
        } else {
            quizPanel.SetActive(false);
            Time.timeScale = 1.0f;
       }
    }
  

    public void NextQuestion()
    {
        while (answeredQuestions.Contains(quizIndex))
        {
          quizIndex = Random.Range(0, questions.Length);
        }
        answeredQuestions.Add(quizIndex);
        quizText.text = questionLines[quizIndex];
       
    }
}

