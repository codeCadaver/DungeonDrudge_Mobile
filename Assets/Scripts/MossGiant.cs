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
        if (animator.GetBool(combatHash) == true)
        {
            Vector3 playerDirection = player.transform.localPosition - transform.localPosition;
            sprite.flipX = playerDirection.x < 0 ? true : false;
        }
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
