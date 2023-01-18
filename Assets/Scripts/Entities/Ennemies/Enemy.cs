using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public abstract class Enemy : Entity
{
    [SerializeField] protected EnemyParameter enemyData;
    NavMeshAgent agent;
    Animator animator;
    [SerializeField] GameObject enemyModel;
    //GameObject player;
    //Transform playerTransform;
    //[SerializeField] float distanceDetection = 5f;

    protected void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = enemyModel.GetComponent<Animator>();
       // player = GameObject.FindGameObjectsWithTag("Player")[0];
        //playerTransform = player.transform;

    }

    protected void Update()
    {
        agent.destination = enemyData.movement.Move();
        animator.SetFloat("ForwardSpeed", agent.velocity.magnitude / agent.speed);
    }
}
