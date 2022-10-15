using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenToggler : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;
    [SerializeField] private TMP_Text endScore;

    private void OnEnable()
    {
        EventManager.onGameComplete += CompleteGame;
    }

    private void OnDisable()
    {
        EventManager.onGameComplete -= CompleteGame;
    }

    void CompleteGame()
    {
        endScreen.SetActive(true);
        endScore.text = "Your score: " + FormatScore(PlayerPrefs.GetInt("playerScore"));
        GameSetup.ResetProgress();
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
}
