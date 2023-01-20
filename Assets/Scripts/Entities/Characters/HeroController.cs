using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeroController : MonoBehaviour
{
    [SerializeField] float directionOffset;
    NavMeshAgent agent;
    Animator animator;
    [SerializeField] GameObject playerModel;
    public bool inputAction1;
    public bool inputAction2;
    public bool inputACtion3;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = playerModel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = transform.position + new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"))*directionOffset;
        animator.SetFloat("ForwardSpeed", agent.velocity.magnitude / agent.speed);

    }

    public void Inputs()
    {
        inputAction1 = Input.GetKeyDown(KeyCode.E);
        inputAction2 = Input.GetKeyDown(KeyCode.R);
        inputACtion3 = Input.GetKeyDown(KeyCode.C);
    }
}
