using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class Quiz : MonoBehaviour
{
    [Header ("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    [SerializeField] QuestionSO currentQuestion;

    [Header ("Answers")]
    [SerializeField] GameObject[] answerButtons;
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    bool hasAnsweredEarly;

    [Header ("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;
    //[SerializeField] Image timerImage;
    //Timer timer;
    //int correctAnswerIndex;

    [Header ("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        //DisplayQuestion();
        GetNextQuestion();
        //scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    void Update() 
    {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    void DisplayAnswer(int index)
    {
        if(index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct!!!";
            Image buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            //scoreKeeper.IncrementCorrectAnswers();
        }
        else
        {
            string correctAnswer = currentQuestion.GetAnswer(currentQuestion.GetCorrectAnswerIndex());
            questionText.text = "Sorry, the correct answer was " + correctAnswer;
            Image buttonImage = answerButtons[currentQuestion.GetCorrectAnswerIndex()].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }
    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        //scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
        timer.CancelTimer();
    }
    void GetNextQuestion()
    {
        if(questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();            
        }
        //scoreKeeper.IncrementQuestionsSeen();
    }
    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
        
    }
    void SetDefaultButtonSprites()
    {
        for(int i = 0; i< answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }

    }
    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();

        for(int i=0; i<answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }
    void SetButtonState(bool state)
    {
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }
}
