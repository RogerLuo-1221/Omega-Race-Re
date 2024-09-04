using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandShipEnemy : Enemy
{
    public float speed;
    public Vector2 direction;
    public float directionChangeInterval;
    public float fireInterval;
    public float dropInterval;
    public GameObject projectilePrefab;
    public GameObject minePrefab;
    public GameObject minePlusPrefab;
    
    private void Awake()
    {
        points = 1500;
        
        speed = 0.3f;
        directionChangeInterval = 10f;
        fireInterval = 2f;
        dropInterval = 10f;

        StartCoroutine(ChangeDirection());
        StartCoroutine(Fire());
        StartCoroutine(DropMine());
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            var playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            
            var fireDirection = (playerTransform.position - transform.position).normalized;
            float angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, angle));
            projectile.transform.eulerAngles += new Vector3(0, 0, 90);
            
            Rigidbody2D projectileRb = projectile.GetComponent<Rigidbody2D>();
            projectileRb.velocity = fireDirection * 4f;
            
            yield return new WaitForSeconds(fireInterval);
        }
    }
    
    private IEnumerator ChangeDirection()
    {
        while (true)
        {
            direction = GetRandomDirection();

            yield return new WaitForSeconds(directionChangeInterval);
        }
    }
    
    private IEnumerator DropMine()
    {
        while (true)
        {
            var selector = Random.Range(0, 2);
            switch (selector)
            {
                case 0:
                    Instantiate(minePrefab, transform.position, Quaternion.identity);
                    break;
                case 1:
                    Instantiate(minePlusPrefab, transform.position, Quaternion.identity);
                    break;
            }

            yield return new WaitForSeconds(dropInterval);
        }
    }

    private Vector2 GetRandomDirection()
    {
        float angle = Random.Range(0f, 360f);
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)).normalized;
    }

    protected override void Move()
    {
        rb.velocity = direction * speed;
    }
}
