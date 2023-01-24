using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    //We use EnemyType to instanciate Move and Attack types rather than attaching scripts directly

    //General Datas/Scripts
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private EnemyParameter enemyData;
    private Health health;
    private Strategy strategy;
    private bool cdUp;

    //To be destroyed
    public WaveManager Manager { get; set; }
    //Needed by Strategy script / general behaviour
    public GameObject Player { get; set; }
    public Camera Camera { get; set; }
    public ProjectileManager ProjectileManager { get; set; }
    public ExperiencePool Xp { get; set; }


    //For movement
    public bool canMove;
    NavMeshAgent agent;
    Rigidbody rb;
    Animator animator;
    [SerializeField] GameObject enemyModel;
    private float knockbackForce = 4.5f;
    protected void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
    }
    protected void Start()
    {
        canMove = true;
        agent = GetComponent<NavMeshAgent>();
        animator = enemyModel.GetComponent<Animator>();
        agent.speed = enemyData.speed;
        agent.acceleration = 10 * enemyData.speed;
        health = gameObject.GetComponent<Health>();
        health.MaxHealth = enemyData.maxHealth;
        switch (enemyType)
        {
            case EnemyType.Soldier:

                strategy = gameObject.GetComponent<SoldierStrategy>();
                strategy.Target = this.Player;

                break;
            case EnemyType.Archer:
                strategy = gameObject.AddComponent<ArcherStrategy>();
                strategy.Camera = this.Camera;
                strategy.Target = this.Player;
                strategy.ArrowManager = this.ProjectileManager;

                break;
            case EnemyType.Liche:
                //strategy = gameObject.AddComponent<nom de votre script de stratégie>();
                break;
            default:
                break;
        }
        StartCoroutine(Cooldown());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Player.GetComponent<Health>().OnTakeDamage(1);
            Debug.Log(health.currentHealth);
        }

        if (!canMove)
        {
            agent.speed = 0;
        }

        if (cdUp)
        {
            //si l'attaque réussi
            if (strategy.Attack())
            {
                StartCoroutine(Cooldown());
            }
        }

        if ((!agent.hasPath) || enemyType == EnemyType.Soldier)
        {
            agent.destination = strategy.Move();    
        }
        animator.SetFloat("ForwardSpeed", agent.velocity.magnitude / agent.speed);

    }

    private IEnumerator Cooldown()
    {
        cdUp = false;
        yield return new WaitForSeconds(enemyData.attackCD);
        cdUp = true;
    }

    public void GetKnockback(Vector3 positionOrigin)
    {
        Debug.Log("in knockback");
        Vector3 knockDirection = (this.gameObject.transform.position - positionOrigin).normalized;
        Vector3 knockback = knockDirection * knockbackForce;
        agent.enabled = false;
        rb.isKinematic = false;
        rb.AddForce(knockback, ForceMode.Impulse);
        StartCoroutine(TimeKnockback());
    }

    private IEnumerator TimeKnockback()
    {
        yield return new WaitForSeconds(0.8f);
        rb.isKinematic = true;
        agent.enabled = true;
    }

    public void Despawn()//Despawns (events aren't working, this is current solution)
    {
        Manager.despawn(this);
    }
}