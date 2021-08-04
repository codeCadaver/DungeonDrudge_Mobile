using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance => _instance;

    public static Action OnPlayerSaved;
    
    [field: SerializeField]
    public bool HasKey { get; set; }
    [field: SerializeField]
    public bool HasBoots { get; set; }
    [field: SerializeField]
    public bool HasSword { get; set; }

    [SerializeField] private float _newGameDelay = 2f;
    [SerializeField] private GameObject _pausePanel;

    private Player _player;


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
        // _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void OpenPausePanel()
    {
        // OnPlayerSaved?.Invoke();
        Time.timeScale = 0;
        _pausePanel.SetActive(true);
        _player = GameObject.FindObjectOfType<Player>();
        if (_player == null)
        {
            _player = new Player();
        }
        SavePlayer();
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

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SavePlayer()
    {
        Debug.Log("System saved");
        if (_player == null)
        {
            return;
        }
        SaveSystem.SavePlayer(_player);
    }

    public void LoadPlayer()
    {
        PlayerData data = SaveSystem.LoadPlayer();

        if (_player == null)
        {
            return;
        }
        _player.Diamonds = data.diamonds;
        _player.Health = data.health;

        Vector3 position = new Vector3();
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        transform.position = position;

        HasBoots = data.hasBoots;
        HasKey = data.hasKey;
        HasSword = data.hasSword;
        
        SceneManager.LoadScene(data.level);
    }
}
