using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    public static Action<int> OnDiamondsRemoved;
    public static Action OnHasFlameSword;
    public static Action OnHasKey;
    
    [SerializeField] private GameObject _shopkeeperPanel;
    [SerializeField] private int[] _selectionOffsets = {69, 37, -146};

    [SerializeField] private int[] _selectedValues = {200, 500, 100};
    private int _currentDiamonds;
    private int _selectedItem;
    private int _selectedItemValue;

    private void Start()
    {
        _currentDiamonds = GameManager.Instance.Diamonds;
    }


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
            // default:
            //     // top button
            //     UIManager.Instance.UpdateShopSelection(_selectionOffsets[0], false);
            //     _selectedItem = 0;
            //     _selectedItemValue = _selectedValues[0];
            //     break;
        }
    }

    public void BuyItem()
    {
        if (_currentDiamonds >= _selectedItemValue)
        {
            switch (_selectedItem)
            {
                case 0:
                    GameManager.Instance.HasSword = true;
                    // TODO: Send Has FlameEvent
                    OnHasFlameSword?.Invoke();
                    break;
                case 1:
                    GameManager.Instance.HasBoots = true;
                    //TODO: Send Double Jump Event
                    
                    break;
                case 2:
                    GameManager.Instance.HasKey = true;
                    OnHasKey?.Invoke();
                    break;
                default:
                    Debug.Log($"ShopKeeper::BuyItem()::Selected Item NOT FOUND: {_selectedItem}");
                    break;
                
            }
            Debug.Log("You can purchase this");
            Debug.Log($"You have: {_currentDiamonds}, You need: {_selectedItemValue}");
            OnDiamondsRemoved?.Invoke(_selectedItemValue);
            // Player player = new Player();
            GameManager.Instance.SavePlayer();
        }
        else
        {
            //TODO: Animate current amount of diamonds to show player doesn't have enough
            Debug.Log("You broke, mother-father!");
            Debug.Log($"You have: {_currentDiamonds}, You need: {_selectedItemValue}");
        }
    }

    public void Cancel()
    {
        _shopkeeperPanel.SetActive(false);
    }
}
