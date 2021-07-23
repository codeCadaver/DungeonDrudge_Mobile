using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] protected int gems;
    [SerializeField] protected float playerDetectionDistance = 2f;
    [SerializeField] protected float speed;

    [SerializeField] protected Transform start, end;

    [SerializeField] protected string idleName, hitName = "Hit";

    protected Animator animator;
    protected bool canMove = true;
    protected bool movingRight = true;
    protected int combatHash = Animator.StringToHash("inCombat");
    protected int hitHash = Animator.StringToHash("Hit");
    protected int idleHash = Animator.StringToHash("Idle");
    protected Player player;
    protected SpriteRenderer sprite;

    public virtual void Init()
    {
        animator = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
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
            if (animator.GetBool(combatHash) == false)
            {
                return;
            }
        }

        // if (animator.GetCurrentAnimatorStateInfo(0).IsName(hitName))
        // {
        //     return;
        // }
        
        sprite.flipX = !direction;
        
        Transform target = direction ? end : start;

        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        CheckPlayerDistance();

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
        
        if (animator.GetBool(combatHash) == true)
        {
            Vector3 playerDirection = player.transform.localPosition - transform.localPosition;
            sprite.flipX = playerDirection.x < 0 ? true : false;
        }
    }

    public void CheckPlayerDistance()
    {
        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);
        if (distance > playerDetectionDistance)
        {
            canMove = true;
            animator.SetBool(combatHash, false);
        }
    }

}
