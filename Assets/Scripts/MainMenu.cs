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

    [SerializeField]
    TMP_Text trueTitle;
    [SerializeField]
    TMP_Text title;

    Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
        mainMenuContainer.SetActive(true);
        characterSelectionContainer.SetActive(false);
        settingsContainer.SetActive(false);
        trueTitle.enabled = false;

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
        int arena = Random.Range(1, 3);
        Debug.Log(arena);
        SceneManager.LoadScene("LV" + arena.ToString());
    }

    // Setting functions
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }
    public void SetMusicVolume(float musicVolume)
    {
        audioMixer.SetFloat("musicVolume", Mathf.Log10(musicVolume) * 20);
    }

    public void SetEffectsVolume(float effectsVolume)
    {
        audioMixer.SetFloat("effectsVolume", Mathf.Log10(effectsVolume) * 20);
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

    public void EasterEggTitle()
    {
        title.enabled = false;
        trueTitle.enabled = true;
    }
}
