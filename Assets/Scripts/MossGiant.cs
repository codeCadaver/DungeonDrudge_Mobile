using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MossGiant : Enemy
{
    private string _animName = "MossGiant_Idle";
    
    private void Start()
    {
        Init();
    } 

    public override void Update()
    {
        Movement(movingRight, _animName);
    }

    public override void Movement(bool direction, string animName)
    {
        base.Movement(direction, animName);
    }
}
