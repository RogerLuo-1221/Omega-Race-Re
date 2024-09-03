using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePlusEnemy : Enemy
{
    public GameObject explode;
    
    private void Awake()
    {
        points = 500;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<PlayerController>().isInvincible)
            {
                Instantiate(explode, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else collision.gameObject.GetComponent<PlayerController>().Damaged();
        }
        else if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(collision.gameObject);
            PlayerController playerController = FindObjectOfType<PlayerController>();
            playerController.DestroyEnemy(gameObject);
        }
    }
}
