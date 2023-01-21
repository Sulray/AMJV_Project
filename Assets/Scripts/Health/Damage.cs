using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public HealthBar healthBar;

    [SerializeField]
    private float maxHealth = 5f;
    [SerializeField]
    private int hitDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.maxHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // entity takes damage
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(hitDamage);
        }
        
        // entity dies
        if (healthBar.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void TakeDamage(int damage)
    {
        healthBar.currentHealth -= damage;
    }
}
