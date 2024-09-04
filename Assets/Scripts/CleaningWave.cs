using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaningWave : MonoBehaviour
{
    private float _timer;
    private CircleCollider2D _collider;
    
    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        
        Debug.Log(_timer);

        var scale = _timer * 8;

        transform.localScale = new Vector3(scale, scale, scale);
        _collider.radius = scale * 0.5f;
        
        if (_timer >= 5) Destroy(gameObject);
    }
    
    
}
