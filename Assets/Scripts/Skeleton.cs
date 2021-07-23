using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base.health;
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
            Destroy(this.gameObject);
        }
    }
    
}
