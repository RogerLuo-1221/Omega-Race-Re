using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class LapLine : MonoBehaviour
{
    public GameObject lapLine;
    public GameObject spawnPos;
    
    public int lapNumber;
    public TMP_Text lapNumberText;
    
    public float lapTimer;
    public TMP_Text lapTimerText;
    
    public ScoreManager scoreManager;
    public EnemySpawnManager enemySpawnManager;

    private void Start()
    {
        lapNumber = 2;
        lapTimer = 25;
        LapNumberTextUpdate();
    }

    private void Update()
    {
        LapTimerTextUpdate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            (lapLine.transform.position, spawnPos.transform.position) = 
                (spawnPos.transform.position, lapLine.transform.position);
            spawnPos.transform.eulerAngles = new Vector3(0, 0, spawnPos.transform.eulerAngles.z + 180);
            
            lapNumber--;

            if (lapNumber == 0) LapUpdate();
            
            LapNumberTextUpdate();
            
            AudioManager.instance.PlaySfx(AudioManager.instance.lapComplete);
        }
    }

    private void LapUpdate()
    {
        if (lapTimer > 0) scoreManager.AddPoints(10000);
        
        lapNumber = 3;
        lapTimer = 25;
        
        EventHappen();
    }

    private void EventHappen()
    {
        var player = GameObject.FindGameObjectWithTag("Player").gameObject;
        
        player.GetComponent<PlayerController>().FireRateUpgrade();
        
        enemySpawnManager.DifficultyUpdate();
        
        var eventSelector = Random.Range(1, 4);
        Debug.Log(eventSelector);

        switch (eventSelector)
        {
            case 1:
                // Invincible
                player.GetComponent<PlayerController>().BeInvincible(12);
                break; 
            case 2:
                // Firearm level up
                player.GetComponent<PlayerController>().FireLevelUp(18);
                break;
            case 3:
                // Extra life
                var gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
                gameManager.ExtraLife();
                break;
        }
        
        // Enemy Clean
        
        // Mine on Boundary
    }

    private void LapNumberTextUpdate()
    {
        lapNumberText.text = "in " + lapNumber + " laps";
    }

    private void LapTimerTextUpdate()
    {
        if (lapTimer <= 0) return;
        
        lapTimer -= Time.deltaTime;
        lapTimerText.text = "bonus in " + lapTimer.ToString("F1") + " seconds";
    }
}
