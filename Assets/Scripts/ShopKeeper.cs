using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField] private GameObject _shopkeeperPanel;
    [SerializeField] private int[] _selectionOffsets = {69, 37, -146};
    private int _diamonds;

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
    }

    private void OnEnable()
    {
        Player.OnDiamondsCollected += GetDiamondCount;
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
                UIManager.Instance.UpdateShopSelection(_selectionOffsets[0]);
                break;
            case 1:
                // middle button
                UIManager.Instance.UpdateShopSelection(_selectionOffsets[1]);
                break;
            case 2:
                // bottom button
                UIManager.Instance.UpdateShopSelection(_selectionOffsets[2]);
                break;
            default:
                // top button
                UIManager.Instance.UpdateShopSelection(_selectionOffsets[0]);
                break;
        }
    }
}
