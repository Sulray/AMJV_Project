using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Entity", menuName = "Entity/New Entity", order = 1)]
public class EntityParameter : ScriptableObject
{   
    public int maxHealth;
    public int damage;
    public int speed;
    public MonoBehaviour strategy;
}
