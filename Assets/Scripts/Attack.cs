using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    
    [SerializeField] private string _attackString = "Attack";
    [SerializeField] private int _attackStrength = 1;
    private bool _canDamage = true;
    private BoxCollider2D _boxCollider2D;

    private void Start()
    {
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        // reset _canDamage
        if (!_boxCollider2D.isActiveAndEnabled)
        {
            _canDamage = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable hit = other.GetComponent<IDamageable>();
        if (hit == null)
            return;
        
        if (_canDamage && hit.IsAlive)
        {
            hit.Damage(_attackStrength);
            _canDamage = false;
        }
    }

    private void CanAttack()
    {
        _canDamage = true;
    }

    public void UpdateAttackStrength(int increasedAmount)
    {
        _attackStrength += increasedAmount;
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
