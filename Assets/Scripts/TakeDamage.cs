using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    public HealthBar healthBar;

    [SerializeField]
    private float maxHealth = 5f;

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
            Damage(3);
        }
        
        // entity dies
        if (healthBar.currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
    void Damage(int damage)
    {
        healthBar.currentHealth -= damage;
    }
}
