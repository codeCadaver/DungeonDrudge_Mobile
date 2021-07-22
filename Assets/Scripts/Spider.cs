using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Spider : Enemy
{
    private string _animName = "Spider_Idle";
    
    // Start is called before the first frame update
    void Start()
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
