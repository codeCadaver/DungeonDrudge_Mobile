using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadData()
    {
        Player player = new Player();
        player.LoadPlayer();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
