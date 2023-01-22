using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float MaxHealth {get; set;}
    private float currentHealth;

    [SerializeField]
    private Image HealthBar;

    [SerializeField]
    private int hitDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        //HealthBar = transform.Find("Fill").GetComponent<Image>();
        //HealthBar.fillAmount = 1;
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
        HealthBar.fillAmount = (currentHealth / MaxHealth);
    }
}
