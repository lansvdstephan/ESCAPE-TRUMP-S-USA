using UnityEngine;
using System.Collections;

public class QuizController : PhilInteractable
{

    public override void Interact(GameObject Interacted)
    {
        if (!QuizDialogue.Instance.quizPanel.activeSelf)
        {
            QuizDialogue.Instance.StartQuiz();
        }
        else if (QuizDialogue.Instance.endDialogue)
        {
            QuizDialogue.Instance.endDialogue = false;
            QuizDialogue.Instance.ShutDown();
        }
        else if(!QuizDialogue.Instance.initiated)
        {
            QuizDialogue.Instance.ContinueQuiz();
        }
    }
} 
