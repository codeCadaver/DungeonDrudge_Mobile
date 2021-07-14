using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");

        _rigidbody2D.velocity = new Vector2(h, _rigidbody2D.velocity.y);
    }

    private void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        _rigidbody2D.velocity = new Vector2(horizontal, _rigidbody2D.velocity.y);
    }
}
