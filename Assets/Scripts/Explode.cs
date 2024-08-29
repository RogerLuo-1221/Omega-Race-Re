using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(DestroyExplode());
    }

    private IEnumerator DestroyExplode()
    {
        yield return new WaitForSeconds(1.2f);
        
        Destroy(gameObject);
    }
}
