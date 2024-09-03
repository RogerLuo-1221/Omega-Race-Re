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
    public TMP_Text lapText;

    private void Start()
    {
        lapNumber = 2;
        LapNumberUpdate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            (lapLine.transform.position, spawnPos.transform.position) = 
                (spawnPos.transform.position, lapLine.transform.position);
            
            lapNumber--;

            if (lapNumber == 0)
            {
                lapNumber = Random.Range(2, 7);
                EventHappen();
            }
            LapNumberUpdate();
        }
    }

    private void EventHappen()
    {
        //var player = GameObject.FindGameObjectWithTag("Player").gameObject;
        //player.GetComponent<PlayerController>().BeInvincible();
        
        var gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.playerLives++;
    }

    private void LapNumberUpdate()
    {
        lapText.text = "in " + lapNumber + " laps";
    }
}
