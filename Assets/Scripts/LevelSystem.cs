using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LevelSystem : MonoBehaviour
{
    private int level = 1;
    private int experience = 0;

    private int currentFib = 5;
    private int previousFib = 3;

    [SerializeField]
    private UnityEngine.UI.Image experienceBar;
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

    [SerializeField]
    private UnityEngine.UI.Image Upgrade1Icon;
    [SerializeField]
    private UnityEngine.UI.Image Upgrade2Icon;
    [SerializeField]
    private UnityEngine.UI.Image Upgrade3Icon;

    private int upgrade1;
    private int upgrade2;
    private int upgrade3;

    private GameObject player;
    private HeroController heroController;
    private Health health;

    private Boolean[] obtained = new Boolean[10];

    [SerializeField] Sprite upgrade_0;
    [SerializeField] Sprite upgrade_1;
    [SerializeField] Sprite upgrade_2;
    [SerializeField] Sprite upgrade_3;
    [SerializeField] Sprite upgrade_4;
    [SerializeField] Sprite upgrade_5;
    [SerializeField] Sprite upgrade_6;
    [SerializeField] Sprite upgrade_7;
    [SerializeField] Sprite upgrade_8;
    [SerializeField] Sprite upgrade_9;

    private Sprite[] upgradeSprites = new Sprite[10];



    // Start is called before the first frame update
    void Start()
    {
        experienceBar.fillAmount = 0;
        player = GameObject.FindWithTag("Player");
        heroController = player.GetComponent<HeroController>();
        health = player.GetComponent<Health>();

        upgradeSprites[0] = upgrade_0;
        upgradeSprites[1] = upgrade_1;
        upgradeSprites[2] = upgrade_2;
        upgradeSprites[3] = upgrade_3;
        upgradeSprites[4] = upgrade_4;
        upgradeSprites[5] = upgrade_5;
        upgradeSprites[6] = upgrade_6;
        upgradeSprites[7] = upgrade_7;
        upgradeSprites[8] = upgrade_8;
        upgradeSprites[9] = upgrade_9;

        for (int i = 0; i < obtained.Length; i++)
        {
            obtained[i] = false;
        }
    }

    public void AddExeprience(int amount)
    {
        experience += amount;
        experienceBar.fillAmount = (float)experience/(float)currentFib;
        while (experience >= currentFib)
        {
            experience -= currentFib;
            NextLevel();
            experienceBar.fillAmount = (float)experience / (float)currentFib;
        }
    }

    private void NextLevel()
    {
        level++;
        levelText.text = "LV. " + level.ToString();
        int tmp = currentFib;
        currentFib += previousFib;
        previousFib = tmp;
        if (level <= 6)
        {
            Time.timeScale = 0f;
            heroController.enabled = false;
            upgrade1 = UnityEngine.Random.Range(0, 5);
            while (obtained[upgrade1]) { upgrade1 = UnityEngine.Random.Range(0, 5); }
            upgrade2 = UnityEngine.Random.Range(0, 5);
            while (obtained[upgrade1]) { upgrade1 = UnityEngine.Random.Range(0, 5); }
            upgrade3 = UnityEngine.Random.Range(0, 5);
            while (obtained[upgrade1]) { upgrade1 = UnityEngine.Random.Range(0, 5); }
            Upgrade1Text.text = UpgradeDescription(upgrade1);
            Upgrade2Text.text = UpgradeDescription(upgrade2);
            Upgrade3Text.text = UpgradeDescription(upgrade3);
            Upgrade1Icon.GetComponent<UnityEngine.UI.Image>().sprite = upgradeSprites[upgrade1];
            Upgrade2Icon.GetComponent<UnityEngine.UI.Image>().sprite = upgradeSprites[upgrade2];
            Upgrade3Icon.GetComponent<UnityEngine.UI.Image>().sprite = upgradeSprites[upgrade3];
            levelUpPanel.SetActive(true);
        }
    }

    public void ChooseUpgrade1()
    {
        Upgrade(upgrade1);
        Debug.Log("you chose upgrade 1");
        levelUpPanel.SetActive(false);
        heroController.enabled = true;
        Time.timeScale = 1f;
    }
    public void ChooseUpgrade2()
    {
        Upgrade(upgrade2);
        Debug.Log("you chose upgrade 2");
        levelUpPanel.SetActive(false);
        heroController.enabled = true;
        Time.timeScale = 1f;
    }
    public void ChooseUpgrade3()
    {
        Upgrade(upgrade3);
        Debug.Log("you chose upgrade 3");
        levelUpPanel.SetActive(false);
        heroController.enabled=true;
        Time.timeScale = 1f;
    }

    //Upgrades
    private void Upgrade(int i)
    {
        obtained[i] = true;
        switch (i)
        {
            case 0: //Cooldown1
                heroController.cooldown1 = (int)heroController.cooldown1 / 2;
                break;
            case 1: //Cooldown2
                heroController.cooldown2 = (int)heroController.cooldown2 / 2;
                break;
            case 2: //Cooldown3
                heroController.cooldown3 = (int)heroController.cooldown3 / 2;
                break;
            case 3: //Speed
                heroController.speed *= 1.5f;
                break;
            case 4: //Health
                health.MaxHealth *= 1.2f;
                health.currentHealth = health.MaxHealth;
                break;
            default:
                Debug.Log("could not find an upgrade");
                break;
        }
    }

    //Upgrades description
    private string UpgradeDescription(int i)
    {
        switch (i)
        {
            case 0: //Cooldown1
                return "Left click cooldown -";
            case 1: //Cooldown2
                return "Right click cooldown -";
            case 2: //Cooldown3
                return "Spacebar cooldown -";
            case 3: //Speed
                return "Speed +50%";
            case 4: //Health
                return "Max health +20%";
            default:
                return "you suck";
        }
    }

    private string UpgradeIcon(int i)
    {
        switch (i)
        {
            case 0: //Cooldown1
                return "Cooldown1Upgrade";
            case 1: //Cooldown2
                return "Cooldown2Upgrade";
            case 2: //Cooldown3
                return "Cooldown3Upgrade";
            case 3: //Speed
                return "Speed x2";
            case 4: //Health
                return "Max health +20%";
            default:
                return "you suck";
        }
    }
}
