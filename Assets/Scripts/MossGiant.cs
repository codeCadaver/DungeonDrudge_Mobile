using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MossGiant : Enemy, IDamageable
{
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    protected override void Start()
    {
        base.Start();
    } 

    public override void Update()
    {
        base.Update();
    }

    public override void Movement(bool direction)
    {
        base.Movement(direction);
       
    }

    public void Damage()
    {
        canMove = false;
        Health -= 1;
        animator.SetTrigger(hitHash);
        animator.SetBool(combatHash, true);
        if (Health <= 0)
        {
            animator.SetBool(combatHash, false);
            animator.SetTrigger("Dead");
        }
    }
}
