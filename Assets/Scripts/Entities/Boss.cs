using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;


public class Boss : MonoBehaviour
{

    BossStates currentState;
    
    NavMeshAgent agent;
    public Animator animator;
    Rigidbody rb;
    CapsuleCollider collider;

    Transform target;

    Health health;
    [SerializeField] float maximumHealth;

    bool isCooldown1Over;
    bool isCooldown2Over;
    bool canMove;
    bool canAttack;
    bool waitForAttack;

    public bool grounded;

    [SerializeField] float groundDetection = 0.5f;
    [SerializeField] float speedPhase1 = 5;
    [SerializeField] float speedPhase2 = 7;
    [SerializeField] float speedPhase3 = 9;

    [SerializeField] float timeAttackAnimation = 0.5f;


    [SerializeField] float cooldownStrike = 3;
    [SerializeField] float cooldownRangeStrike = 5;
    [SerializeField] float cooldownJump = 15;
    [SerializeField] float cooldownBigJump = 10;
    [SerializeField] float cooldownSpeedBoost = 10;
    [SerializeField] float cooldownCanAttack = 2;

    [SerializeField] float speedBoostValue = 15;
    [SerializeField] float speedBoostTime = 2;



    [SerializeField] float speedJump = 15;
    [SerializeField] float radiusEarthquake = 2;
    [SerializeField] float radiusBigEarthquake = 5;

    [SerializeField] int damageEarthquake = 2; //from jump attack
    [SerializeField] int damageBigEarthquake = 5;
    [SerializeField] int rangeEarthquake = 2; //from jump attack
    [SerializeField] int rangeBigEarthquake = 5;
    [SerializeField] int knockbackEarthquake = 2; //from jump attack
    [SerializeField] int knockbackBigEarthquake = 5;


    [SerializeField] int damageStrike = 2;
    [SerializeField] int damageRangeStrike = 5;
    [SerializeField] int rangeStrike = 2;
    [SerializeField] int rangeRangeStrike = 5;

    [SerializeField] LayerMask Player;



    float gSquared = Physics.gravity.sqrMagnitude;


    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;

        agent = this.GetComponent<NavMeshAgent>();
        agent.speed = speedPhase1;
        agent.acceleration = agent.speed * 2;

        health = this.GetComponent<Health>();
        health.MaxHealth = maximumHealth;

        animator = this.GetComponentInChildren<Animator>();

        collider = this.GetComponent<CapsuleCollider>();
        rb = this.GetComponent<Rigidbody>();
        rb.isKinematic = true;

