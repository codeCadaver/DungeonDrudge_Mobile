using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;
    
    [field: SerializeField]
    public bool HasKey { get; set; }
    [field: SerializeField]
    public bool HasBoots { get; set; }
    [field: SerializeField]
    public bool HasSword { get; set; }
    
    [SerializeField] private GameObject _pausePanel;


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
    
    public void OpenPausePanel()
    {
        Time.timeScale = 0;
        _pausePanel.SetActive(true);
    }

    public void ClosePausePanel()
    {
        Time.timeScale = 1;
        _pausePanel.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
