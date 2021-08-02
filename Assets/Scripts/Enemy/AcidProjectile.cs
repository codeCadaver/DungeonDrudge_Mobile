using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float _damage,_destructionDelay, _speed;
    [SerializeField] private Transform _offset;
    [SerializeField] private int _attackStrength = 1;

    public float Damage { get; set; }
    public float Speed { get; set; }
    public Transform Offset { get; set; }

    private bool _flipDirection = false;

    private void Start()
    {
        Damage = _damage;
        Speed = _speed;
        Offset = _offset;
        
        Destroy(this.gameObject, _destructionDelay);

        StartCoroutine(MoveRoutine());
    }

    public bool FlipDirection(bool direction)
    {
        return _flipDirection = direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            IDamageable hit = other.GetComponent<IDamageable>();

            if (hit == null)
            {
                return;
            }
            
            hit.Damage(_attackStrength);
            Destruction();
        }

        if (!other.CompareTag("Enemy"))
        {
            Destruction();
        }
    }

    IEnumerator MoveRoutine()
    {
        Vector2 direction = _flipDirection ? Vector2.left : Vector2.right; 
        while (true)
        {
            transform.Translate(transform.InverseTransformDirection(direction * (Time.deltaTime * Speed)));
            yield return new WaitForEndOfFrame();
        }
    }


    public void Destruction()
    {
        Destroy(this.gameObject);
    }
}
