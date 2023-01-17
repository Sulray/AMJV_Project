using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    /*[SerializeField] protected float maxHealth;
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;*/
    [SerializeField] public EnityParameters entityData;
    [SerializeField] protected float health;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
