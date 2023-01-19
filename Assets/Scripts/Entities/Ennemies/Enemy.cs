using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public abstract class Enemy : MonoBehaviour
{
    //We use EnemyType to instanciate Move and Attack types rather than attaching scripts directly
    [SerializeField] private EnemyType enemyType;
    [SerializeField] private EnemyParameter enemyData;
    private Attack attack;
    private Movement movement;

    [SerializeField] protected GameObject player;

    NavMeshAgent agent;
    Animator animator;
    [SerializeField] GameObject enemyModel;
    //GameObject player;
    //Transform playerTransform;
    //[SerializeField] float distanceDetection = 5f;
    protected void Awake()
    {

    }
    protected void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = enemyModel.GetComponent<Animator>();
        // player = GameObject.FindGameObjectsWithTag("Player")[0];
        //playerTransform = player.transform;

        switch (enemyType)
        {
            case EnemyType.Warrior:
                break;
            case EnemyType.Archer:
                attack = gameObject.AddComponent<ArcherAttack>();
                movement = gameObject.AddComponent<ArcherMovement>();
                Debug.Log(attack);
                Debug.Log(movement);
                break;
            case EnemyType.Liche:
                break;
            default:
                break;
        }

    }

    protected void Update()
    {
        attack.First();
        agent.destination = movement.Move(player);
        animator.SetFloat("ForwardSpeed", agent.velocity.magnitude / agent.speed);
    }
}
