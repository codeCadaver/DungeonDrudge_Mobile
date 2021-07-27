using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamageable
{
    public static Action OnPlayerAttacked;  // Player attacked something
    public static Action OnPlayerDied;
    public static Action OnPlayerHit;       // Player was hit
    public static Action<float> OnPlayerMoved;
    public static Action<int> OnDiamondsCollected;
    public static Action<bool> OnPlayerJumping;

    public bool IsAlive { get; set; }
    public int Health { get; set; }

    [SerializeField] private float _attackDelay = 0.2f;
    [SerializeField] private float _groundCheckDistance = 0.7f;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private int _health = 4;

    private Animator _animator;
    private bool _canAttack = true;
    private bool _canMove = true;
    private bool _isGrounded = false;
    private bool _resetJump = true;
    private CapsuleCollider2D _collider2D;
    private float move;
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

        _canMove = true;
        _collider2D.enabled = true;
        IsAlive = true;
    }

    private void Update()
    {
        // Movement();
        // Attack();
        IsGrounded();
        Debug.DrawRay(transform.position, Vector3.down * _groundCheckDistance, Color.green);
    }

    public void OnMovement(InputAction.CallbackContext ctx)
    {
        Vector2 inputMovement = ctx.ReadValue<Vector2>().normalized;
        // rawInputMovement = new Vector3(inputMovement.x, inputMovement.y, 0);
        // Make sure Attack Animation isn't running in order to move player
        _canMove = (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) ? true : false;
        
        if (_canMove && IsAlive)
        {
            move = inputMovement.x;
            // Jump();
            
            
            _rigidbody2D.velocity = new Vector2(move * _speed, _rigidbody2D.velocity.y);
            FlipSprite(move);
            
            OnPlayerMoved?.Invoke(move);
            
        }
    }

    // private void Movement()
    // {
    //     // Make sure Attack Animation isn't running in order to move player
    //     _canMove = (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) ? true : false;
    //     
    //     if (_canMove && IsAlive)
    //     {
    //         float move = Input.GetAxisRaw("Horizontal");
    //         Jump();
    //         
    //         
    //         _rigidbody2D.velocity = new Vector2(move * _speed, _rigidbody2D.velocity.y);
    //         FlipSprite(move);
    //         
    //         OnPlayerMoved?.Invoke(move);
    //         
    //     }
    // }

    public void OnAttack(InputAction.CallbackContext ctx)
    {
        if (IsGrounded())
        {
            if (_canAttack)
            {
                StartCoroutine(AttackRoutine());
            }
        }
    }

    // private void Attack()
    // {
    //     // if (Input.GetMouseButtonDown(0))
    //     {
    //         if (IsGrounded())
    //         {
    //             if (_canAttack)
    //             {
    //                 StartCoroutine(AttackRoutine());
    //             }
    //             // _canMove = false;
    //             // _rigidbody2D.velocity = new Vector2(0, _rigidbody2D.velocity.y);
    //             // OnPlayerAttacked?.Invoke();
    //             // _playerAnimation.Attack();
    //         }
    //     }
    // }

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

    public void OnJump(InputAction.CallbackContext ctx)
    {
        Debug.Log("Player::OnJump() was called");
        // IsGrounded();
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
        _rigidbody2D.velocity = new Vector2(move * _speed, _rigidbody2D.velocity.y);
    }

    // private void Jump()
    // {
    //     IsGrounded();
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         if (!IsGrounded())
    //         {
    //             return;
    //         }
    //         _resetJump = false;
    //         // _playerAnimation.IsJumping(true);
    //         OnPlayerJumping?.Invoke(true);
    //         _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
    //         // delay routine
    //         StartCoroutine(JumpDelayRoutine());
    //     }
    // }

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
        Health -= 1;
        OnPlayerHit?.Invoke();

        if (Health <= 0 && IsAlive)
        {
            _animator.SetTrigger(_deathHash);
            IsAlive = false;
            OnPlayerDied?.Invoke();
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
        ShopKeeper.OnDiamondsRemoved += RemoveDiamonds;
        SpriteHelper.OnAttackEnded += CanMove;
    }

    private void OnDisable()
    {
        Diamond.OnDiamondCollected -= CollectDiamonds;
        ShopKeeper.OnDiamondsRemoved -= RemoveDiamonds;
        SpriteHelper.OnAttackEnded -= CanMove;
    }

    private void CollectDiamonds(int value)
    {
        _diamonds += value;
        OnDiamondsCollected?.Invoke(_diamonds);
    }

    private void RemoveDiamonds(int value)
    {
        _diamonds -= value;
        OnDiamondsCollected?.Invoke(_diamonds);
    }
}
