using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    public bool IsAlive { get; set; }
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base.health;
        IsAlive = true;
    }

    public override void Update()
    {
        base.Update();
        
        // if player x < zombie start || player x > zombie end
            // zombie canMove
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
            IsAlive = false;
            base.isAlive = IsAlive;
        }
    }
    
}
