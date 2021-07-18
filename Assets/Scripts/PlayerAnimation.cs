using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private Animator _swordAnimator;
    private int _attackHash = Animator.StringToHash("Attack");
    private int _speedHash = Animator.StringToHash("Speed");
    private int _jumpHash = Animator.StringToHash("isJumping");

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _swordAnimator = transform.GetChild(1).GetComponent<Animator>();
    }

    private void Move(float speed)
    {
        _animator.SetFloat(_speedHash, Math.Abs(speed));
    }

    private void IsJumping(bool value)
    {
        _animator.SetBool(_jumpHash, value);
    }

    private void Attack()
    {
        _animator.SetTrigger(_attackHash);
        _swordAnimator.SetTrigger(_attackHash);
    }

    private void OnEnable()
    {
        Player.OnPlayerAttacked += Attack;
        Player.OnPlayerMoved += Move;
        Player.OnPlayerJumping += IsJumping;
    }

    private void OnDisable()
    {
        Player.OnPlayerAttacked -= Attack;
        Player.OnPlayerMoved -= Move;
        Player.OnPlayerJumping -= IsJumping;
    }
}
