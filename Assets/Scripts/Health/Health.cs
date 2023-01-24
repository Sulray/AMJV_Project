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
    public float currentHealth;//private

    [SerializeField]
    private Image HealthBar;

    [SerializeField]
    private int hitDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        Debug.Log(HealthBar);
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
            if (gameObject.CompareTag("Player")){
                GameObject.FindWithTag("SceneManager").GetComponent<PauseMenu>().GameOver();
                //playerDeath?.Invoke();
            }
            else
            {
                GetComponent<Enemy>().Despawn();
                //enemyDeath?.Invoke(this.gameObject.GetComponent<Enemy>());
            }
                
        }
        Debug.Log(HealthBar);
        Debug.Log(currentHealth);
        Debug.Log(MaxHealth);
        HealthBar.fillAmount = (currentHealth / MaxHealth);
    }
}
