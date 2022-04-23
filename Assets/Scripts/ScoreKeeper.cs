using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswer = 0;
    int questionsSeen = 0;

    public int GetCorrectAnswers()
    {
        return correctAnswer;
    }
    public int GetQuestionsSeen()
    {
        return questionsSeen;
    }
    public void IncrementQuestionsSeen()
    {
        questionsSeen++;
    }
    public void IncrementCorrectAnswers()
    {
        correctAnswer++;
    }
    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswer / (float)questionsSeen * 100);
    }
}
