using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Player", menuName = "Player/New Player", order = 1)]
public class PlayerParameter : ScriptableObject
{
    public int startingHealth;
    
    public int startingDamage1;
    public int startingDamage2;
    public int startingDamage3;

    public int startingSpeed;

    public int startingCD1;
    public int startingCD2;
    public int startingCD3;



}
