using System.Collections;
using UnityEngine;

public class DroidShipEnemy : Enemy
{
    public float speed;
    public Vector2 direction;
    public float directionChangeInterval;
    public GameObject commandShipPrefab;

    private float _evolutionTimer;

    private void Awake()
    {
        points = 1000;
        speed = 0.1f;
        directionChangeInterval = 10f;

        StartCoroutine(ChangeDirection());
    }

    private void Update()
    {
        _evolutionTimer += Time.deltaTime;

        if (_evolutionTimer >= 8) Evolution();
    }

    private void Evolution()
    {
        Instantiate(commandShipPrefab, transform.position, transform.rotation);
        
        Destroy(gameObject);
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