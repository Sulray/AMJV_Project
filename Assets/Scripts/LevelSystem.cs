using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    private int level = 1;
    private int experience = 0;

    [SerializeField]
    private int experienceToNextLevel = 5;
    [SerializeField]
    private int experienceMultiplier = 5;

    public void AddExeprience(int amount)
    {
        experience += amount;
        Debug.Log("current experience: " + experience);
        while (experience >= experienceToNextLevel)
        {
            experience -= experienceToNextLevel;
            NextLevel();
            Debug.Log("current experience: " + experience);
        }
    }

    private void NextLevel()
    {
        level++;
        Debug.Log("level up to" + level);
        experienceToNextLevel = experienceToNextLevel*experienceMultiplier;
        Debug.Log("experience to next level: " + experienceToNextLevel);
    }
}
