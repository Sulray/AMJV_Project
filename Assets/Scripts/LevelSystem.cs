using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public int level = 1;
    public int experience = 0;

    [SerializeField]
    int experienceToNextLevel = 5;
    [SerializeField]
    int experienceMultiplier = 5;
    [SerializeField]
    private Image experienceBar;
    [SerializeField]
    private TMP_Text levelText;

    // Start is called before the first frame update
    void Start()
    {
        experienceBar.fillAmount = 0;
    }

    public void AddExeprience(int amount)
    {
        experience += amount;
        experienceBar.fillAmount = (float)experience/(float)experienceToNextLevel;
        Debug.Log("current experience: " + experience);
        while (experience >= experienceToNextLevel)
        {
            experience -= experienceToNextLevel;
            NextLevel();
            experienceBar.fillAmount = (float)experience / (float)experienceToNextLevel;
            Debug.Log("current experience: " + experience);
        }
    }

    private void NextLevel()
    {
        level++;
        levelText.text = "LV. " + level.ToString();
        Debug.Log("level up to" + level);
        experienceToNextLevel = experienceToNextLevel*experienceMultiplier;
        Debug.Log("experience to next level: " + experienceToNextLevel);
    }
}
