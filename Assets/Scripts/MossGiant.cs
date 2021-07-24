using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class MossGiant : Enemy, IDamageable
{
    public bool IsAlive { get; set; }
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base.health;
        gems = Random.Range(minGems, maxGems);
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

            StartCoroutine(SpawnDiamondsRoutine());
        }
    }
    
    IEnumerator SpawnDiamondsRoutine()
    {
        Vector3 randomOffset = transform.position;
        while (gems > 0)
        {
            randomOffset.x += UnityEngine.Random.Range(-.3f, .3f);
            Instantiate(diamondPrefab, randomOffset, Quaternion.identity);
            gems -= 1;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
