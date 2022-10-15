using UnityEngine;
using TMPro;

public class ScoreUpdater : MonoBehaviour
{
    private TMP_Text scoreDisplay;
    private void Awake()
    {
        scoreDisplay = GetComponent<TMP_Text>();
    }

    string FormatScore(int value)
    {
        string scoreString = value.ToString();

        if (value < 1000)
            scoreString = "0" + scoreString;
        if (value <= 100)
            scoreString = "0" + scoreString;
        if (value <= 10)
            scoreString = "0" + scoreString;

        return scoreString;
    }



    private void OnEnable()
    {
        EventManager.onScoreUpdated += UpdateScore;
    }
    private void OnDisable()
    {
        EventManager.onScoreUpdated -= UpdateScore;
    }

    void UpdateScore(int value)
    {
        scoreDisplay.text = FormatScore(value);
    }
}
