using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LevelSystem : MonoBehaviour
{
    //Initial level
    private int level = 1;
    private int experience = 0;

    //Experience to next level parameters
    private int currentFib = 5;
    private int previousFib = 3;

    //In game UI elements
    [SerializeField]
    private Image experienceBar;
    [SerializeField]
    private TMP_Text levelText;

    //Level up UI
    [SerializeField]
    GameObject levelUpPanel;
    
    [SerializeField]
    private TMP_Text proposition1Text;
    [SerializeField]
    private TMP_Text proposition2Text;
    [SerializeField]
    private TMP_Text proposition3Text;

    [SerializeField]
    private Image proposition1Icon;
    [SerializeField]
    private Image proposition2Icon;
    [SerializeField]
    private Image proposition3Icon;

    //Upgrade selection
    private int proposition1;
    private int proposition2;
    private int proposition3;
    private Boolean[] obtained = new Boolean[10];
    private string effect1;
    private string effect2;
    private string effect3;

    //Player data
    private GameObject player;
    private string playerName;
    private HeroController heroController;
    private Health health;
    [SerializeField] KnightAttack knightAttack;
    [SerializeField] MageAttack mageAttack;
    [SerializeField] HunterAttack hunterAttack;
    private int upgradesNumber;

    //Upgrades sprites
    [SerializeField] Sprite upgrade_0;
    [SerializeField] Sprite upgrade_1;
    [SerializeField] Sprite upgrade_2;
    [SerializeField] Sprite upgrade_3;
    [SerializeField] Sprite upgrade_4;
    [SerializeField] Sprite upgrade_5;
    [SerializeField] Sprite upgrade_6;
    [SerializeField] Sprite upgrade_7K;
    [SerializeField] Sprite upgrade_7H;
    [SerializeField] Sprite upgrade_7M;
    [SerializeField] Sprite upgrade_8K;
    [SerializeField] Sprite upgrade_8H;
    [SerializeField] Sprite upgrade_8M;
    [SerializeField] Sprite upgrade_9K;
    [SerializeField] Sprite upgrade_9H;
    [SerializeField] Sprite upgrade_9M;
    [SerializeField] Sprite upgrade_10K;
    [SerializeField] Sprite upgrade_10H;
    [SerializeField] Sprite upgrade_10M;


    // Start is called before the first frame update
    void Start()
    {
        experienceBar.fillAmount = 0;
        player = GameObject.FindWithTag("Player");
        playerName = player.name;
        if (player.name == "Knight Warrior") 
        { 
            knightAttack = player.GetComponent<KnightAttack>();
            upgradesNumber = 10;
        }
        if (player.name == "Mage Warrior") 
        { 
            mageAttack = player.GetComponent<MageAttack>();
            upgradesNumber = 6;
        }
        if (player.name == "Corssbow Warrior") 
        { 
            hunterAttack = player.GetComponent<HunterAttack>(); 
            upgradesNumber = 7;
        }
        heroController = player.GetComponent<HeroController>();
        health = player.GetComponent<Health>();

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
        if (level <= 11)
        {
            Time.timeScale = 0f;
            heroController.enabled = false;

            proposition1 = UnityEngine.Random.Range(0, upgradesNumber);
            while (obtained[proposition1]) { proposition1 = UnityEngine.Random.Range(0, upgradesNumber); }
            proposition2 = UnityEngine.Random.Range(0, upgradesNumber);
            while (obtained[proposition2]) { proposition2 = UnityEngine.Random.Range(0, upgradesNumber); }
            proposition3 = UnityEngine.Random.Range(0, upgradesNumber);
            while (obtained[proposition3]) { proposition3 = UnityEngine.Random.Range(0, upgradesNumber); }
            
            (proposition1Text.text, proposition1Icon.GetComponent<Image>().sprite, effect1)  = UpgradeUI(proposition1);
            (proposition2Text.text, proposition2Icon.GetComponent<Image>().sprite, effect2) = UpgradeUI(proposition2);
            (proposition3Text.text, proposition3Icon.GetComponent<Image>().sprite, effect3) = UpgradeUI(proposition3);

            levelUpPanel.SetActive(true);
        }
    }

    public void ChooseUpgrade1()
    {
        Invoke(effect1, 0);
        obtained[proposition1] = true;
        Debug.Log(obtained[proposition1]);
        Debug.Log("you chose upgrade 1");
        levelUpPanel.SetActive(false);
        heroController.enabled = true;
        Time.timeScale = 1f;
    }
    public void ChooseUpgrade2()
    {
        Invoke(effect2, 0);
        obtained[proposition2] = true;
        Debug.Log(obtained[proposition2]);
        Debug.Log("you chose upgrade 2");
        levelUpPanel.SetActive(false);
        heroController.enabled = true;
        Time.timeScale = 1f;
    }
    public void ChooseUpgrade3()
    {
        Invoke(effect3, 0);
        obtained[proposition3] = true;
        Debug.Log(obtained[proposition3]);
        Debug.Log("you chose upgrade 3");
        levelUpPanel.SetActive(false);
        heroController.enabled=true;
        Time.timeScale = 1f;
    }
    
    //Upgrades description
    private (string, Sprite, string) UpgradeUI(int i)
    {
        switch (i + 1)
        {
            case 1: //Cooldown1
                return ("Left click cooldown -", upgrade_1, "Effect1");
            case 2: //Cooldown2
                return ("Right click cooldown -", upgrade_2, "Effect2");
            case 3: //Cooldown3
                return ("Spacebar cooldown -", upgrade_3, "Effect3");
            case 4: //Speed
                return ("Speed +50%", upgrade_4, "Effect4");
            case 5: //Health
                return ("Max health +20%", upgrade_5, "Effect5");
            case 6:
                switch (playerName)
                {
                    case "Knight Warrior":
                        return ("Left click attack x2", upgrade_6, "Effect6K");
                    case "Corssbow Warrior":
                        return ("Left click attack x2", upgrade_6, "Effect6H");
                    case "Mage Warrior":
                        return ("Left click attack x2", upgrade_6, "Effect6M");
                    default:
                        return ("Oops", upgrade_0, "Effect0");
                }
            case 7:
                switch (playerName)
                {
                    case "Knight Warrior":
                        return ("Right click attack x2", upgrade_7K, "Effect7K");
                    case "Corssbow Warrior":
                        return ("Spacebar speed x2", upgrade_7H, "Effect7H");
                    case "Mage Warrior":
                        return ("Description", upgrade_7M, "Effect7M");
                    default:
                        return ("Oops", upgrade_0, "Effect0");
                }
            case 8:
                switch (playerName)
                {
                    case "Knight Warrior":
                        return ("Spacebar attack x2", upgrade_8K, "Effect8K");
                    case "Corssbow Warrior":
                        return ("Description", upgrade_8H, "Effect8H");
                    case "Mage Warrior":
                        return ("Description", upgrade_8M, "Effect8M");
                    default:
                        return ("Oops", upgrade_0, "Effect0");
                }
            case 9:
                switch (playerName)
                {
                    case "Knight Warrior":
                        return ("Right click range +", upgrade_9K, "Effect9K");
                    case "Corssbow Warrior":
                        return ("Description", upgrade_9H, "Effect9H");
                    case "Mage Warrior":
                        return ("Description", upgrade_9M, "Effect9M");
                    default:
                        return ("Oops", upgrade_0, "Effect0");
                }
            case 10:
                switch (playerName)
                {
                    case "Knight Warrior":
                        return ("Spacebar range +", upgrade_10K, "Effect10K");
                    case "Corssbow Warrior":
                        return ("Description", upgrade_10H, "Effect10H");
                    case "Mage Warrior":
                        return ("Description", upgrade_10M, "Effect10M");
                    default:
                        return ("Oops", upgrade_0, "Effect0");
                }
            default:
                return ("Oops", upgrade_0, "Effect0");
        }
    }

    //Upgrades
    private void Effect1()
    {
        heroController.cooldown1 = (int)heroController.cooldown1 / 2;
    }
    private void Effect2()
    {
        heroController.cooldown2 = (int)heroController.cooldown2 / 2;
    }
    private void Effect3()
    {
        heroController.cooldown3 = (int)heroController.cooldown3 / 2;
    }
    private void Effect4()
    {
        heroController.speed *= 1.5f;
    }
    private void Effect5()
    {
        health.MaxHealth *= 1.2f;
        health.currentHealth = health.MaxHealth;
    }
    private void Effect6K()
    {
        knightAttack.attack1Damage *= 2;
    }
    private void Effect6H()
    {
        hunterAttack.attack1Damage *= 2;
    }
    private void Effect6M()
    {
        mageAttack.attack1Damage *= 2;
    }
    private void Effect7K()
    {
        knightAttack.attack2Damage *= 2;
    }
    private void Effect7H()
    {
        hunterAttack.attack3Speed *= 2;
    }
    private void Effect7M()
    {

    }
    private void Effect8K()
    {
        knightAttack.attack3Damage *= 2;
    }
    private void Effect8H()
    {

    }
    private void Effect8M()
    {

    }
    private void Effect9K()
    {
        knightAttack.attack2Range *= 2;
    }
    private void Effect9H()
    {

    }
    private void Effect9M()
    {

    }
    private void Effect10K()
    {
        knightAttack.attack3Range *= 2;
    }
    private void Effect10H()
    {

    }
    private void Effect10M()
    {

    }
}
