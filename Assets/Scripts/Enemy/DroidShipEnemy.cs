using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidShipEnemy : Enemy
{
    private void Awake()
    {
        points = 1000;
    }

    protected override void Move()
    {
        if (Input.GetKey(KeyCode.K))
        {
            rb.AddForce(transform.right * (200f * Time.deltaTime));
        }
    }
}
