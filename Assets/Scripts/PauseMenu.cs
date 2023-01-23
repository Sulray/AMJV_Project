using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject settings;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
