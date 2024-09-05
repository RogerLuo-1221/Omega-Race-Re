using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMPro.TMP_Text highestScoreText;
    public TMPro.TMP_Text scoreText;
    
    private int _highestScore;
    private int _playerScore;

    private void Start()
    {
        _highestScore = PlayerPrefs.GetInt("HighestScore", 0);
        highestScoreText.text = _highestScore.ToString();
        
        UpdateScoreText();
    }
    
    public void AddPoints(int points)
    {
        _playerScore += points;
        UpdateScoreText();
    }

    public void UpdateHighestScore()
    {
        if (_playerScore <= _highestScore) return;
        
        PlayerPrefs.SetInt("HighestScore", _playerScore);
        PlayerPrefs.Save();
    }
    
    private void UpdateScoreText()
    {
        scoreText.text = _playerScore.ToString();
    }
}
