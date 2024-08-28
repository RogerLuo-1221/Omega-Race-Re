using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PlayerController playerController = FindObjectOfType<PlayerController>();
            playerController.DestroyEnemy(collision.gameObject);
        }
        
        Destroy(gameObject); 
    }
    
    
}
