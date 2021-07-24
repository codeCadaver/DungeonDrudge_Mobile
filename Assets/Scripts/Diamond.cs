using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public static Action<int> OnDiamondCollected;

    [field: SerializeField]
    public int Value { get; set; } = 1;
    
    private Animator _animator;
    private bool _canCollect = false;
    [SerializeField] private float _height = 20f;
    [SerializeField] private float _speed = 5f;
    private Rigidbody2D _rigidbody2D;
    private GameObject _target;
    

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_canCollect)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, _target.transform.position, Time.deltaTime * _speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!_canCollect)
            {
                _target = other.gameObject;
                _rigidbody2D.velocity = Vector2.up * (_height * Time.deltaTime);
                _rigidbody2D.gravityScale = 1f;
            }
            else
            {
                // add to player score
                OnDiamondCollected?.Invoke(Value);
                Destroy(this.gameObject);
            }
        }
        
    }

    IEnumerator CollectRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        _canCollect = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(CollectRoutine());
        }
    }
}
