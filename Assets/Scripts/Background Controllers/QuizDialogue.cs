﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuizDialogue : MonoBehaviour
{
    public static QuizDialogue Instance { get; set; }
    public GameObject quizPanel;
    public string[] questions;
    public bool[] answers;
    public string introText;
    public GameObject buttonTrue;
    public GameObject buttonFalse;
    public bool endDialogue;

    private List<string> questionLines;
    private List<bool> questionAnswers;
    private List<int> answeredQuestions;
    private Text quizText; 
    private int quizIndex;
    private int questionAmount;
    private Text trueFalseText;

    public bool isTyping = false;
    private bool cancelTyping = false;
    public float typeSpeed;



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

    public void StartQuiz()
    {
        buttonTrue.SetActive(false);
        buttonFalse.SetActive(false);
        quizPanel.SetActive(true);
        Time.timeScale = 0.0f;
        questionAmount = 0;
        trueFalseText.text = "\n " + ">>Access denied";
        StartCoroutine(TextScroll(">>Answer Security Questions \n" + ">>5 questions should be answered correctly \n"));
        //quizText.text = ">>Answer Security Questions \n" + ">>5 questions should be answered correctly \n";  
       
    }

    public void StartQuizAfterWrongAnswer()
    {
       buttonTrue.SetActive(false);
       buttonFalse.SetActive(false);
       quizPanel.SetActive(true);
       Time.timeScale = 0.0f;
       trueFalseText.text = "\n " + ">>Access denied";
        StartCoroutine(TextScroll(">>Answer Security Questions \n" + ">>5 questions should be answered correctly \n" + "\n" + ">>Correct answers: " + questionAmount));

    }
    public void ContinueQuiz()
    {
        if (!isTyping)
        {
            buttonTrue.SetActive(true);
            buttonFalse.SetActive(true);

            questionLines = new List<string>(questions.Length);
            questionLines.AddRange(questions);
            questionAnswers = new List<bool>(answers.Length);
            questionAnswers.AddRange(answers);
            answeredQuestions = new List<int>(questions.Length);
            quizIndex = Random.Range(0, questions.Length);
            answeredQuestions.Add(quizIndex);
            trueFalseText.text = "True or False: ";
            StartCoroutine(TextScroll(questionLines[quizIndex]));
        }
    }


    public void CheckAnswer(bool ans)
    {
        if (!isTyping)
        {
            if (questionAnswers[quizIndex] == ans)
            {
                questionAmount++;
                if (questionAmount == 5)
                {
                    StartCoroutine(TextScroll("Code: Secret Area == 1946"));
                    trueFalseText.text = " ";
                    buttonTrue.SetActive(false);
                    buttonFalse.SetActive(false);
                    endDialogue = true;
                }
                else
                {
                    NextQuestion();
                }
            }
            else
            {
                StartQuizAfterWrongAnswer();

            }
        }
        else if (isTyping && !cancelTyping)
        {
            //cancelTyping = true;
        }
    }

    private IEnumerator TextScroll(string lineOfText)
    {
        int letter = 0;
        quizText.text = "";
        isTyping = true;
        cancelTyping = false;
        while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            quizText.text += lineOfText[letter];
            letter += 1;
            yield return new WaitForSeconds(typeSpeed);
        }
        if (lineOfText == "Code: Secret Area == 1946")
        {
            endDialogue = true;
        }
        quizText.text = lineOfText;
        isTyping = false;
        cancelTyping = false;
    }

    public void ShutDown()
    {
        Time.timeScale = 1.0f;
        quizPanel.SetActive(false);
        endDialogue = false;
    } 

    public void NextQuestion()
    {
        while (answeredQuestions.Contains(quizIndex))
        {
          quizIndex = Random.Range(0, questions.Length);
        }
        answeredQuestions.Add(quizIndex);
    }
}

