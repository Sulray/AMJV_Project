using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;


public class Boss : MonoBehaviour
{

    BossStates currentState;
    
    Health health;
    NavMeshAgent agent;
    public Animator animator;
    Rigidbody rb;
    [SerializeField] float maximumHealth;
    bool isCooldownOver;
    bool canMove;
    bool canAttack;

    Transform target;

    [SerializeField] float speed = 15;
    [SerializeField] float speedJump = 15;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.acceleration = speed * 10;

        health = this.GetComponent<Health>();
        health.MaxHealth = maximumHealth;

        animator = this.GetComponentInChildren<Animator>();

        rb = this.GetComponent<Rigidbody>();
        rb.isKinematic = true;

    }

    // Update is called once per frame
    void Update()
    {

        BossStates healthState= CheckExit();
        if (healthState != currentState)
        {
            TransitionToState(healthState);
        }


        switch (currentState)
        {
            case BossStates.FirstPhase:
                Debug.Log("Phase 1");
                UpdateFirstPhase();
                break;

            case BossStates.SecondPhase:
                Debug.Log("Phase 2");
                UpdateSecondPhase();
                break;

            case BossStates.ThirdPhase:
                Debug.Log("Phase 3");
                UpdateThirdPhase();
                break;

            default:
                Debug.Log("State not in the list");
                break;
        }
    }


    void UpdateFirstPhase()
    {
        if (isCooldownOver && canAttack)
        {

        }
    }

    void UpdateSecondPhase()
    {
        if (isCooldownOver && canAttack)
        {

        }
    }

    void UpdateThirdPhase()
    {
        if (isCooldownOver && canAttack)
        {

        }
    }

    

    void TransitionToState(BossStates newState)
    {
        switch (newState)
        {
            case BossStates.FirstPhase:
                Debug.Log("Phase 1");
                break;
            case BossStates.SecondPhase:
                Debug.Log("Phase 2");
                break;
            case BossStates.ThirdPhase:
                Debug.Log("Phase 3");
                break;
            default:
                Debug.Log("State not in the list");
                break;
        }
        SwitchToState(newState);
    }


    void SwitchToState(BossStates newState)
    {
        currentState = newState;
    }



    public IEnumerator Action2Cooldown(float cooldown) //la coroutine prend en arguments le temps de cooldown pour une action donnée
                                                       //et le booléen correspondant à l'action.
    {
        isCooldownOver = false;
        yield return new WaitForSeconds(cooldown);
        isCooldownOver = true;
    }


    IEnumerator TimeAttack(int intAttack, float wait)
    {
        animator.SetInteger("intAttack", intAttack);
        canMove = false;
        
        yield return new WaitForSeconds(wait);
        canMove = true;
        animator.SetInteger("intAttack", 0);

    }
    BossStates CheckExit()
    {
        float healthProgress = health.currentHealth / maximumHealth;
        if(healthProgress<0.33)
        {
            return BossStates.ThirdPhase;

        }
        if (healthProgress < 0.66)
        {
            return BossStates.SecondPhase;

        }
        return BossStates.FirstPhase;

    }


    void JumpAttack(float timeInAir) // T = 2 * v * sin(teta) / g
    {
        Vector3 toTarget = target.position - transform.position;

        // Set up the terms we need to solve the quadratic equations.
        float gSquared = Physics.gravity.sqrMagnitude;
        float b = speedJump * speedJump + Vector3.Dot(toTarget, Physics.gravity);
        float discriminant = b * b - gSquared * toTarget.sqrMagnitude;

        // Check whether the target is reachable at max speed or less.
        if (discriminant < 0)
        {
            // Target is too far away to hit at this speed.
            // Abort, or fire at max speed in its general direction?
        }

        float discRoot = Mathf.Sqrt(discriminant);

        // Highest shot with the given max speed:
        float T_max = Mathf.Sqrt((b + discRoot) * 2f / gSquared);

        // Most direct shot with the given max speed:
        float T_min = Mathf.Sqrt((b - discRoot) * 2f / gSquared);

        // Lowest-speed arc available:
        float T_lowEnergy = Mathf.Sqrt(Mathf.Sqrt(toTarget.sqrMagnitude * 4f / gSquared));

        float T = T_min; // choose T_max, T_min, or some T in-between like T_lowEnergy

        // Convert from time-to-hit to a launch velocity:
        Vector3 velocity = toTarget / T - Physics.gravity * T / 2f;

        // Apply the calculated velocity (do not use force, acceleration, or impulse modes)
        rb.AddForce(velocity, ForceMode.VelocityChange);
    }

    void RushAttack()
    {

    }



    void StrikeAttack() //voir pour augmenter portée en fonction phase en rajoutant effet de compression air
    {

    }




}
