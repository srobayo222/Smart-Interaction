using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerTrivia : MonoBehaviour
{
    Question[] _questions = null;
    public Question[] Questions { get { return _questions; } }

    [SerializeField] GameEvents events = null;

    private List<AnswerData> PickedAnswerds = new List<AnswerData>();
    private List<int> FinishedQUestions = new List<int>();
    private int currentQuestion = 0;

    private void Start()
    {
        LoadQuestions();
        foreach (var question in Questions)
        {
            Debug.Log(question.Info);

        }
        //  Display();
    }
    void EreaseAnswers()
    {
        PickedAnswerds = new List<AnswerData>();
    }
    void Display()
    {
        EreaseAnswers();
        var question = GetRandomQuestion();
        if (events.UpdateQuestionUI != null)
        {
            events.UpdateQuestionUI(question);
        }
        else
        {
            Debug.LogWarning("Ups! Something went wrong while trying to display new Question UI Data. GameEvents.UpdateQuestionUI is null. Issue occured in GameManager.Display() method.");
        }

    }

    Question GetRandomQuestion()
    {
        var randomIndex = GetRandomQuestionIndex();
        currentQuestion = randomIndex;
        return Questions[randomIndex];
    }

    int GetRandomQuestionIndex()
    {
        var random = 0;
        if (FinishedQUestions.Count < Questions.Length)
        {
            do
            {
                random = UnityEngine.Random.Range(0, Questions.Length);
            } while (FinishedQUestions.Contains(random) || random == currentQuestion);
        }
        return random;
    }

    void LoadQuestions()
    {
        Object[] objs = Resources.LoadAll("Questions", typeof(Question));
        _questions = new Question[objs.Length];
        for (int i = 0; i < objs.Length; i++)
        {
            _questions[i] = (Question)objs[i];
        }
    }
}
