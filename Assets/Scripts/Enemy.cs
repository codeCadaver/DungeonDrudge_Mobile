using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int speed;
    [SerializeField] protected int gems;

    [SerializeField] protected Transform start, end;

    protected Animator animator;
    protected bool movingRight = true;
    protected int idleHash = Animator.StringToHash("Idle");
    protected SpriteRenderer sprite;

    public virtual void Init()
    {
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    
    public abstract void Update();
    

    public virtual void Movement(bool direction, string animName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animName))
        {
            return;
        }
        
        sprite.flipX = !direction;
        
        Transform target = direction ? end : start;

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (transform.position == target.position)
        {
            animator.SetTrigger(idleHash);
        }

        if (transform.position == start.position)
        {
            movingRight = true;
        }
        else if (transform.position == end.position)
        {
            movingRight = false;
        }
    }

}
