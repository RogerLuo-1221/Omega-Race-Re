using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    
    private float _safeDistance = 2.5f;
    private float _spawnInterval = 4f;
    private float _timer;

    private float _difficultyTimer;

    public int enemyCount;
    
    private void Update()
    {
        _timer += Time.deltaTime;
        _difficultyTimer += Time.deltaTime;
        
        if (_timer >= _spawnInterval)
        {
            EnemySpawn();
            _timer = 0;
        }

        /*if (_difficultyTimer >= 20f) 
        {
            DifficultyUpdate();
            _difficultyTimer = 0;
        }*/
    }

    public void DifficultyUpdate()
    {
        if (_spawnInterval > 2)
        {
            _spawnInterval -= 1;
        }
    }
    
    private void EnemySpawn()
    {
        Vector2 spawnPos;
        var playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        do
        {
            var x = Random.Range(3.1f, 7.8f) * (Random.Range(0, 2) * 2 - 1);
            var y = Random.Range(1.6f, 3.9f) * (Random.Range(0, 2) * 2 - 1);
            spawnPos = new Vector2(x, y);
        } while (Vector2.Distance(spawnPos, playerTransform.position) < _safeDistance);
        
        var enemyIndex = Random.Range(0, 10);

        if (enemyIndex == 0)
        {
            Instantiate(enemyPrefabs[2], spawnPos, Quaternion.identity);
        }
        else if (enemyIndex is >= 1 and <= 7)
        {
            Instantiate(enemyPrefabs[0], spawnPos, Quaternion.identity);
        }
        else if (enemyIndex is >= 8 and <= 9)
        {
            Instantiate(enemyPrefabs[1], spawnPos, Quaternion.identity);
        }
    }
}
