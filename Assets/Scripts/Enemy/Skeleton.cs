using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Skeleton : Enemy, IDamageable
{
    public bool IsAlive { get; set; }
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base.health;
        IsAlive = true;
        gems = Random.Range(minGems, maxGems);
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
