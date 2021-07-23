using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MossGiant : Enemy, IDamageable
{
    public int Health { get; set; }
    
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
        
    }
}
