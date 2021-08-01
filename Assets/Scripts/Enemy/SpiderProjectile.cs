using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderProjectile : MonoBehaviour
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _offset;

    public float Speed { get; set; }
    public float Damage { get; set; }
    public Transform Offset { get; set; }

    private void Start()
    {
        Offset = _offset;
    }

    public void Fire()
    {
        GameObject projectile = Instantiate(_projectile, Offset.position, Quaternion.identity);
    }

    // private void OnEnable()
    // {
    //     Spider.OnPlayerDetected += Fire;
    // }
    //
    // private void OnDisable()
    // {
    //     Spider.OnPlayerDetected -= Fire;
    // }
}
