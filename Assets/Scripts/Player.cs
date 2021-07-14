using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 5f;

    private bool _isGrounded = false;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement();
        // Debug.DrawRay(transform.position, Vector3.down * 0.6f, Color.green);
    }

    private void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        
        Jump();
        
        _rigidbody2D.velocity = new Vector2(move, _rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsGrounded();
            if (!_isGrounded) { return; }

            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
            _isGrounded = false;
        }
    }

    private void IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, 1 << 8);
        
        if (hit.collider == null) return;
        
        _isGrounded = true;

        Debug.Log($"Collided with: {hit.collider.name}");
    }
}
