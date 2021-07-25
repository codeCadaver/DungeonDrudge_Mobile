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

    [SerializeField] private TMP_Text _gemCountText;
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
        _gemCountText.text = diamonds.ToString() + " G";
    }

    public void UpdateShopSelection(int yPosition)
    {
        Vector2 newPosition = _selectionImage.rectTransform.anchoredPosition;
        newPosition.y = yPosition;
        _selectionImage.rectTransform.anchoredPosition = newPosition;
    }
}
