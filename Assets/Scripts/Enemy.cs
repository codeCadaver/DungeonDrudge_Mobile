using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int gems;
    [SerializeField] protected float speed;

    [SerializeField] protected Transform start, end;

    [SerializeField] protected string idleName, hitName = "Hit";

    protected Animator animator;
    protected bool movingRight = true;
    protected int combatHash = Animator.StringToHash("inCombat");
    protected int hitHash = Animator.StringToHash("Hit");
    protected int idleHash = Animator.StringToHash("Idle");
    protected SpriteRenderer sprite;

    public virtual void Init()
    {
        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        Movement(movingRight);
    }
    

    public virtual void Movement(bool direction)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(idleName))
        {
            return;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName(hitName))
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
