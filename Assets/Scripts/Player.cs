using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public static Action OnPlayerAttacked;
    public static Action<float> OnPlayerMoved;
    public static Action<bool> OnPlayerJumping;

    public bool IsAlive { get; set; }
    public int Health { get; set; }

    [SerializeField] private float _attackDelay = 0.2f;
    [SerializeField] private float _groundCheckDistance = 0.7f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private int _health = 3;

    private Animator _animator;
    private bool _canAttack = true;
    private bool _canMove = true;
    private bool _isGrounded = false;
    private bool _resetJump = true;
    private CapsuleCollider2D _collider2D;
    private int _deathHash;
    private int _diamonds;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;
    private Vector3 _localArcPos;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _collider2D = GetComponent<CapsuleCollider2D>();
        _deathHash = Animator.StringToHash("Death");
        _playerSprite = GetComponentInChildren<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _swordArcSprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        _localArcPos = _swordArcSprite.transform.localPosition;
        Health = _health;
        
        _collider2D.enabled = true;
        IsAlive = true;
    }

    private void Update()
    {
        Movement();
        Attack();
        Debug.DrawRay(transform.position, Vector3.down * _groundCheckDistance, Color.green);
    }

    private void Movement()
    {
        // Make sure Attack Animation isn't running in order to move player
        _canMove = (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) ? true : false;
        
        if (_canMove && IsAlive)
        {
            float move = Input.GetAxisRaw("Horizontal");
            Jump();
            
            
            _rigidbody2D.velocity = new Vector2(move * _speed, _rigidbody2D.velocity.y);
            FlipSprite(move);
            
            OnPlayerMoved?.Invoke(move);
            
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

    public void Damage()
    {
        Debug.Log("Player was damaged");
        Health -= 1;
        Debug.Log($"CurrentHealth: {Health}");

        if (Health <= 0 && IsAlive)
        {
            _animator.SetTrigger(_deathHash);
            IsAlive = false;
            // _rigidbody2D.isKinematic = true;
            // _collider2D.enabled = false;
        }
    }

    IEnumerator JumpDelayRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        _resetJump = true;
    }

    private void OnEnable()
    {
        Diamond.OnDiamondCollected += CollectDiamonds;
        SpriteHelper.OnAttackEnded += CanMove;
    }

    private void OnDisable()
    {
        Diamond.OnDiamondCollected -= CollectDiamonds;
        SpriteHelper.OnAttackEnded -= CanMove;
    }

    private void CollectDiamonds(int value)
    {
        _diamonds += value;
        Debug.Log($"Diamonds: {_diamonds}");
    }
}
