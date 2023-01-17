using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity", menuName = "Entity/New Entity", order = 1)]
public class EnityParameters : ScriptableObject
{
    public int maxHealth;
    public int speed;
    public int damage;
}
