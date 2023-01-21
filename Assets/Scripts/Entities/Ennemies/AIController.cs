using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class AIController : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    [SerializeField] UnityEngine.GameObject enemyModel;
    UnityEngine.GameObject player;
    Transform playerTransform;
    [SerializeField] float distanceDetection = 5f;

    void Start()
    {
        Debug.Log("ai");
        agent = GetComponent<NavMeshAgent>();
        animator = enemyModel.GetComponent<Animator>();
        player = UnityEngine.GameObject.FindGameObjectsWithTag("Player")[0];
        playerTransform = player.transform;

    }

    void Update()
    {
        if(Vector3.Distance(this.transform.position,playerTransform.position)<distanceDetection)
        {
            agent.destination = playerTransform.position;
        }

        animator.SetFloat("ForwardSpeed", agent.velocity.magnitude / agent.speed);
    }
}
