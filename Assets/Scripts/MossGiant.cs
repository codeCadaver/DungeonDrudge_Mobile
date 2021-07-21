using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MossGiant : Enemy
{
    private bool _movingRight = true;
    
    private void Start()
    {
        
    } 

    public override void Update()
    {
        // if at start, moving right = true
        // if at end, moving right = false
        // move right at beginning
        Movement(_movingRight);
    }

    private void Movement(bool direction)
    {
        Transform target = direction ? end : start;
        
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            
        if (transform.position == end.position)
            _movingRight = false;
        if (transform.position == start.position)
            _movingRight = true;
    }
}
