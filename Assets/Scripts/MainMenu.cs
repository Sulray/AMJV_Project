using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenuContainer;

    [SerializeField]
    GameObject characterSelectionContainer;

    [SerializeField]
    GameObject settingsContainer;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuContainer.SetActive(true);
        characterSelectionContainer.SetActive(false);
        settingsContainer.SetActive(false);
    }

    public void PlayGame()
    {
        mainMenuContainer.SetActive(false);
        characterSelectionContainer.SetActive(true);
    }

    public void Settings()
    {
        mainMenuContainer.SetActive(false);
        settingsContainer.SetActive(true);
    }

    public void BackToMenu()
    {
        mainMenuContainer.SetActive(true);
        settingsContainer.SetActive(false);
        characterSelectionContainer.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        // SceneManager.LoadScene(0);
    }
}
