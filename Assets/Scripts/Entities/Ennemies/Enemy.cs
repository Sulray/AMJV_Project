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
    private Attack attack;
    private Movement movement;

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
                //attack = gameObject.AddComponent<*nom de votre script d'attaque*>();
                //movement = gameObject.AddComponent<*nom de votre script de mouvement*>();
                break;
            case EnemyType.Archer:
                attack = gameObject.AddComponent<ArcherAttack>();
                movement = gameObject.AddComponent<ArcherMovement>();
                break;
            case EnemyType.Liche:
                //attack = gameObject.AddComponent<*nom de votre script d'attaque*>();
                //movement = gameObject.AddComponent<*nom de votre script de mouvement*>();
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
        attack.First();
        agent.destination = movement.Move(player);
        animator.SetFloat("ForwardSpeed", agent.velocity.magnitude / agent.speed);
    }
}
