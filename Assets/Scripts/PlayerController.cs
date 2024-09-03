using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed;
    public float thrustSpeed;
    public Rigidbody2D rb;
    public float projectileSpeed;
    public GameObject projectilePrefab;
    public Transform spawnPos;

    public SpriteRenderer spriteRenderer;
    public Sprite ship;
    public Sprite acceleration;
    
    public bool isInvincible;

    public GameObject explodePrefab;

    public GameManager gameManager;
    private ScoreManager _scoreManager;

    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
        rotationSpeed = 150f;
        thrustSpeed = 200f;
        rb = GetComponent<Rigidbody2D>();
        projectileSpeed = 5f;
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        _scoreManager = FindObjectOfType<ScoreManager>();
        gameManager = FindObjectOfType<GameManager>();

        BeInvincible();
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

        if (Input.GetKeyDown(KeyCode.K))
        {
            spriteRenderer.sprite = acceleration;
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            spriteRenderer.sprite = ship;
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
            if (isInvincible)
            {
                DestroyEnemy(collision.gameObject);
            }
            else Damaged();
        }
    }

    public void Damaged()
    {
        gameManager.LoseLife();
        
        Respawn();
    }

    private void Respawn()
    {
        Instantiate(explodePrefab, transform.position, Quaternion.identity);
        Instantiate(gameObject, spawnPos.position, new Quaternion(0, 0, 90, 0));
        
        StartCoroutine(Invincible());
        
        Destroy(gameObject);
    }

    void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
        projectileRb.velocity = transform.up * projectileSpeed;
    }
    
    public void DestroyEnemy(GameObject enemy)
    {
        if (enemy.layer == 9)
        {
            var points = enemy.GetComponent<Enemy>().points;

            _scoreManager.AddPoints(points);

            Instantiate(explodePrefab, enemy.transform.position, Quaternion.identity);
        }
        
        Destroy(enemy);
    }

    public void BeInvincible()
    {
        Debug.Log("called");
        StartCoroutine(Invincible());
    }
    
    private IEnumerator Invincible()
    {
        isInvincible = true;
        Physics2D.IgnoreLayerCollision(6, 8, true);
        
        var isTranslucent = false;

        for (int i = 0; i < 30; i++)
        {
            yield return new WaitForSeconds(0.3f);
            
            if (isTranslucent)
            {
                spriteRenderer.color = new Color(1, 1, 1, 1f);
                isTranslucent = false;
            }
            else
            {
                spriteRenderer.color = new Color(1, 1, 1, 0.1f);
                isTranslucent = true;
            }
        }
        
        isInvincible = false;
        Physics2D.IgnoreLayerCollision(6, 8, false);
    }
}
