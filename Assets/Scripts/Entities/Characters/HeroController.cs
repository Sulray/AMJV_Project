using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HeroController : MonoBehaviour
{
    [SerializeField] private PlayerType playerType;
    [SerializeField] public PlayerParameter playerData;
    private Attack attack;

    //[SerializeField] float directionOffset;
    NavMeshAgent agent;
    Animator animator;
    //[SerializeField] GameObject playerModel;
    public float speed;

    public bool inputAction1;
    public bool inputAction2;
    public bool inputAction3;

    public float cooldown1;
    public float cooldown2;
    public float cooldown3;

    [SerializeField]
    CooldownUI lClickCooldown; //Left click
    [SerializeField]
    CooldownUI rClickCooldown; //Right click
    [SerializeField]
    CooldownUI spaceCooldown; //Space bar


    bool isCooldown1Over = true;
    bool isCooldown2Over = true;
    bool isCooldown3Over = true;
    
    public bool canMove = true;


    public bool inputJump;
    float xMove;
    float zMove;

    Rigidbody rb;
    [SerializeField] float buttonTimeJump = 0.3f;
    [SerializeField] float jumpAmount = 20;
    float jumpTime;
    bool jumping;
    public bool grounded;
    [SerializeField] float groundDetection;
    [SerializeField] float gravityOnFall;

    public Ray rayMouse;

    float momentSpeed;
    bool moving;
    private Vector3 positionRay;

    [SerializeField]
    private LevelSystem levelSystem;
    [SerializeField] int experienceAmount = 1;


    private void Awake()
    {
        GetComponent<Health>().MaxHealth = playerData.startingHealth;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        //agent = GetComponent<NavMeshAgent>();
        animator = this.GetComponentInChildren<Animator>();
        //animator = playerModel.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        speed = playerData.startingSpeed;
        cooldown1 = playerData.startingCD1;
        cooldown2 = playerData.startingCD2;
        cooldown3 = playerData.startingCD3;
        
        Debug.Log("setHealth" + playerData.startingHealth);
        switch (playerType)
        {
            case PlayerType.Mage:
                attack = gameObject.AddComponent<MageAttack>();
                break;
            case PlayerType.Hunter:
                attack = gameObject.AddComponent<HunterAttack>();
                break;
            case PlayerType.Knight:
                attack = gameObject.AddComponent<KnightAttack>();
                break;

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
        Action();
        inAir();
        LookAt();
        grounded = IsGrounded();

        //momentSpeed = agent.velocity.magnitude / agent.speed;
        momentSpeed = rb.velocity.magnitude / speed;

        //Debug.Log(momentSpeed);
        moving = (momentSpeed != 0);
        //Debug.Log("Grounded : " + grounded);
        if (grounded) 
        {
            rb.velocity = new Vector3(xMove, 0, zMove).normalized * speed + new Vector3(0, rb.velocity.y, 0); // Creates velocity in direction of value equal to keypress (WASD). rb.velocity.y deals with falling + jumping by setting velocity to y. 
            //agent.destination = transform.position + new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //* directionOffset  
        }
        animator.SetFloat("floatVelocity",momentSpeed);
        //animator.SetBool("Moving", moving);
        //Debug.Log(rb.velocity+" at speed "+speed);
    }


    bool IsGrounded()
    {
        Debug.DrawRay(transform.position-Vector3.up*0.1f, -Vector3.up * (0.2f + groundDetection), Color.red);
        return Physics.Raycast(transform.position+Vector3.up*0.2f, -Vector3.up,0.3f+groundDetection);

    }

    void LookAt()
    {
        RaycastHit hit;
        if (Physics.Raycast(rayMouse, out hit))
        {
            positionRay = hit.point;
        }
        Quaternion Rotation = Quaternion.LookRotation(new Vector3(positionRay.x, transform.position.y, positionRay.z) - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Rotation, 0.3f);
    }

    public void Inputs()
    {
        rayMouse = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (canMove)
        {

            xMove = Input.GetAxisRaw("Horizontal"); // d key changes value to 1, a key changes value to -1
            zMove = Input.GetAxisRaw("Vertical"); // w key changes value to 1, s key changes value to -1
            inputAction1 = Input.GetKeyDown(KeyCode.Mouse0);
            inputAction2 = Input.GetKeyDown(KeyCode.Mouse1);
            inputAction3 = Input.GetKeyDown(KeyCode.Space);
        }

    }

    public void Action()
    {
        if (inputAction1 && isCooldown1Over)
        {
            StartCoroutine(TimeAttack(1,0.5f));
            StartCoroutine(Action1Cooldown(cooldown1));
            attack.First();
        }
        if (inputAction2 && isCooldown2Over)
        {
            StartCoroutine(TimeAttack(2, 0.5f));
            StartCoroutine(Action2Cooldown(cooldown2));
            attack.Second();
        }
        if (inputAction3 && isCooldown3Over)
        {
            StartCoroutine(TimeAttack(3, 0.5f));
            StartCoroutine(Action3Cooldown(cooldown3));
            attack.Third();
        }
        /*
        if (inputJump && isGrounded == true)
        {
            animator.SetFloat("Jumping", 1);
            animator.SetFloat("Trigger Number", 1);
            jumping = true;
            jumpTime = 0;

        }
       
        if (jumping)
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpAmount, rb.velocity.z);
            jumping = false;
            
            //    jumpTime += Time.deltaTime;
        }
        //if (Input.GetKeyUp(KeyCode.Space) | jumpTime > buttonTimeJump)
        //{
        //    jumping = false;
        //}
        */
    }

    public void inAir()
    {

        //Debug.Log("velocity on y : " + rb.velocity.y);

        if ((rb.velocity.y < -0.1f) && (!grounded))
        {
            //rb.AddForce(new Vector3(0, gravityOnFall, 0), ForceMode.Acceleration);
            animator.SetInteger("intAir", 2);
        }
        else if (grounded)
        {
            animator.SetInteger("intAir", 0);
        }
        /*
        if (grounded)
        {
            animator.SetInteger("intAir", 0);
            //agent.enabled = true;

        }
        else
        {
            //agent.enabled = false;
        }
        */
    }

    void OnCollisionEnter(Collision hit)
    {
        /*
        if (hit.gameObject.CompareTag("Ground"))
        {
            //iGrounded = true;
            animator.SetInteger("intAir", 0);
            rb.velocity = new Vector3(rb.velocity.x,0,rb.velocity.z);
        }
        */

    }

    void OnCollisionExit(Collision hit)
    {
        if (hit.gameObject.CompareTag("Ground"))
        {
            //isGrounded = false;
        }
    }


    public IEnumerator Action1Cooldown(float cooldown) //la coroutine prend en arguments le temps de cooldown pour une action donnée
                                                       //et le booléen correspondant à l'action.
    {
        isCooldown1Over = false;
        Debug.Log(cooldown);
        StartCoroutine(lClickCooldown.ShowCooldown((int)cooldown));
        yield return new WaitForSeconds(cooldown);
        isCooldown1Over = true;
    }

    public IEnumerator Action2Cooldown(float cooldown) //la coroutine prend en arguments le temps de cooldown pour une action donnée
                                                       //et le booléen correspondant à l'action.
    {
        isCooldown2Over = false;
        Debug.Log(cooldown);
        StartCoroutine(rClickCooldown.ShowCooldown((int)cooldown));
        yield return new WaitForSeconds(cooldown);
        isCooldown2Over = true;
    }

    public IEnumerator Action3Cooldown(float cooldown) //la coroutine prend en arguments le temps de cooldown pour une action donnée
                                                       //et le booléen correspondant à l'action.
    {
        isCooldown3Over = false;
        StartCoroutine(spaceCooldown.ShowCooldown((int)cooldown));
        yield return new WaitForSeconds(cooldown);
        isCooldown3Over = true;
    }
    IEnumerator TimeAttack(int intAttack, float wait)
    {
        animator.SetInteger("intAttack", intAttack);
        canMove = false;
        xMove = 0f;
        zMove = 0f;
        inputAction1 = false;
        inputAction2 = false;
        inputAction3 = false;
        yield return new WaitForSeconds(wait);
        canMove = true;
        animator.SetInteger("intAttack", 0);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Experience"))
        {
            levelSystem.AddExeprience(experienceAmount);
        }
    }

}
