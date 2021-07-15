using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _animator;
    private int _speedHash = Animator.StringToHash("Speed");

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void Move(float speed)
    {
        _animator.SetFloat(_speedHash, Math.Abs(speed));
    }
}
