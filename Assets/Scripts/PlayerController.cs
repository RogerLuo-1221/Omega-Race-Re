using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float rotationSpeed;
    public float thrustSpeed;
    public Rigidbody2D rb;
    public float projectileSpeed;
    public GameObject projectilePrefab;
    public Transform spawnPos;
    public float fireRate;

    private float _fireTimer;

    public SpriteRenderer spriteRenderer;
    public Sprite ship;
    public Sprite acceleration;
    
    public bool isInvincible;
    public Slider invincibleSlider;
    public RectTransform invincibleSliderRect;
    public bool fireLevelUp;

    public GameObject explodePrefab;

    public GameManager gameManager;
    private ScoreManager _scoreManager;

    private void Start()
    {
        rotationSpeed = 200f;
        thrustSpeed = 200f;
        rb = GetComponent<Rigidbody2D>();
        projectileSpeed = 8f;
        fireRate = 0.4f;
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        _scoreManager = FindObjectOfType<ScoreManager>();
        gameManager = FindObjectOfType<GameManager>();

        BeInvincible(6);
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

        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.up * (thrustSpeed * Time.deltaTime));
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            spriteRenderer.sprite = acceleration;
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            spriteRenderer.sprite = ship;
        }
        
        _fireTimer += Time.deltaTime;
        
        if (Input.GetKey(KeyCode.J) && _fireTimer >= fireRate)
        {
            FireProjectile();
            _fireTimer = 0;
        }

        InvincibleSliderFollow();
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
        AudioManager.instance.PlaySfx(AudioManager.instance.playerDie);
        
        gameManager.LoseLife();
    }

    public void Respawn()
    {
        Instantiate(explodePrefab, transform.position, Quaternion.identity);
        
        transform.SetPositionAndRotation(spawnPos.position, spawnPos.rotation);
        rb.velocity = Vector2.zero;
        
        BeInvincible(6);
    }

    void FireProjectile()
    {
        if (fireLevelUp)
        {
            Fire(15);
            Fire(-15);
        }
        Fire(0);
        
        AudioManager.instance.PlaySfx(AudioManager.instance.playerFire);
    }
    
    public void DestroyEnemy(GameObject enemy)
    {
        if (enemy.layer == 9)
        {
            var points = enemy.GetComponent<Enemy>().points;

            _scoreManager.AddPoints(points);

            Instantiate(explodePrefab, enemy.transform.position, Quaternion.identity);
        }
        
        AudioManager.instance.PlaySfx(AudioManager.instance.enemyDie);
        Destroy(enemy);
    }

    public void BeInvincible(float duration)
    {
        StartCoroutine(Invincible(duration));
    }
    
    private IEnumerator Invincible(float duration)
    {
        isInvincible = true;

        invincibleSlider.maxValue = duration;
        invincibleSlider.value = duration;
        invincibleSlider.gameObject.SetActive(true);
        
        Physics2D.IgnoreLayerCollision(6, 8, true);
        
        var isTranslucent = false;

        var times = (int)(duration / 0.3);
        for (var i = 0; i < times; i++)
        {
            yield return new WaitForSeconds(0.3f);
            
            invincibleSlider.value -= 0.3f;
            
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
        invincibleSlider.gameObject.SetActive(false);
        Physics2D.IgnoreLayerCollision(6, 8, false);
    }

    private void InvincibleSliderFollow()
    {
        invincibleSliderRect.position = transform.position + new Vector3(0, 0.6f, 0);
    }

    private void Fire(float angle)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        Vector2 direction = rotation * transform.up;
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
        projectile.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

    public void FireRateUpgrade()
    {
        if (fireRate > 0.1) fireRate -= 0.1f;
    }

    public void FireLevelUp(float second)
    {
        StartCoroutine(TriFire(second));
    }

    private IEnumerator TriFire(float time)
    {
        fireLevelUp = true;
        fireRate = 0.2f;
        
        yield return new WaitForSeconds(time);
        
        fireLevelUp = false;
        fireRate = 0.4f;
    }
}
