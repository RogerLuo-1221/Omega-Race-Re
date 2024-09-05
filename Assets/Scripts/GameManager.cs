using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverBanner;
    public GameObject player;
    public ScoreManager scoreManager;
    
    public int playerLives;
    public TMP_Text playerLivesText;
    
    void Start()
    {
        player  = GameObject.FindGameObjectWithTag("Player");
        
        Time.timeScale = 1f;
        gameOverBanner.SetActive(false);

        playerLives = 3;
        UpdatePlayerLivesText();
    }

    
    public void LoseLife()
    {
        playerLives--;
        UpdatePlayerLivesText();

        if (playerLives > 0) player.GetComponent<PlayerController>().Respawn();
        else
        {
            Destroy(player);
            GameOver();
        }
    }

    public void ExtraLife()
    {
        playerLives++;
        UpdatePlayerLivesText();
    }

    private void UpdatePlayerLivesText()
    {
        playerLivesText.text = playerLives.ToString();
    }

    private void GameOver()
    {
        scoreManager.GetComponent<ScoreManager>().UpdateHighestScore();
        
        Time.timeScale = 0f;
        gameOverBanner.SetActive(true);
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
