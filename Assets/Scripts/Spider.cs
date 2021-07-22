using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class Spider : Enemy
{
    private Animator _animator;
    private bool _movingRight = true;
    private int _idleHash = Animator.StringToHash("Idle");
    private SpriteRenderer _sprite;
    
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _sprite = GetComponentInChildren<SpriteRenderer>();
    }

    public override void Update()
    {
        Movement(_movingRight);
    }

    private void Movement(bool moveRight)
    {
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("Spider_Idle"))
        {
            return;
        }
        _sprite.flipX = !moveRight;
        
        Transform target = moveRight ? end : start;

        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (transform.position == target.position)
        {
            _animator.SetTrigger(_idleHash);
        }

        if (transform.position == start.position)
        {
            _movingRight = true;
        }
        else if (transform.position == end.position)
        {
            _movingRight = false;
        }
    }
}
