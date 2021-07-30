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
    // private float move;
    private int _deathHash;
    private int _diamonds;
    private PlayerControls _controls;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _playerSprite;
    private SpriteRenderer _swordArcSprite;
    private Vector2 _inputMovement;
    private Vector3 _localArcPos;

    private void Awake()
    {
        // _controls = new PlayerControls();

        // _controls.Player.Move.performed += OnMovement;
        // _controls.Player.Move.canceled += OnStoppedMoving;
        // _controls.Player.Attack.performed += OnAttack;
        // _controls.Player.Jump.performed += OnJump;
    }

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
        Movement();
        Attack();
        IsGrounded();

        Debug.DrawRay(transform.position, Vector3.down * _groundCheckDistance, Color.green);
    }

    // public void OnStoppedMoving(InputAction.CallbackContext ctx)
    // {
    //     _rigidbody2D.velocity = Vector2.zero;
    //     OnPlayerMoved?.Invoke(_rigidbody2D.velocity.x);
    // }

    // public void OnMovement(InputAction.CallbackContext ctx)
    // {
    //     _canMove = (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) ? true : false;
    //
    //     _inputMovement = ctx.ReadValue<Vector2>();
    //     var move = _inputMovement.x;
    //     if (move != 0)
    //     {
    //         move = move > 0 ? 1 : -1;
    //     }
    //     if (_canMove && IsAlive)
    //     {
    //         Debug.Log($"inputMovement X: {move}");
    //         // Jump();
    //         
    //         
    //         _rigidbody2D.velocity = new Vector2(move * _speed, _rigidbody2D.velocity.y);
    //         FlipSprite(move);
    //         
    //         OnPlayerMoved?.Invoke(move);
    //     }
    // }


    private void Movement()
    {
        // Make sure Attack Animation isn't running in order to move player
        _canMove = (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) ? true : false;
        
        if (_canMove && IsAlive)
        {
            var gamepad = Gamepad.current;
            // float move = Input.GetAxisRaw("Horizontal");
            float move = gamepad.leftStick.ReadValue().x;
            Jump();
            
            
            _rigidbody2D.velocity = new Vector2(move * _speed, _rigidbody2D.velocity.y);
            FlipSprite(move);
            
            OnPlayerMoved?.Invoke(move);
        }
    }

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


    private void Attack()
    {
        // if (Input.GetMouseButtonDown(0))
        var gamepad = Gamepad.current;
        if (gamepad.buttonWest.wasPressedThisFrame)
            
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

    // public void CanMove()
    // {
    //     _canMove = true;
    // }

    // public void OnJump(InputAction.CallbackContext ctx)
    // {
    //     // IsGrounded();
    //     if (!IsGrounded())
    //     {
    //         return;
    //     }
    //     _resetJump = false;
    //     OnPlayerJumping?.Invoke(true);
    //     _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
    //     // delay routine
    //     StartCoroutine(JumpDelayRoutine());
    // }

    private void Jump()
    {
        IsGrounded();
        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            return;
        }
        // if (Input.GetKeyDown(KeyCode.Space))
        if (gamepad.buttonEast.wasPressedThisFrame)
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
        // _controls.Player.Enable();
        Diamond.OnDiamondCollected += CollectDiamonds;
        ShopKeeper.OnDiamondsRemoved += RemoveDiamonds;
        // SpriteHelper.OnAttackEnded += CanMove;
    }

    private void OnDisable()
    {
        // _controls.Player.Disable();
        Diamond.OnDiamondCollected -= CollectDiamonds;
        ShopKeeper.OnDiamondsRemoved -= RemoveDiamonds;
        // SpriteHelper.OnAttackEnded -= CanMove;
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
