using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidProjectile : MonoBehaviour, IProjectile
{
    [SerializeField] private float _damage,_destructionDelay, _speed;
    [SerializeField] private Transform _offset;
    
    public float Damage { get; set; }
    public float Speed { get; set; }
    public Transform Offset { get; set; }

    private void Start()
    {
        Damage = _damage;
        Speed = _speed;
        Offset = _offset;
        
        Destroy(this.gameObject, _destructionDelay);

        StartCoroutine(MoveRoutine());
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
            
            hit.Damage();
            Destruction();
        }

        if (!other.CompareTag("Enemy"))
        {
            Destruction();
        }
    }

    IEnumerator MoveRoutine()
    {
        while (true)
        {
            transform.Translate(Vector3.right * (Time.deltaTime * Speed));
            yield return new WaitForEndOfFrame();
        }
    }


    public void Destruction()
    {
        Destroy(this.gameObject);
    }
}
