using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandShipEnemy : Enemy
{
    public float speed;
    public Vector2 direction;
    public float directionChangeInterval;
    
    private void Awake()
    {
        points = 1500;
        
        speed = 0.2f;
        directionChangeInterval = 15f;

        StartCoroutine(ChangeDirection());
    }
    
    private IEnumerator ChangeDirection()
    {
        while (true)
        {
            direction = GetRandomDirection();

            yield return new WaitForSeconds(directionChangeInterval);
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
