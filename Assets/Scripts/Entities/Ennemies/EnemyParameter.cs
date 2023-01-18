using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy/New Enemy", order = 1)]
public class EnemyParameter : ScriptableObject
{
    public int maxHealth;
    public int damage;
    public int speed;
    public int attackCD;
    public Movement movement;
    public Attack attacks;
    
}
