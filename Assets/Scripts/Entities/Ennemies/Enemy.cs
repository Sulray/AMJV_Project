using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public abstract class Enemy : Entity
{
    [Header("Enemy Parameters")]
    NavMeshAgent agent;
    Animator animator;
    [SerializeField] GameObject enemyModel;
    GameObject player;
    Transform playerTransform;
    [SerializeField] float distanceDetection = 5f;

    protected void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = enemyModel.GetComponent<Animator>();
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        playerTransform = player.transform;

    }

    void Update()
    {
        
    }
}
