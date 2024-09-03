using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMPro.TMP_Text scoreText;
    private int _playerScore;

    private void Start()
    {
        UpdateScoreText();
    }
    
    public void AddPoints(int points)
    {
        _playerScore += points;
        UpdateScoreText();
    }
    
    private void UpdateScoreText()
    {
        scoreText.text = _playerScore.ToString();
    }
}
