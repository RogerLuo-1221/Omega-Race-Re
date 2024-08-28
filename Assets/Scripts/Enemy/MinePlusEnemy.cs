using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePlusEnemy : Enemy
{
    private void Awake()
    {
        points = 500;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
}
