using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    
    public float safeDistance = 2f;
    
    public float spawnInterval = 3f;
    
    private float _timer;
    
    private void Update()
    {
        _timer += Time.deltaTime;
        
        if (_timer >= spawnInterval)
        {
            EnemySpawn();
            _timer = 0;
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
        } while (Vector2.Distance(spawnPos, playerTransform.position) < safeDistance);
        
        var enemyIndex = Random.Range(0, enemyPrefabs.Length);
        
        Instantiate(enemyPrefabs[enemyIndex], spawnPos, Quaternion.identity);
    }
}
