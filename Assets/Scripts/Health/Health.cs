using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    //These aren't working
    /*
    public UnityEvent<Enemy> enemyDeath;
    public UnityEvent playerDeath;*/
    public float MaxHealth {get; set;}
    public float currentHealth;

    [SerializeField]
    private Image HealthBar;

    [SerializeField]
    private int hitDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        HealthBar.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("damage "+damage);
        Debug.Log("health " + currentHealth);
        //entity dies
        if (currentHealth <= 0)
        {
            if (gameObject.CompareTag("Player")){
                Debug.Log("You died");
                GameObject.FindWithTag("SceneManager").GetComponent<PauseMenu>().GameOver();
                //playerDeath?.Invoke();
            }
            else
            {
                GetComponent<Enemy>().Despawn();
                GameObject.Instantiate(GetComponent<Enemy>().xp, transform);
                //GetComponent<ExperiencePool>().OnSpawnExp(transform.position);
                //enemyDeath?.Invoke(this.gameObject.GetComponent<Enemy>());
            }
        }
        HealthBar.fillAmount = (currentHealth / MaxHealth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Poison"))
        {
            OnTakeDamage(500000);
        }
    }
}
