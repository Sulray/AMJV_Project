using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{

    [Header("Entity Parameters")]
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float health;
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
