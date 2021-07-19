using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Action OnPlayerAttacked;
    public static Action<float> OnPlayerMoved;
    public static Action<bool> OnPlayerJumping;

    [SerializeField] private float _attackDelay = 0.2f;
    [SerializeField] private float _groundCheckDistance = 0.7f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _speed = 5f;

    private bool _canAttack = true;
    private bool _canMove = true;
    private bool _isGrounded = false;
    private bool _resetJump = true;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;
    private Vector3 _localArcPos;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        _localArcPos = _swordArcSprite.transform.localPosition;
    }

    private void Update()
    {
        // Debug.Log(IsGrounded());
        Movement();
        Attack();
        Debug.DrawRay(transform.position, Vector3.down * _groundCheckDistance, Color.green);
    }

    private void Movement()
    {
        if (_canMove)
        {
            float move = Input.GetAxisRaw("Horizontal");
            Jump();
            
            _rigidbody2D.velocity = new Vector2(move * _speed, _rigidbody2D.velocity.y);
            
            FlipSprite(move);
            
            OnPlayerMoved?.Invoke(move);
            // _playerAnimation.Move(move);
            
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsGrounded())
            {
                if (_canAttack)
                {
                    StartCoroutine(AttackRoutine());
                }
                // _canMove = false;
                // _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
                // OnPlayerAttacked?.Invoke();
                // _playerAnimation.Attack();
            }
        }
    }

    IEnumerator AttackRoutine()
    {
        _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
        OnPlayerAttacked?.Invoke();
        _canAttack = false;
        yield return new WaitForSeconds(_attackDelay);
        _canAttack = true;
    }

    public void CanMove()
    {
        _canMove = true;
    }

    private void Jump()
    {
        IsGrounded();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!IsGrounded())
            {
                return;
            }
            _resetJump = false;
            // _playerAnimation.IsJumping(true);
            OnPlayerJumping?.Invoke(true);
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
            // delay routine
            StartCoroutine(JumpDelayRoutine());
        }
    }

    private bool IsGrounded()
    {
        if (_resetJump)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _groundCheckDistance, 1 << 8);
            
            if (hit.collider == null) { return false;}
            OnPlayerJumping?.Invoke(false);
            // _playerAnimation.IsJumping(false);
            return true;
        }
        return false;
    }

    private void FlipSprite(float speed)
    {
        if (speed != 0)
        {
            Vector3 xPos = _localArcPos;
            _playerSprite.flipX = speed < 0 ? true : false;
            _swordArcSprite.flipY = speed < 0 ? true : false;
            xPos.x *= Mathf.Sign(speed) * _localArcPos.x;
            
            //TODO: adjust rotation of sword arc 
            if (Mathf.Sign(speed) < 0)
            {
                xPos.x -= 0.3f;
            }
            _swordArcSprite.transform.localPosition = xPos;
        }
    }

    IEnumerator JumpDelayRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        _resetJump = true;
    }

    private void OnEnable()
    {
        SpriteHelper.OnAttackEnded += CanMove;
    }

    private void OnDisable()
    {
        SpriteHelper.OnAttackEnded -= CanMove;
    }
}
