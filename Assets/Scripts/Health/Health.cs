using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private float maxHealth;
    private float currentHealth;

    [SerializeField]
    private Image HealthBar;

    [SerializeField]
    private int hitDamage = 1;

    public Health(float maxHealth)
    {
        this.maxHealth = maxHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        //HealthBar = transform.Find("Fill").GetComponent<Image>();
        HealthBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTakeDamage(int damage)
    {
        currentHealth -= damage;
        //entity dies
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
        HealthBar.fillAmount = (currentHealth / maxHealth);
    }
}
