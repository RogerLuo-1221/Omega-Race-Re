using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int lifeCount;
    public float rotationSpeed;
    public float thrustSpeed;
    public Rigidbody2D rb;
    public float projectileSpeed;
    public GameObject projectilePrefab;
    
    private ScoreManager _scoreManager;

    private void Start()
    {
        lifeCount = 3;
        
        rotationSpeed = 150f;
        thrustSpeed = 200f;
        rb = GetComponent<Rigidbody2D>();
        projectileSpeed = 5f;

        _scoreManager = FindObjectOfType<ScoreManager>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, 0, -rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.K))
        {
            rb.AddForce(transform.up * (thrustSpeed * Time.deltaTime));
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            FireProjectile();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            lifeCount--;
            Destroy(gameObject);
        }
    }

    void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.velocity = transform.up * projectileSpeed;
    }
    
    public void DestroyEnemy(GameObject enemy)
    {
        var points = enemy.GetComponent<Enemy>().points;

        _scoreManager.AddPoints(points);
        
        Destroy(enemy);
    }
}
