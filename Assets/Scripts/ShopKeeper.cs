using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopKeeper : MonoBehaviour
{
    [SerializeField] private GameObject _shopkeeperPanel;
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

    public void SelectItem()
    {
        Debug.Log("Button Pressed");
    }
}
