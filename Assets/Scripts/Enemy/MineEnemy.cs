using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MineEnemy : Enemy
{
    public GameObject minePlus;
    
    private void Awake()
    {
        points = 350;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Enemy>() != null &&
            collision.gameObject.GetComponent<Enemy>().GetType() == typeof(CommandShipEnemy))
        {
            var newMinePlus = Instantiate(minePlus, transform.position, transform.rotation);
            newMinePlus.transform.position += new Vector3(0, -0.07f, 0);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(collision.gameObject);
        }
    }
}
