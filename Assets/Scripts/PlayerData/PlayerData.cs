using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public bool hasKey;
    public bool hasBoots;
    public bool hasSword;
    public int diamonds;
    public int level;
    public int health;
    public float[] position;

    public PlayerData(Player player)
    {
        diamonds = player.Diamonds;
        level = 1;
        health = player.Health;
        position = new float[]
        {
            player.transform.position.x,
            player.transform.position.y,
            player.transform.position.z
        };

        hasKey = GameManager.Instance.HasKey;
        hasBoots = GameManager.Instance.HasBoots;
        hasSword = GameManager.Instance.HasSword;
    }
    
    public PlayerData()
    {
        diamonds = GameManager.Instance.Diamonds;
        level = 1;
        health = GameManager.Instance.Health;
        // position = new float[]
        // {
        //     player.transform.position.x,
        //     player.transform.position.y,
        //     player.transform.position.z
        // };

        hasKey = GameManager.Instance.HasKey;
        hasBoots = GameManager.Instance.HasBoots;
        hasSword = GameManager.Instance.HasSword;
    }
}
