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

    private List<string> questionLines;
    private List<bool> questionAnswers;
    private Text trueFalseText;
    private Text quizText;
    private int quizIndex;

    void Awake()
    {
        quizText = quizPanel.transform.FindChild("Quiz Text").GetComponent<Text>();
        trueFalseText = quizPanel.transform.FindChild("trueFalseText").GetComponent<Text>();
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
        quizIndex = Random.Range(0, questions.Length);
        questionLines = new List<string>(questions.Length);
        questionLines.AddRange(questions);
        questionAnswers = new List<bool>(answers.Length);
        questionAnswers.AddRange(answers);
        CreateDialogue();
    }

    public void CreateDialogue()
    {
        print("started dialog");
        quizText.text = questionLines[quizIndex];
        quizPanel.SetActive(true);
    }

    public void CheckAnswer(bool ans)
    {
        
    }

    public void ContinueDialogue()
    {

        if (quizIndex < questionLines.Count - 1)
        {
            quizIndex++;
            quizText.text = questionLines[quizIndex];
            print("Dialogue continue");
        }
        else
        {
            quizPanel.SetActive(false);
            print("Dialogue ended");
        }
    }
}

