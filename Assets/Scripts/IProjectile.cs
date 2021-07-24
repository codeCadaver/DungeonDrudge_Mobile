using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    float Speed { get; set; }
    float Damage { get; set; }
    
    Transform Offset { get; set; }

    void Destruction();

}
