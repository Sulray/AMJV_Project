using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    UnityEngine.GameObject mainMenuContainer;

    [SerializeField]
    UnityEngine.GameObject characterSelectionContainer;

    [SerializeField]
    UnityEngine.GameObject settingsContainer;

    [SerializeField]
    TMP_Dropdown resolutionDropdown;

    [SerializeField]
    AudioMixer audioMixer;

    Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuContainer.SetActive(true);
        characterSelectionContainer.SetActive(false);
        settingsContainer.SetActive(false);

        // Resolution settings set up
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
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

    // Setting functions
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void SetMusicVolume(float musicVolume)
    {
        audioMixer.SetFloat("musicVolume", musicVolume);
    }

    public void SetEffectsVolume(float effectsVolume)
    {
        audioMixer.SetFloat("effectsVolume", effectsVolume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
