using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public enum SwordFX
{
    NONE = 0,
    BLUE = 3,
    RED = 2,
    ORANGE = 1
};

public class SpriteHelper : MonoBehaviour
{
    public static Action OnAttackEnded;
    public static Action OnAttackBegan;

    
    [SerializeField] private GameObject[] _swordFX;
    private GameObject _currentSpriteFX;

    public SwordFX _currentFX;

    private void Start()
    {
        _currentFX = SwordFX.ORANGE;
        _currentSpriteFX = _swordFX[0];
    }

    private void Update()
    {
        switch (_currentFX)
        {
            case SwordFX.NONE:
                _currentSpriteFX = null;
                break;
            case SwordFX.BLUE:
                _currentSpriteFX = _swordFX[3];
                break;
            case SwordFX.ORANGE:
                _currentSpriteFX = _swordFX[0];
                break;
            case SwordFX.RED:
                _currentSpriteFX = _swordFX[1];
                break;
            default:
                _currentSpriteFX = null;
                break;
        }
    }


    public void AttackEnded()
    {
        OnAttackEnded?.Invoke();
        foreach (var fx in _swordFX)
        {
            fx.SetActive(false);
        }
    }

    public void AttackBegan()
    {
        OnAttackBegan?.Invoke();
        // _currentSpriteFX.SetActive(true);
        // _currentSpriteFX.GetComponent<Animator>().SetTrigger("Attack");
    }
    
}
