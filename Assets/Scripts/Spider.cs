using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Spider : Enemy, IDamageable
{
    public bool IsAlive { get; set; }
    public int Health { get; set; }
    
    public override void Init()
    {
        base.Init();
        Health = base.health;
    }

    protected override void Start()
    {
        base.Start();
        IsAlive = true;
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Movement(bool direction)
    {
        // no movement
    }
    

    public void Damage()
    {
        Health -= 1;
        animator.SetTrigger(hitHash);
        animator.SetBool(combatHash, true);
        if (Health <= 0)
        {
            animator.SetBool(combatHash, false);
            animator.SetTrigger("Dead");
            IsAlive = false;
            base.isAlive = IsAlive;
        }
    }

}
