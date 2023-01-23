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

    //Needed by Strategy script
    public GameObject Player { get; set; }
    public Camera Camera { get; set; }
    public ProjectileManager ProjectileManager { get; set; }

    //For movement
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
        agent = GetComponent<NavMeshAgent>();
        animator = enemyModel.GetComponent<Animator>();
        agent.speed = enemyData.speed;
        agent.acceleration = 10 * enemyData.speed;
        //health = gameObject.AddComponent<Health>();
        //health.MaxHealth = enemyData.maxHealth;
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
        if (cdUp)
        {
            //si l'attaque réussi
            if (strategy.Attack())
            {
                StartCoroutine(Cooldown());
            }
        }
        
        if((!agent.hasPath) || enemyType == EnemyType.Soldier)
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
}
