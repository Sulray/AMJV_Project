using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    private int level = 0;
    private int experience = 0;

    [SerializeField]
    private int experienceToNextLevel;

    public void AddExeprience(int amount)
    {
        experience += amount;
        Debug.Log(experience);
        if (experience >= experienceToNextLevel)
        {
            experience -= experienceToNextLevel;
            NextLevel();
        }
    }

    private void NextLevel()
    {
        level++;
        Debug.Log("level up to" + level);
        experienceToNextLevel = experienceToNextLevel*experienceToNextLevel;
        Debug.Log("experience to next level: " + experienceToNextLevel);
    }
}
