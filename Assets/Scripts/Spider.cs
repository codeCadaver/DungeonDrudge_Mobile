using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Spider : Enemy, IDamageable
{
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
    
    public int Health { get; set; }

    public void Damage()
    {
        
    }
}
