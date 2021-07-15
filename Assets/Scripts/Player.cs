using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _groundCheckDistance = 0.7f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _speed = 5f;

    private bool _isGrounded = false;
    private PlayerAnimation _playerAnimation;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _playerSprite;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerAnimation = GetComponent<PlayerAnimation>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        Movement();
        Debug.DrawRay(transform.position, Vector3.down * _groundCheckDistance, Color.green);
    }

    private void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");
        
        Jump();
        
        _rigidbody2D.velocity = new Vector2(move * _speed, _rigidbody2D.velocity.y);
        
        FlipSprite(move);
        
        _playerAnimation.Move(move);
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsGrounded();
            if (!IsGrounded()) { return; }

            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
        }
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, 1 << 8);
        
        if (hit.collider == null) return false;
        
        return true;
    }

    private void FlipSprite(float speed)
    {
        if (speed != 0)
        {
            _playerSprite.flipX = speed < 0 ? true : false;
        }
    }
}