        canMove = true;
        canAttack = true;
        waitForAttack = false;

    }

    // Update is called once per frame
    void Update()
    {
        grounded = IsGrounded();

        BossStates healthState= CheckExit();
        if (healthState != currentState)
        {
            TransitionToState(healthState);
        }
        /*
        if (!canAttack && !waitForAttack)
        {
            StartCoroutine(GetAttack(cooldownCanAttack));
        }
        */
        if (canMove)
        {
            agent.destination = target.position;
            animator.SetFloat("floatVelocity",agent.velocity.magnitude/agent.speed);
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
       
        if (isCooldown2Over && canAttack)
        {
            StartCoroutine(SpeedBoostAttack(speedBoostTime, speedBoostValue));
        }
        else if (isCooldown1Over && canAttack)
        {
            StrikeAttack(rangeStrike, cooldownStrike, damageStrike);
        }
    
    }

    void UpdateSecondPhase()
    {
        if (isCooldown2Over && canAttack)
        {
            CheckJumpAttack(cooldownJump, true, damageEarthquake,knockbackEarthquake, rangeEarthquake);
        }
        else if (isCooldown1Over && canAttack)
        {
            StrikeAttack(rangeStrike, cooldownStrike, damageStrike);
        }
    }

    void UpdateThirdPhase()
    {
        if (isCooldown2Over && canAttack)
        {
            CheckJumpAttack(cooldownBigJump, true, damageBigEarthquake, knockbackBigEarthquake, rangeBigEarthquake);
        }
        else if (isCooldown1Over && canAttack)
        {
            StrikeAttack(rangeRangeStrike, cooldownRangeStrike, damageRangeStrike);
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
                agent.speed = speedPhase2;
                agent.acceleration = agent.speed * 2;
                break;
            case BossStates.ThirdPhase:
                Debug.Log("Phase 3");
                agent.speed = speedPhase3;
                agent.acceleration = agent.speed * 2;
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


    public IEnumerator Action1Cooldown(float cooldown) //la coroutine prend en arguments le temps de cooldown pour une action donnée
                                                       //et le booléen correspondant à l'action.
    {
        isCooldown1Over = false;
        yield return new WaitForSeconds(cooldown);
        isCooldown1Over = true;
    }



    public IEnumerator Action2Cooldown(float cooldown) //la coroutine prend en arguments le temps de cooldown pour une action donnée
                                                       //et le booléen correspondant à l'action.
    {
        isCooldown2Over = false;
        yield return new WaitForSeconds(cooldown);
        isCooldown2Over = true;
    }


    IEnumerator AttackAnimation(int intAttack, float wait)
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

   

    IEnumerator SpeedBoostAttack(float time,float speedValue)
    {
        agent.speed= speedValue;
        StartCoroutine(Action2Cooldown(cooldownSpeedBoost));

        yield return new WaitForSeconds(time);
        agent.speed = speedPhase1; //choice to let canAttack true
    }

    void StrikeAttack(float range, float cooldown, float damage) //voir pour augmenter portée en fonction phase en rajoutant effet de compression air
    {
        RaycastHit hit;
        if (Physics.CapsuleCast(this.transform.position, target.position.normalized*range, collider.radius , transform.forward, out hit, Player))
        {

            StartCoroutine(Action1Cooldown(cooldown));
            StartCoroutine(AttackAnimation(1,timeAttackAnimation));
            StartCoroutine(WaitAttack(timeAttackAnimation));
            if(hit.collider.gameObject.tag == "Player")
            {
                hit.collider.gameObject.GetComponent<Health>().OnTakeDamage(damageStrike);
                Debug.Log("damage strike");
            }
            

        }
    }


    void CheckJumpAttack(float cooldown, bool lowestJump, int damageEarthquake, float knockback, float range)
    {
        Vector3 toTarget = target.position - transform.position;

        // Set up the terms we need to solve the quadratic equations.
        float b = speedJump * speedJump + Vector3.Dot(toTarget, Physics.gravity);
        float discriminant = b * b - gSquared * toTarget.sqrMagnitude;

        // Check whether the target is reachable at max speed or less.
        if (discriminant >= 0)
        {
            // Target is too far away to hit at this speed.
            // Abort, or fire at max speed in its general direction?
            JumpAttack(cooldown, lowestJump,discriminant, b, toTarget, damageEarthquake, knockback, range);
        }
    }

    void JumpAttack(float cooldown, bool lowestJump, float discriminant, float b, Vector3 toTarget, int damageEarthquake, float knockback, float range) // T = 2 * v * sin(teta) / g
    {
        StartCoroutine(Action2Cooldown(cooldown));
        float discRoot = Mathf.Sqrt(discriminant);

        // Highest shot with the given max speed:
        float T_max = Mathf.Sqrt((b + discRoot) * 2f / gSquared);

        // Most direct shot with the given max speed:
        float T_min = Mathf.Sqrt((b - discRoot) * 2f / gSquared);

        // Lowest-speed arc available:
        float T_lowEnergy = Mathf.Sqrt(Mathf.Sqrt(toTarget.sqrMagnitude * 4f / gSquared));


        float T = (lowestJump) ?  T_lowEnergy : T_max; // choose T_max, T_min, or some T in-between like T_lowEnergy

        // Convert from time-to-hit to a launch velocity:
        Vector3 velocity = toTarget / T - Physics.gravity * T / 2f;

        // Apply the calculated velocity (do not use force, acceleration, or impulse modes)
        agent.enabled = false;
        rb.isKinematic = false;
        rb.AddForce(velocity, ForceMode.VelocityChange);
        StartCoroutine(TimeOnAirJump(T, damageEarthquake, knockback, range));
    }



    IEnumerator TimeOnAirJump(float wait, int damageEarthquake, float knockback, float range)
    {
        canMove = false;
        canAttack = false;


        yield return new WaitForSeconds(wait);

        rb.isKinematic = true;
        agent.enabled = true;
        canMove = true;
        canAttack = true;
        Earthquake(damageEarthquake,knockback, range);

    }
    void Earthquake(int damageEarthquake, float knockback, float range)
    {
        RaycastHit hit;
        if (Physics.SphereCast(this.transform.position, range, transform.forward, out hit, Player))
        {
            GameObject objectHit = hit.collider.gameObject;
            StartCoroutine(WaitAttack(timeAttackAnimation));
            // mtn faut voir les dégâts et knockback
            if (objectHit.tag == "Player")
            {
                objectHit.GetComponent<Health>().OnTakeDamage(damageEarthquake);
                objectHit.GetComponent<Rigidbody>().AddForce((objectHit.transform.position - this.transform.position).normalized * knockback, ForceMode.Impulse);
                Debug.Log("damage earthquake");
            }

        }
    }



    bool IsGrounded()
    {
        Debug.DrawRay(transform.position - Vector3.up * 0.1f, -Vector3.up * (0.2f + groundDetection), Color.red);
        return Physics.Raycast(transform.position + Vector3.up * 0.2f, -Vector3.up, 0.3f + groundDetection);

    }

    public void inAir()
    {

        
        if ( (rb.isKinematic==false) &&(rb.velocity.y < -0.1f) && (!grounded))
        {
            animator.SetInteger("intAir", 2);
        }
        else if (grounded)
        {
            animator.SetInteger("intAir", 0);
        }
        
    }
    
    IEnumerator WaitAttack(float wait)
    {
        canAttack = false;

        yield return new WaitForSeconds(wait);
        canAttack = true;
    }
    
    IEnumerator GetAttack(float wait)
    {
        waitForAttack = true;
        yield return new WaitForSeconds(wait);
        canAttack = true;
        waitForAttack = false;

    }
}
