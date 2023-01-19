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
    private Strategy strategy;

    private GameObject player;

    NavMeshAgent agent;
    Animator animator;
    [SerializeField] GameObject enemyModel;
    protected void Awake()
    {

    }
    protected void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = enemyModel.GetComponent<Animator>();

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

    public void SetTarget(GameObject target)
    {
        player = target;
    }

    void Update()
    {
        strategy.Attack();
        agent.destination = strategy.Move(player);
        animator.SetFloat("ForwardSpeed", agent.velocity.magnitude / agent.speed);
    }
}
