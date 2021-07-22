using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MossGiant : Enemy
{
    private Animator _animator;
    private bool _movingRight = true;
    private int _idleHash = Animator.StringToHash("Idle");
    private SpriteRenderer _sprite;
    
    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    } 

    public override void Update()
    {
        Movement(_movingRight);
    }

    private void Movement(bool direction)
    {
        // don't move character while in idle
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("MossGiant_Idle"))
        {
            return;
        }
        
        // face forward direction
        _sprite.flipX = !_movingRight;
        
        Transform target = direction ? end : start;
        
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (transform.position == end.position)
        {
            _movingRight = false;
        }

        else if (transform.position == start.position)
        {
            _movingRight = true;
        }

        if (transform.position == target.position)
        {
            _animator.SetTrigger(_idleHash);
        }

        
    }

    IEnumerator FlipDelayRoutine(bool flip)
    {
        yield return new WaitForSeconds(0.5f);
        _sprite.flipX = flip;
    }
}
