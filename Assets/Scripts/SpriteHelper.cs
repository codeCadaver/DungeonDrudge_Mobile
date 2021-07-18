using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHelper : MonoBehaviour
{
    public static Action OnAttackEnded;

    public void AttackEnded()
    {
        OnAttackEnded?.Invoke();
    }


}
