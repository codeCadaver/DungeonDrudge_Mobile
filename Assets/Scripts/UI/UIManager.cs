using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;

    [SerializeField] private TMP_Text _gemCount_Shop_Text, _gemCount_HUD_Text;
    [SerializeField] private Image _selectionImage;
    [SerializeField] private Image[] _healthBars;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        foreach (var bar in _healthBars)
        {
            bar.enabled = true;
        }
    }

    public void OpenShop(int diamonds)
    {
        _gemCount_Shop_Text.text = diamonds.ToString() + " G";
    }

    public void UpdateShopSelection(int yPosition, bool visible)
    {
        if (visible)
        {
            _selectionImage.enabled = true;
            Vector2 newPosition = _selectionImage.rectTransform.anchoredPosition;
            newPosition.y = yPosition;
            _selectionImage.rectTransform.anchoredPosition = newPosition;
        }
        else
        {
            _selectionImage.enabled = false;
        }
    }

    void UpdateGemCountHUD(int diamonds)
    {
        _gemCount_HUD_Text.text = diamonds.ToString();
    }

    private void OnEnable()
    {
        Player.OnDiamondsCollected += UpdateGemCountHUD;
        Player.OnPlayerHit += RemoveHealthBar;
    }

    private void OnDisable()
    {
        Player.OnDiamondsCollected -= UpdateGemCountHUD;
        Player.OnPlayerHit -= RemoveHealthBar;
    }

    private void RemoveHealthBar(int healthUnits)
    {
        int currentUnits = 0;
        {
            for (int i = _healthBars.Length - 1; i >= 0; i--)
            {
                if (_healthBars[i].isActiveAndEnabled)
                {
                    _healthBars[i].enabled = false;
                    currentUnits += 1;
                    if (currentUnits >= healthUnits)
                    {
                        break;
                    }
                }
            }
        }
    }
}
