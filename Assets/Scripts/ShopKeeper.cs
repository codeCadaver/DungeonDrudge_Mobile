using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public static Action<int> OnDiamondsRemoved;
    
    [SerializeField] private GameObject _shopkeeperPanel;
    [SerializeField] private int[] _selectionOffsets = {69, 37, -146};

    [SerializeField] private int[] _selectedValues = {200, 500, 100};
    private int _currentDiamonds;
    private int _selectedItem;
    private int _selectedItemValue;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _shopkeeperPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _shopkeeperPanel.SetActive(false);
        }
    }

    private void GetDiamondCount(int diamonds)
    {
        UIManager.Instance.OpenShop(diamonds);
        _currentDiamonds = diamonds;
    }

    private void OnEnable()
    {
        Player.OnDiamondsCollected += GetDiamondCount;
        
        // Hide Selection Image
        SelectItem(99);
    }

    private void OnDisable()
    {
        Player.OnDiamondsCollected -= GetDiamondCount;
        
    }

    public void SelectItem(int item)
    {
        // 0 = Top Button
        // 1 = Middle Button
        // 2 = Bottom Button
        
        switch (item)
        {
            case 0:
                // top button
                UIManager.Instance.UpdateShopSelection(_selectionOffsets[0], true);
                _selectedItem = 0;
                _selectedItemValue = _selectedValues[0];
                break;
            case 1:
                // middle button
                UIManager.Instance.UpdateShopSelection(_selectionOffsets[1], true);
                _selectedItem = 1;
                _selectedItemValue = _selectedValues[1];
                break;
            case 2:
                // bottom button
                UIManager.Instance.UpdateShopSelection(_selectionOffsets[2], true);
                _selectedItem = 2;
                _selectedItemValue = _selectedValues[2];
                break;
            default:
                // top button
                UIManager.Instance.UpdateShopSelection(_selectionOffsets[0], false);
                _selectedItem = 0;
                _selectedItemValue = _selectedValues[0];
                break;
        }
    }

    public void BuyItem()
    {
        if (_currentDiamonds >= _selectedItemValue)
        {
            Debug.Log("You can purchase this");
            Debug.Log($"You have: {_currentDiamonds}, You need: {_selectedItemValue}");
            OnDiamondsRemoved?.Invoke(_selectedItemValue);
        }
        else
        {
            Debug.Log("You broke, mother-father!");
            Debug.Log($"You have: {_currentDiamonds}, You need: {_selectedItemValue}");
        }
    }
}
