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
    }

    private void OnDisable()
    {
        Player.OnDiamondsCollected -= UpdateGemCountHUD;
    }
}
