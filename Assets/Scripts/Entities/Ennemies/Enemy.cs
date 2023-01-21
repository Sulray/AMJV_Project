using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class Enemy : MonoBehaviour
{
    //We use EnemyType to instanciate Move and Attack types rather than attaching scripts directly
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private EnemyParameter enemyData;
    private Health health;
    private float cdTimer = 0f;
    private Strategy strategy;

    public GameObject Player { get; set; }
    public Camera Camera { get; set; }

    NavMeshAgent agent;
    Rigidbody rb;
    Animator animator;
    [SerializeField] GameObject enemyModel;
    private float knockbackForce = 100f;
    protected void Awake()
    {
        rb.isKinematic = true;
    }
    protected void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = enemyModel.GetComponent<Animator>();
        agent.speed = enemyData.speed;
        agent.acceleration = 10 * enemyData.speed;
        health = new Health(enemyData.maxHealth);
        switch (enemyType)
        {
            case EnemyType.Warrior:
                //strategy = gameObject.AddComponent<*nom de votre script de stratégie*>();
                break;
            case EnemyType.Archer:
                strategy = gameObject.AddComponent<ArcherStrategy>();
                
                break;
            case EnemyType.Liche:
                //strategy = gameObject.AddComponent<*nom de votre script de stratégie*>();
                break;
            default:
                break;
        }

    }

    void Update()
    {
        strategy.Attack();
        agent.destination = strategy.Move();
        animator.SetFloat("ForwardSpeed", agent.velocity.magnitude / agent.speed);
    }

    public void GetKnockback(Vector3 positionOrigin)
    {
        Vector3 knockDirection = (transform.position - positionOrigin).normalized;
        Vector3 knockback = knockDirection * knockbackForce;
        agent.enabled = false;
        rb.isKinematic = false;
        rb.AddForce(knockback, ForceMode.Impulse);
        if (agent.velocity.magnitude < 1)
        {
            rb.isKinematic = true;
            agent.enabled = true;
        }
    }
}
