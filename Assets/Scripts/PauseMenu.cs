using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
public class PauseMenu : MonoBehaviour
{
    
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settings;
    private GameObject player;
    public UnityAction gameOver;
    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.FindWithTag("Player");
        //player.GetComponent<Health>().playerDeath.AddListener(gameOver);
        gameOver += GameOver;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        Time.timeScale = 1 - Time.timeScale;
        //if playing -> pause | if pause -> resume | if settings -> resume
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy && !settings.activeInHierarchy);
        settings.SetActive(false);
        
    }

    public void Settings()
    {
        pauseMenu.SetActive(false);
        settings.SetActive(true);
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void BackToPause()
    {
        settings.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    
}
