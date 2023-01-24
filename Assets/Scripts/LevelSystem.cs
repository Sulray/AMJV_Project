using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    private int level = 1;
    private int experience = 0;

    [SerializeField]
    int experienceToNextLevel = 5;
    [SerializeField]
    int experienceMultiplier = 5;
    [SerializeField]
    private Image experienceBar;
    [SerializeField]
    private TMP_Text levelText;

    [SerializeField]
    UnityEngine.GameObject levelUpPanel;
    [SerializeField]
    private TMP_Text Upgrade1Text;
    [SerializeField]
    private TMP_Text Upgrade2Text;
    [SerializeField]
    private TMP_Text Upgrade3Text;

    private HeroController heroController;
    private Health health;

    private Boolean upgrade1Available = true;
    private Boolean upgrade2Available = true;
    private Boolean upgrade3Available = true;
    private Boolean upgrade4Available = true;
    private Boolean upgrade5Available = true;
    private Boolean upgrade6Available = true;
    private Boolean upgrade7Available = true;
    private Boolean upgrade8Available = true;
    private Boolean upgrade9Available = true;
    private Boolean upgrade10Available = true;


    // Start is called before the first frame update
    void Start()
    {
        experienceBar.fillAmount = 0;
        heroController = GameObject.FindWithTag("Player").GetComponent<HeroController>();
    }

    public void AddExeprience(int amount)
    {
        experience += amount;
        experienceBar.fillAmount = (float)experience/(float)experienceToNextLevel;
        while (experience >= experienceToNextLevel)
        {
            experience -= experienceToNextLevel;
            NextLevel();
            experienceBar.fillAmount = (float)experience / (float)experienceToNextLevel;
        }
    }

    private void NextLevel()
    {
        level++;
        levelText.text = "LV. " + level.ToString();
        experienceToNextLevel = experienceToNextLevel*experienceMultiplier;
        Time.timeScale = 0f;
        heroController.enabled = false;
        levelUpPanel.SetActive(true);
    }

    public void ChooseUpgrade1()
    {
        Upgrade(UnityEngine.Random.Range(1,11));
        Debug.Log("you chose upgrade 1");
        levelUpPanel.SetActive(false);
        heroController.enabled = true;
        Time.timeScale = 1f;
    }
    public void ChooseUpgrade2()
    {
        Upgrade(UnityEngine.Random.Range(1, 11));
        Debug.Log("you chose upgrade 2");
        levelUpPanel.SetActive(false);
        heroController.enabled = true;
        Time.timeScale = 1f;
    }
    public void ChooseUpgrade3()
    {
        Upgrade(UnityEngine.Random.Range(1, 11));
        Debug.Log("you chose upgrade 3");
        levelUpPanel.SetActive(false);
        heroController.enabled=true;
        Time.timeScale = 1f;
    }

    //Upgrades
    private void Upgrade(int i)
    {
        switch (i)
        {
            case 1: //Cooldown1
                heroController.cooldown1 = (int)heroController.cooldown1 / 2;
                upgrade1Available = false;
                break;
            case 2: //Cooldown2
                heroController.cooldown2 = (int)heroController.cooldown2 / 2;
                upgrade2Available = false;
                break;
            case 3: //Cooldown3
                heroController.cooldown3 = (int)heroController.cooldown3 / 2;
                upgrade3Available = false;
                break;
            case 4: //Speed
                heroController.speed *= 1.5f;
                upgrade4Available = false;
                break;
            case 5: //Health
                health.MaxHealth *= 1.2f;
                upgrade5Available = false;
                break;
        }
    }
}
