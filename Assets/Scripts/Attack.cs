using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private bool _canDamage = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();
        if (hit == null)
            return;
        
        if (_canDamage)
        {
            hit.Damage();
            _canDamage = false;
        }
    }

    private void CanAttack()
    {
        _canDamage = true;
    }

    private void OnEnable()
    {
        SpriteHelper.OnAttackEnded += CanAttack;
    }

    private void OnDisable()
    {
        SpriteHelper.OnAttackEnded -= CanAttack;
    }
}
