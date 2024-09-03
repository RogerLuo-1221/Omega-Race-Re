using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathShipEnemy : Enemy
{
    public float speed;
    public Vector2 direction;
    public float directionChangeInterval;
    
    private void Awake()
    {
        points = 1500;
        
        speed = 2.5f;
        directionChangeInterval = 1.5f;

        StartCoroutine(ChangeDirection());
    }
    
    private IEnumerator ChangeDirection()
    {
        while (true)
        {
            var palyerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            direction = (palyerTransform.position - transform.position).normalized;

            yield return new WaitForSeconds(directionChangeInterval);
        }
    }

    protected override void Move()
    {
        rb.velocity = direction * speed;
    }
}
